using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class TPCamera3DDeadZoneComponent {

        bool enable;
        internal bool IsEnable => enable;

        Vector2 deadZoneScreenMin;
        internal Vector2 DeadZoneScreenMin => deadZoneScreenMin;

        Vector2 deadZoneScreenMax;
        internal Vector2 DeadZoneScreenMax => deadZoneScreenMax;

        internal Vector2 Center => (deadZoneScreenMin + deadZoneScreenMax) / 2f;
        internal Vector2 Size => deadZoneScreenMax - deadZoneScreenMin;

        internal Vector2 LT => new Vector2(deadZoneScreenMin.x, deadZoneScreenMax.y);
        internal Vector2 RT => deadZoneScreenMax;
        internal Vector2 RB => new Vector2(deadZoneScreenMax.x, deadZoneScreenMin.y);
        internal Vector2 LB => deadZoneScreenMin;

        internal TPCamera3DDeadZoneComponent() {
            deadZoneScreenMin = Vector2.zero;
            deadZoneScreenMax = Vector2.zero;
            enable = false;
        }

        internal void Zone_Set(Vector2 deadZoneNormalizedSize, Vector2 viewSize) {
            Vector2 deadZoneSize;
            deadZoneSize.x = viewSize.x * deadZoneNormalizedSize.x;
            deadZoneSize.y = viewSize.y * deadZoneNormalizedSize.y;
            var screenCenter = viewSize / 2f;
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