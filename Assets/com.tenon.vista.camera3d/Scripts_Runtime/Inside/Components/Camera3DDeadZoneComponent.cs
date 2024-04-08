using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DDeadZoneComponent {

        bool enable;
        internal bool Enable => enable;

        Vector3 deadZoneScreenMin;
        internal Vector3 DeadZoneScreenMin => deadZoneScreenMin;

        Vector3 deadZoneScreenMax;
        internal Vector3 DeadZoneScreenMax => deadZoneScreenMax;

        internal Camera3DDeadZoneComponent() {
            deadZoneScreenMin = Vector3.zero;
            deadZoneScreenMax = Vector3.zero;
            enable = false;
        }

        internal void Zone_Set(Vector3 deadZoneNormalizedSize, Vector3 screenSize) {
            var deadZoneSize = new Vector3(screenSize.x * deadZoneNormalizedSize.x, screenSize.y * deadZoneNormalizedSize.y);
            var screenCenter = screenSize / 2f;
            var deadZoneHalfSize = deadZoneSize / 2f;
            deadZoneScreenMin = screenCenter - deadZoneHalfSize;
            deadZoneScreenMax = screenCenter + deadZoneHalfSize;
            enable = true;
        }

        internal void Enable_Set(bool enable) {
            this.enable = enable;
        }

        internal Vector3 ScreenDiff_Get(Vector3 screenPos) {
            Vector3 diff = Vector3.zero;

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