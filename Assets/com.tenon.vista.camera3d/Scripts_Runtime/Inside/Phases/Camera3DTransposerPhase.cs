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
            Vector3 targetPos = cameraWorldPos;

            targetPos = driverWorldPos;
            // 临时代码: 硬跟随; 需改成: 根据每帧偏移量阻尼跟随
            Camera3DMoveDomain.SetPos(ctx, id, mainCamera, targetPos);

            Vector3 damping = currentCamera.Transposer_SoftZone_DampingFactor;
            cameraWorldPos.x += (targetPos.x - cameraWorldPos.x) * damping.x * deltaTime;
            cameraWorldPos.y += (targetPos.y - cameraWorldPos.y) * damping.y * deltaTime;
            cameraWorldPos.z += (targetPos.z - cameraWorldPos.z) * damping.z * deltaTime;
            Camera3DMoveDomain.SetPos(ctx, id, mainCamera, cameraWorldPos);
            return;

        }

    }

}