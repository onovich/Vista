using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DComposerPhase {

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

            LookAtDriverWithComposer(ctx, current.ID, mainCamera, driver, dt);
        }

        // Aim Driver
        static void LookAtDriverWithComposer(Camera3DContext ctx, int id, Camera mainCamera, Transform driver, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"LookAtDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 targetPosition = driver.position;
            Vector3 currentPosition = mainCamera.transform.position;

            // 计算目标方向
            Vector3 directionToTarget = (targetPosition - currentPosition).normalized;

            // 计算目标Yaw值
            float targetYaw = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;

            // 获取当前相机的旋转的欧拉角
            Vector3 currentEulerAngles = currentCamera.Rotation.eulerAngles;

            // 构建只改变Yaw的新旋转，保留Pitch和Roll
            Quaternion targetWorldRot = Quaternion.Euler(currentEulerAngles.x, targetYaw, currentEulerAngles.z);

            // 使用Slerp进行平滑过渡
            float rotationDamping = 1 - currentCamera.Composer_DampingFactor;
            Quaternion rot = Quaternion.Slerp(currentCamera.Rotation, targetWorldRot, rotationDamping);

            // 设置相机的新旋转
            Camera3DRotateDomain.SetRotation(ctx, id, rot);
        }

    }

}