namespace MortiseFrame.Vista {

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