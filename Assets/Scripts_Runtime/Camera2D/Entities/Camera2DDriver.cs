using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public class Camera2DDriver {

        FVector2 pos;
        public FVector2 Pos => pos;

        Bounds colliderBox;
        public Bounds ColliderBox => colliderBox;

        public Camera2DDriver(FVector2 pos, FVector2 size) {
            this.pos = pos;
            colliderBox = new Bounds(pos, size);
        }

        public void SetPos(FVector2 pos) {
            this.pos = pos;
        }

    }

}