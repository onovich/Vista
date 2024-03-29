namespace MortiseFrame.Vista {

    public class CameraFadingComponent {

        public CameraFadingStatus Status { get; private set; }

        public bool Idle_isEntering { get; set; }

        public bool FadingIn_isEntering { get; set; }
        public float FadingIn_timer { get; set; }

        public bool FadingOut_isEntering { get; set; }
        public float FadingOut_timer { get; set; }


        public CameraFadingComponent() { }

        public void EnterIdle() {
            Status = CameraFadingStatus.Idle;
            Idle_isEntering = true;
        }

        public void EnterFadingIn(float duration) {
            Status = CameraFadingStatus.FadingIn;
            FadingIn_isEntering = true;
            FadingIn_timer = duration;
        }

        public void EnterFadingOut(float duration) {
            Status = CameraFadingStatus.FadingOut;
            FadingOut_isEntering = true;
            FadingOut_timer = duration;
        }

    }

}