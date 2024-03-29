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

        Bounds softZone;
        public Bounds SoftZone => softZone;

        // DampingFactor
        float dampingFactor = 1f;
        public float DampingFactor => dampingFactor;

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
            var softZoneMin = (Vector2)softZone.min + pos;
            var softZoneMax = (Vector2)softZone.max + pos;

            var xDiffMin = driverScreenPos.x - deadZoneMin.x;
            var yDiffMin = driverScreenPos.y - deadZoneMin.y;
            var xDiffMax = driverScreenPos.x - deadZoneMax.x;
            var yDiffMax = driverScreenPos.y - deadZoneMax.y;

            float dampingX = 1f;
            float dampingY = 1f;

            if (driverScreenPos.x < softZoneMin.x || driverScreenPos.x > softZoneMax.x) {
                float distanceOutsideXMin = Mathf.Max(0, softZoneMin.x - driverScreenPos.x);
                float distanceOutsideXMax = Mathf.Max(0, driverScreenPos.x - softZoneMax.x);
                float totalDistanceX = distanceOutsideXMin + distanceOutsideXMax;
                dampingX = Mathf.Clamp01(1 - (totalDistanceX / dampingFactor));
            }

            if (driverScreenPos.y < softZoneMin.y || driverScreenPos.y > softZoneMax.y) {
                float distanceOutsideYMin = Mathf.Max(0, softZoneMin.y - driverScreenPos.y);
                float distanceOutsideYMax = Mathf.Max(0, driverScreenPos.y - softZoneMax.y);
                float totalDistanceY = distanceOutsideYMin + distanceOutsideYMax;
                dampingY = Mathf.Clamp01(1 - (totalDistanceY / dampingFactor));
            }

            // 根据阻尼效果更新位置
            if (xDiffMin < 0 || xDiffMax > 0) {
                pos.x += (xDiffMin < 0 ? xDiffMin : xDiffMax) * dampingX;
            }
            if (yDiffMin < 0 || yDiffMax > 0) {
                pos.y += (yDiffMin < 0 ? yDiffMin : yDiffMax) * dampingY;
            }
        }


    }

}