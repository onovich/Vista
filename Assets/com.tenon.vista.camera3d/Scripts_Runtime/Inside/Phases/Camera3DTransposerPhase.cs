using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DTransposerPhase {

        internal static void FSMTick(Camera3DContext ctx, float dt) {

            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            var status = fsmCom.Status;

            if (current == null) {
                return;
            }

            if (!ctx.ConfinerIsVaild) {
                return;
            }

            if (status == Camera3DMovingStatus.MovingByDriver) {
                TickMovingByDriver(ctx, dt);
                return;
            }

        }

        static void TickMovingByDriver(Camera3DContext ctx, float dt) {
            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            if (fsmCom.MovingByDriver_isEntering) {
                fsmCom.MovingByDriver_isEntering = false;
            }

            var driver = fsmCom.MovingByDriver_driver;
            if (driver == null) {
                fsmCom.EnterIdle();
                return;
            }

            var mainCamera = ctx.MainCamera;
            var driverWorldPos = driver.position;

            FollowDriverWithTransposer(ctx, current.ID, mainCamera, driverWorldPos, dt);
        }

        internal static void FollowDriverWithTransposer(Camera3DContext ctx, int id, Camera mainCamera, Vector3 driverWorldPos, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            bool deadZoneEnable = currentCamera.Transposer_DeadZone_IsEnable();
            bool softZoneEnable = currentCamera.Transposer_SoftZone_IsEnable();
            Vector3 cameraWorldPos = currentCamera.Pos;
            Vector3 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = driverWorldPos;
                Camera3DMoveDomain.SetPos(ctx, id, mainCamera, targetPos);
                return;
            }

            var driverScreenPos = Camera3DMathUtil.WorldToScreenPos(mainCamera, driverWorldPos);
            var deadZoneDiff = currentCamera.Transposer_DeadZone_GetScreenDiff(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return;
            }

            // Get Depth
            var depth = Camera3DMathUtil.GetDepth(mainCamera, driverWorldPos - cameraWorldPos);

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneEnable) {
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, depth);
                targetPos += deadZoneWorldDiff;

                Camera3DMoveDomain.SetPos(ctx, id, mainCamera, targetPos);
                return;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneDiff = currentCamera.Transposer_SoftZone_GetScreenDiff(driverScreenPos);
            if (softZoneDiff == Vector2.zero) {
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, depth);
                targetPos += deadZoneWorldDiff;

                Vector3 damping = currentCamera.Transposer_SoftZone_DampingFactor;
                cameraWorldPos.x += (targetPos.x - cameraWorldPos.x) * damping.x * deltaTime;
                cameraWorldPos.y += (targetPos.y - cameraWorldPos.y) * damping.y * deltaTime;
                cameraWorldPos.z += (targetPos.z - cameraWorldPos.z) * damping.z * deltaTime;
                Camera3DMoveDomain.SetPos(ctx, id, mainCamera, cameraWorldPos);
                return;
            }

            // Driver 在 SoftZone 外：硬跟随 SoftZone Diff
            var softZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, softZoneDiff, depth);
            cameraWorldPos += softZoneWorldDiff;
            Camera3DMoveDomain.SetPos(ctx, id, mainCamera, cameraWorldPos);

        }

    }

}