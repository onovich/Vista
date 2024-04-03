using UnityEngine;

namespace MortiseFrame.Vista {

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

        internal Vector2 ScreenDiff_Get(Vector2 screenPos) {
            Vector2 diff = Vector2.zero;

            if (screenPos.x < deadZoneScreenMin.x) {
                diff.x = screenPos.x - deadZoneScreenMin.x;
            }

            if (screenPos.x > deadZoneScreenMax.x) {
                diff.x = screenPos.x - deadZoneScreenMax.x;
            }

            if (screenPos.y < deadZoneScreenMin.y) {
                diff.y = screenPos.y - deadZoneScreenMin.y;
            }

            if (screenPos.y > deadZoneScreenMax.y) {
                diff.y = screenPos.y - deadZoneScreenMax.y;
            }

            return diff;
        }

    }

}