using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public static class Camera2DStatePhase {

        public static void FSMTick(Camera2DEntity camera, float dt) {

            var fsmCom = camera.FSMCom;
            var status = fsmCom.Status;

            if (status == CameraFSMStatus.Idle) {
                TickIdle(camera, dt);
                return;
            }

            if (status == CameraFSMStatus.MovingToTarget) {
                TickMovingToTarget(camera, dt);
                return;
            }

            if (status == CameraFSMStatus.FadingIn) {
                TickFadingIn(camera, dt);
                return;
            }

            if (status == CameraFSMStatus.FadingOut) {
                TickFadingOut(camera, dt);
                return;
            }

        }

        static void TickIdle(Camera2DEntity camera, float dt) {
            var fsmCom = camera.FSMCom;
            if (fsmCom.Idle_isEntering) {
                fsmCom.Idle_isEntering = false;
            }
        }

        static void TickMovingToTarget(Camera2DEntity camera, float dt) {
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

            var pos = EasingHelper.Easing2D(startPos, targetPos, current, duration, easingType, easingMode);
            camera.Pos_Set(pos);

            fsmCom.MovingToTarget_IncTimer(dt);
            if (fsmCom.MovingToTarget_IsDone()) {
                fsmCom.MovingToTarget_OnComplete();
                fsmCom.EnterIdle();
            }
        }

        static void TickFadingIn(Camera2DEntity camera, float dt) {
            var fsmCom = camera.FSMCom;
            if (fsmCom.FadingIn_isEntering) {
                fsmCom.FadingIn_isEntering = false;
            }
        }

        static void TickFadingOut(Camera2DEntity camera, float dt) {
            var fsmCom = camera.FSMCom;
            if (fsmCom.FadingOut_isEntering) {
                fsmCom.FadingOut_isEntering = false;
            }
        }

    }

}