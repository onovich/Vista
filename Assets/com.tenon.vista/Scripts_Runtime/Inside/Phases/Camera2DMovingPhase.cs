using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DMovingPhase {

        internal static void FSMTick(Camera2DContext ctx, float dt) {
            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            var status = fsmCom.Status;

            if (current == null) {
                return;
            }
            if (!ctx.ConfinerIsVaild) {
                return;
            }
            if (status == Camera2DMovingStatus.Idle) {
                TickIdle(ctx, dt);
                return;
            }
            if (status == Camera2DMovingStatus.MovingByDriver) {
                TickMovingByDriver(ctx, dt);
                return;
            }
            if (status == Camera2DMovingStatus.MovingByDriverRelease) {
                TickMovingByDriverRelease(ctx, dt);
                return;
            }
            if (status == Camera2DMovingStatus.MovingToTarget) {
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

            var driverPos = current.DriverPos;
            var lastFrameDriverPos = current.LastFrameDriverPos;
            var dampingtargetPos = Camera2DMoveDomain.GetDriverTargetWithDamping(ctx, current.ID, driverPos, dt);
            var targetPos = Camera2DMoveDomain.GetDriverTargetWithoutDamping(ctx, current.ID, driverPos, dt);
            if (driverPos == lastFrameDriverPos) {
                fsmCom.EnterMovingByDriverRelease(current.Pos,
                                                  targetPos,
                                                  current.EasingDuration,
                                                  current.EasingType,
                                                  current.EasingMode);
                return;
            }

            Camera2DMoveDomain.MoveByDriver(ctx, current.ID, dampingtargetPos);
        }

        static void TickMovingByDriverRelease(Camera2DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            var fsmCom = camera.FSMCom;
            if (fsmCom.MovingByDriverRelease_isEntering) {
                fsmCom.MovingByDriverRelease_isEntering = false;
            }

            var driverPos = camera.DriverPos;
            var lastFrameDriverPos = camera.LastFrameDriverPos;
            if (driverPos != lastFrameDriverPos) {
                fsmCom.EnterMovingByDriver();
                return;
            }

            var startPos = fsmCom.MovingByDriverRelease_startPos;
            var targetPos = fsmCom.MovingByDriverRelease_targetPos;

            if (fsmCom.MovingByDriverRelease_IsDone()) {
                Camera2DMoveDomain.RefreshCameraPos(ctx, camera.ID, targetPos);
                return;
            }

            if (startPos == targetPos) {
                return;
            }

            var current = fsmCom.MovingByDriverRelease_current;
            var duration = fsmCom.MovingByDriverRelease_duration;
            var easingType = fsmCom.MovingByDriverRelease_easingType;
            var easingMode = fsmCom.MovingByDriverRelease_easingMode;

            Camera2DMoveDomain.MoveToTarget(ctx, camera.ID, startPos, targetPos, current, duration, easingType, easingMode);
            fsmCom.MovingByDriverRelease_IncTimer(dt);
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

            Camera2DMoveDomain.MoveToTarget(ctx, camera.ID, startPos, targetPos, current, duration, easingType, easingMode);

            fsmCom.MovingToTarget_IncTimer(dt);
            if (fsmCom.MovingToTarget_IsDone()) {
                fsmCom.MovingToTarget_OnComplete();
                fsmCom.EnterIdle();
            }
        }

    }

}