namespace MortiseFrame.Vista {

    public class Camera2DCore {

        Camera2DContext context;

        public Camera2DCore() {
            context = new Camera2DContext();
        }

        public void Tick(Camera2DEntity camera, float dt) {
            Camera2DFSMController.FSMTick(camera, dt);
        }

    }

}