namespace TenonKit.Vista.Camera3D {

    internal class IDService3D {

        byte cameraIDRecord;

        internal IDService3D() {
            cameraIDRecord = 0;
        }

        internal int PickCameraID() {
            return ++cameraIDRecord;
        }

    }

}