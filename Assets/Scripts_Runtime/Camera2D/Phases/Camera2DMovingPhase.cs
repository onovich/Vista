using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DMovingPhase {

        public static void FSMTick(Camera2DContext ctx, float dt) {

            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            var status = fsmCom.Status;

            if (status == CameraMovingStatus.Idle) {
                TickIdle(ctx, dt);
                return;
            }

            if (status == CameraMovingStatus.MovingByDriver) {
                TickMovingByDriver(ctx, dt);
                return;
            }

            if (status == CameraMovingStatus.MovingToTarget) {
                TickMovingToTarget(ctx, dt);
                return;
            }

        }

        static void TickIdle(Camera2DContext ctx, float dt) {
            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            if (fsmCom.Idle_isEntering) {
                fsmCom.Idle_isEntering = false;
            }
        }

        static void TickMovingByDriver(Camera2DContext ctx, float dt) {
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

            Camera2DDomain.MoveByDriver(ctx, current, mainCamera, driverWorldPos, dt);
        }

        static void TickMovingToTarget(Camera2DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            var fsmCom = camera.FSMCom;
            if (fsmCom.MovingToTarget_isEntering) {
                fsmCom.MovingToTarget_isEntering = false;
            }

            var startPos = fsmCom.MovingToTarget_startPos;
            var targetPos = fsmCom.MovingToTarget_targetPos;
            var current = fsmCom.MovingToTarget_current;
            var duration = fsmCom.MovingToTarget_duration;
            var easingType = fsmCom.MovingToTarget_easingType;
            var easingMode = fsmCom.MovingToTarget_easingMode;

            Camera2DDomain.MoveToTarget(ctx, camera, startPos, targetPos, current, duration, easingType, easingMode);
            ctx.MainCamera.transform.position = new Vector3(camera.Pos.x, camera.Pos.y, ctx.MainCamera.transform.position.z);

            fsmCom.MovingToTarget_IncTimer(dt);
            if (fsmCom.MovingToTarget_IsDone()) {
                fsmCom.MovingToTarget_OnComplete();
                fsmCom.EnterIdle();
            }
        }

    }

}