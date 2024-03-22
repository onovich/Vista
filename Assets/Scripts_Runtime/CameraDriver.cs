using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public class CameraDriver {

        FVector2 pos;
        public FVector2 Pos => pos;

        Bounds deadZone;
        FVector2 deadZonePos;

        public FVector2 DeadZoneMin => deadZone.Min + deadZonePos;
        public FVector2 DeadZoneMax => deadZone.Max + deadZonePos;
        public FVector2 DeadZoneCenter => deadZone.Center + deadZonePos;
        public FVector2 DeadZoneSize => deadZone.Size;

        public CameraDriver(FVector2 pos) {
            this.pos = pos;
            deadZone = new Bounds(FVector2.zero, FVector2.zero);
        }

        public void SetDeadZone(FVector2 center, FVector2 size) {
            deadZone = new Bounds(center, size);
            deadZonePos = center;
        }

        public void SetPos(FVector2 pos) {
            this.pos = pos;
        }

    }

}