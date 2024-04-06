namespace TenonKit.Vista.Camera2D {

    internal class Camera2DFadingComponent {

        internal CameraFadingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool FadingIn_isEntering { get; set; }
        internal float FadingIn_timer { get; set; }

        internal bool FadingOut_isEntering { get; set; }
        internal float FadingOut_timer { get; set; }


        internal Camera2DFadingComponent() { }

        internal void EnterIdle() {
            Status = CameraFadingStatus.Idle;
            Idle_isEntering = true;
        }

        internal void EnterFadingIn(float duration) {
            Status = CameraFadingStatus.FadingIn;
            FadingIn_isEntering = true;
            FadingIn_timer = duration;
        }

        internal void EnterFadingOut(float duration) {
            Status = CameraFadingStatus.FadingOut;
            FadingOut_isEntering = true;
            FadingOut_timer = duration;
        }

    }

}