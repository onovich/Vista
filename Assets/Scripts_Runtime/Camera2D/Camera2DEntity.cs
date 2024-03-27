using MortiseFrame.Abacus;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class Camera2DEntity {

        // ID
        int id;
        public int ID => id;

        // Pos
        FVector2 pos;

        // Driver
        Camera2DDriver driver;

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
        public CameraFSMComponent FSMCom => fsmCom;

        public Camera2DEntity(int id, FVector2 pos, Bounds confiner, Bounds deadZone, Bounds viewSize) {
            fsmCom = new CameraFSMComponent();
        }

        public void Inject(Camera2DDriver driver) {
            this.driver = driver;
        }

        public void Driver_Set(Camera2DDriver driver) {
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

        public void MoveToTarget(FVector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None) {
            fsmCom.EnterMovingToTarget(pos, target, duration, easingType, easingMode);
        }

        public void MoveByDriver(Camera2DDriver driver) {
            var driverMin = deadZone.Min;
            var driverMax = deadZone.Max;

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