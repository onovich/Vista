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
            var driverWorldPos = driver;

            FollowDriverWithTransposer(ctx, current.ID, mainCamera, driver, dt);
        }

        internal static void FollowDriverWithTransposer(Camera3DContext ctx, int id, Camera mainCamera, Transform driver, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            Vector3 cameraWorldPos = currentCamera.Pos;
            Vector3 targetWorldPos = currentCamera.GetDriverWorldFollowPoint();

            // 将目标位置转换为基于角色的局部坐标系
            var driverRotation = driver.rotation;
            var driverWorldPos = driver.position;
            Vector3 targetLocalPos = Quaternion.Inverse(driverRotation) * (targetWorldPos - driverWorldPos);
            Vector3 cameraLocalPos = Quaternion.Inverse(driverRotation) * (cameraWorldPos - driverWorldPos);

            // 仅修改局部 y 和 z 坐标
            cameraLocalPos.y += (targetLocalPos.y - cameraLocalPos.y) * currentCamera.Composer_SoftZone_DampingFactor.y * deltaTime;
            cameraLocalPos.z += (targetLocalPos.z - cameraLocalPos.z) * currentCamera.Composer_SoftZone_DampingFactor.z * deltaTime;

            // 将修改后的局部坐标转换回全局坐标系
            cameraWorldPos = driverWorldPos + (driverRotation * cameraLocalPos);

            Camera3DMoveDomain.SetPos(ctx, id, mainCamera, cameraWorldPos);
            return;
        }

    }

}