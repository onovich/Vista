namespace TenonKit.Vista.Camera2D {

    internal class IDService {

        byte cameraIDRecord;

        internal IDService() {
            cameraIDRecord = 0;
        }

        internal int PickCameraID() {
            return ++cameraIDRecord;
        }

    }

}