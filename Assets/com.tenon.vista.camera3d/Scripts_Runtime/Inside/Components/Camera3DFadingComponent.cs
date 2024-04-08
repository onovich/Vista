namespace TenonKit.Vista.Camera3D {

    internal class Camera3DFadingComponent {

        internal Camera3DFadingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool FadingIn_isEntering { get; set; }
        internal float FadingIn_timer { get; set; }

        internal bool FadingOut_isEntering { get; set; }
        internal float FadingOut_timer { get; set; }


        internal Camera3DFadingComponent() { }

        internal void EnterIdle() {
            Status = Camera3DFadingStatus.Idle;
            Idle_isEntering = true;
        }

        internal void EnterFadingIn(float duration) {
            Status = Camera3DFadingStatus.FadingIn;
            FadingIn_isEntering = true;
            FadingIn_timer = duration;
        }

        internal void EnterFadingOut(float duration) {
            Status = Camera3DFadingStatus.FadingOut;
            FadingOut_isEntering = true;
            FadingOut_timer = duration;
        }

    }

}