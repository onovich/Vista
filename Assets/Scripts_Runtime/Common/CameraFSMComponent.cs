using MortiseFrame.Abacus;

namespace MortiseFrame.Vista{

    public class CameraFSMComponent {

        public CameraFSMStatus Status { get; private set; }

        public bool Idle_isEntering { get; set; }

        public bool MovingToTarget_isEntering { get; set; }

        public bool FadingIn_isEntering { get; set; }
        public float FadingIn_timer { get; set; }

        public bool FadingOut_isEntering { get; set; }
        public float FadingOut_timer { get; set; }

        public FVector2 movingDir;

        public CameraFSMComponent() { }

        public void EnterIdle() {
            Status = CameraFSMStatus.Idle;
            Idle_isEntering = true;
        }

        public void EnterMovingToTarget(FVector2 movingDir) {
            Status = CameraFSMStatus.MovingToTarget;
            MovingToTarget_isEntering = true;
            this.movingDir = movingDir;
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