namespace MortiseFrame.Vista {

    public class IDService {

        byte cameraIDRecord;

        public IDService() {
            cameraIDRecord = 0;
        }

        public int PickCameraID() {
            return ++cameraIDRecord;
        }

    }

}