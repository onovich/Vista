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
            var driverMin = driver.ColliderBox.Min;
            var driverMax = driver.ColliderBox.Max;
            var deadZoneMin = deadZone.Min;
            var deadZoneMax = deadZone.Max;

            var xDiffMin = driverMin.x - deadZoneMin.x;
            var yDiffMin = driverMin.y - deadZoneMin.y;
            var xDiffMax = driverMax.x - deadZoneMax.x;
            var yDiffMax = driverMax.y - deadZoneMax.y;

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