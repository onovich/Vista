using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal class Camera2DDeadZoneComponent {

        bool enable;
        internal bool Enable => enable;

        Vector2 deadZoneScreenMin;
        internal Vector2 DeadZoneScreenMin => deadZoneScreenMin;

        Vector2 deadZoneScreenMax;
        internal Vector2 DeadZoneScreenMax => deadZoneScreenMax;

        internal Camera2DDeadZoneComponent() {
            deadZoneScreenMin = Vector2.zero;
            deadZoneScreenMax = Vector2.zero;
            enable = false;
        }

        internal Vector2 LB => deadZoneScreenMin;
        internal Vector2 RT => deadZoneScreenMax;
        internal Vector2 LT => new Vector2(deadZoneScreenMin.x, deadZoneScreenMax.y);
        internal Vector2 RB => new Vector2(deadZoneScreenMax.x, deadZoneScreenMin.y);

        internal void Zone_Set(Vector2 deadZoneNormalizedSize, Vector2 screenSize) {
            var deadZoneSize = new Vector2(screenSize.x * deadZoneNormalizedSize.x, screenSize.y * deadZoneNormalizedSize.y);
            var screenCenter = screenSize / 2f;
            var deadZoneHalfSize = deadZoneSize / 2f;
            deadZoneScreenMin = screenCenter - deadZoneHalfSize;
            deadZoneScreenMax = screenCenter + deadZoneHalfSize;
            enable = true;
        }

        internal void Enable_Set(bool enable) {
            this.enable = enable;
        }

        internal Vector2 ScreenDiff_Get(Vector2 screenPoint) {
            Vector2 diff = Vector2.zero;

            if (screenPoint.x < deadZoneScreenMin.x) {
                diff.x = screenPoint.x - deadZoneScreenMin.x;
            }

            if (screenPoint.x > deadZoneScreenMax.x) {
                diff.x = screenPoint.x - deadZoneScreenMax.x;
            }

            if (screenPoint.y < deadZoneScreenMin.y) {
                diff.y = screenPoint.y - deadZoneScreenMin.y;
            }

            if (screenPoint.y > deadZoneScreenMax.y) {
                diff.y = screenPoint.y - deadZoneScreenMax.y;
            }

            return diff;
        }

    }

}