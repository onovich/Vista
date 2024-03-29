using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista {

    public class Camera2DEntity {

        // ID
        int id;
        public int ID => id;

        // Pos
        Vector2 pos;
        public Vector2 Pos => pos;

        // Confiner
        Bounds confiner;
        public Bounds Confiner => confiner;

        // DeadZone
        Bounds deadZone;
        public Bounds DeadZone => deadZone;

        // ViewSize
        Bounds viewSize;
        public Bounds ViewSize => viewSize;
        public Vector2 ViewSizeMax => (Vector2)viewSize.max + pos;
        public Vector2 ViewSizeMin => (Vector2)viewSize.min + pos;

        // FSM
        CameraMovingComponent fsmCom;
        public CameraMovingComponent FSMCom => fsmCom;

        public Camera2DEntity(int id, Vector2 pos, Bounds confiner, Bounds deadZone, Bounds viewSize) {
            fsmCom = new CameraMovingComponent();
            this.id = id;
            this.pos = pos;
            this.confiner = confiner;
            this.deadZone = deadZone;
            this.viewSize = viewSize;
        }

        // Pos
        public void Pos_Set(Vector2 pos) {
            this.pos = pos;
        }

        // Move
        public void SetMoveToTarget(Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            fsmCom.EnterMovingToTarget(pos, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(Transform driver) {
            fsmCom.EnterMovingByDriver(driver);
        }

        public void MoveToTarget(Vector2 startPos, Vector2 targetPos, float current, float duration, EasingType easingType, EasingMode easingMode) {
            pos = EasingHelper.Easing2D(startPos, targetPos, current, duration, easingType, easingMode);
        }

        public void MoveByDriver(Vector2 driverScreenPos) {
            var deadZoneMin = (Vector2)deadZone.min + pos;
            var deadZoneMax = (Vector2)deadZone.max + pos;

            var xDiffMin = driverScreenPos.x - deadZoneMin.x;
            var yDiffMin = driverScreenPos.y - deadZoneMin.y;
            var xDiffMax = driverScreenPos.x - deadZoneMax.x;
            var yDiffMax = driverScreenPos.y - deadZoneMax.y;

            if (xDiffMin < 0 || xDiffMax > 0) {
                pos.x += xDiffMin;
            }

            if (yDiffMin < 0 || yDiffMax > 0) {
                pos.y += yDiffMin;
            }
        }

    }

}