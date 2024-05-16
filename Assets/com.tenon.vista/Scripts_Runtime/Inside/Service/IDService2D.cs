namespace TenonKit.Vista.Camera2D {

    internal class IDService2D {

        byte cameraIDRecord;

        internal IDService2D() {
            cameraIDRecord = 0;
        }

        internal int PickCameraID() {
            return ++cameraIDRecord;
        }

    }

}