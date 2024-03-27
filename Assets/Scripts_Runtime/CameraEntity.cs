using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public class CameraEntity {

        // ID
        int id;
        public int ID => id;

        // Pos
        FVector2 pos;

        // Driver
        CameraDriver driver;

        // Confiner
        Bounds confiner;

        // DeadZone
        Bounds deadZone;

        // ViewSize
        Bounds viewSize;
        public FVector2 ViewSizeMax => viewSize.Max + pos;
        public FVector2 ViewSizeMin => viewSize.Min + pos;

        // FSM
        CameraFSMComponent fsmCom;

        public CameraEntity() {
            fsmCom = new CameraFSMComponent();
        }

        public void Inject(CameraDriver driver) {
            this.driver = driver;
        }

        public void Driver_Set(CameraDriver driver) {
            this.driver = driver;
        }

        // Move
        public void Pos_Set(FVector2 pos) {
            var confinerMin = confiner.Min;
            var confinerMax = confiner.Max;
            pos.x = FMath.Clamp(pos.x, confinerMin.x, confinerMax.x);
            pos.y = FMath.Clamp(pos.y, confinerMin.y, confinerMax.y);
            this.pos = pos;
        }

        public void MoveByDir(FVector2 dir) {
            var _pos = pos + dir;
            Pos_Set(_pos);
        }

        public void MoveByDriver(CameraDriver driver) {
            var driverMin = driver.DeadZoneMin;
            var driverMax = driver.DeadZoneMax;

            var xDiffMin = driverMin.x - ViewSizeMin.x;
            var yDiffMin = driverMin.y - ViewSizeMin.y;
            var xDiffMax = driverMax.x - ViewSizeMax.x;
            var yDiffMax = driverMax.y - ViewSizeMax.y;

            var _pos = pos;

            if (xDiffMin < 0 || xDiffMax > 0) {
                _pos.x += xDiffMin;
            }

            if (yDiffMin < 0 || yDiffMax > 0) {
                _pos.y += yDiffMin;
            }

            Pos_Set(_pos);
        }



    }

}