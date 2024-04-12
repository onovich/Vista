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
            Vector3 cameraWorldPos = currentCamera.Pos;
            Vector3 targetWorldPos = currentCamera.GetDriverWorldFollowPoint();

            var diff = targetWorldPos - cameraWorldPos;

            // Driver 静止时: 不跟随
            if (diff == Vector3.zero) {
                return;
            }

            // 阻尼跟随  Diff
            Vector3 damping = currentCamera.Composer_SoftZone_DampingFactor;
            cameraWorldPos.x += diff.x * damping.x * deltaTime;
            cameraWorldPos.y += diff.y * damping.y * deltaTime;
            cameraWorldPos.z += diff.z * damping.z * deltaTime;
            Camera3DMoveDomain.SetPos(ctx, id, mainCamera, cameraWorldPos);
            return;
        }

    }

}