using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public class Camera2DDriver {

        FVector2 pos;
        public FVector2 Pos => pos;

        public Camera2DDriver(FVector2 pos) {
            this.pos = pos;
        }

        public void SetPos(FVector2 pos) {
            this.pos = pos;
        }

    }

}