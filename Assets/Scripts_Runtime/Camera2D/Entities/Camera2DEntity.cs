using MortiseFrame.Abacus;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class Camera2DEntity {

        // ID
        int id;
        public int ID => id;

        // Pos
        FVector2 pos;
        public FVector2 Pos => pos;

        // Driver
        Camera2DDriver driver;
        public Camera2DDriver Driver => driver;

        // Confiner
        Bounds confiner;
        public Bounds Confiner => confiner;

        // DeadZone
        Bounds deadZone;
        public Bounds DeadZone => deadZone;

        // ViewSize
        Bounds viewSize;
        public Bounds ViewSize => viewSize;
        public FVector2 ViewSizeMax => viewSize.Max + pos;
        public FVector2 ViewSizeMin => viewSize.Min + pos;

        // FSM
        CameraFSMComponent fsmCom;
        public CameraFSMComponent FSMCom => fsmCom;

        public Camera2DEntity(int id, FVector2 pos, Bounds confiner, Bounds deadZone, Bounds viewSize) {
            fsmCom = new CameraFSMComponent();
            this.id = id;
            this.pos = pos;
            this.confiner = confiner;
            this.deadZone = deadZone;
            this.viewSize = viewSize;
        }

        public void Inject(Camera2DDriver driver) {
            this.driver = driver;
        }

        public void Driver_Set(Camera2DDriver driver) {
            this.driver = driver;
        }

        // Pos
        public void Pos_Set(FVector2 pos) {
            this.pos = pos;
        }

        // Move
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

            if (xDiffMin < 0 || xDiffMax > 0) {
                pos.x += xDiffMin;
            }

            if (yDiffMin < 0 || yDiffMax > 0) {
                pos.y += yDiffMin;
            }
        }

    }

}