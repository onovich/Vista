using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public class Camera2DCore {

        Camera2DContext ctx;

        public Camera2DCore() {
            ctx = new Camera2DContext();
        }

        public void Tick(Camera2DEntity camera, float dt) {
            Camera2DFSMController.FSMTick(camera, dt);
        }

        public Camera2DEntity CreateCamera2D(FVector2 pos, FVector2 confinerSize, FVector2 confinerPos, FVector2 deadZoneSize, FVector2 deadZonePos, FVector2 viewSize) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos, deadZoneSize, deadZonePos, viewSize);
            ctx.AddCamera(camera, camera.ID);
            return camera;
        }

    }

}