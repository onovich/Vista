using System;
using MortiseFrame.Abacus;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class CameraFSMComponent {

        public CameraFSMStatus Status { get; private set; }

        public bool Idle_isEntering { get; set; }

        public bool MovingToTarget_isEntering { get; set; }
        public FVector2 MovingToTarget_startPos { get; set; }
        public FVector2 MovingToTarget_targetPos { get; set; }
        public float MovingToTarget_current { get; set; }
        public float MovingToTarget_duration { get; set; }
        public EasingType MovingToTarget_easingType { get; set; }
        public EasingMode MovingToTarget_easingMode { get; set; }
        public Action MovingToTarget_onComplete { get; set; }

        public bool FadingIn_isEntering { get; set; }
        public float FadingIn_timer { get; set; }

        public bool FadingOut_isEntering { get; set; }
        public float FadingOut_timer { get; set; }

        public CameraFSMComponent() { }

        public void EnterIdle() {
            Status = CameraFSMStatus.Idle;
            Idle_isEntering = true;
        }

        public void EnterMovingToTarget(FVector2 startPos, FVector2 targetPos, float duration, EasingType easingType, EasingMode easingMode, Action onComplete = null) {
            Status = CameraFSMStatus.MovingToTarget;
            MovingToTarget_isEntering = true;
            MovingToTarget_startPos = startPos;
            MovingToTarget_targetPos = targetPos;
            MovingToTarget_current = 0f;
            MovingToTarget_duration = duration;
            MovingToTarget_easingType = easingType;
            MovingToTarget_onComplete = onComplete;
        }

        public void MovingToTarget_IncTimer(float dt) {
            MovingToTarget_current += dt;
        }

        public bool MovingToTarget_IsDone() {
            return MovingToTarget_current >= MovingToTarget_duration;
        }

        public void MovingToTarget_OnComplete() {
            MovingToTarget_onComplete?.Invoke();
        }

        public void EnterFadeIn(float duration) {
            Status = CameraFSMStatus.FadingIn;
            FadingIn_isEntering = true;
            FadingIn_timer = duration;
        }

        public void EnterFadeOut(float duration) {
            Status = CameraFSMStatus.FadingOut;
            FadingOut_isEntering = true;
            FadingOut_timer = duration;
        }

    }

}