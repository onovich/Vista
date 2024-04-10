using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DDeadZoneModel {

        bool enable;
        internal bool Enable => enable;

        Vector3 deadZoneScreenMin;
        internal Vector3 DeadZoneScreenMin => deadZoneScreenMin;

        Vector3 deadZoneScreenMax;
        internal Vector3 DeadZoneScreenMax => deadZoneScreenMax;

        internal Camera3DDeadZoneModel() {
            deadZoneScreenMin = Vector2.zero;
            deadZoneScreenMax = Vector2.zero;
            enable = false;
        }

        internal void Zone_Set(Vector2 deadZoneNormalizedSize, Vector3 viewSize) {
            Vector3 deadZoneSize;
            deadZoneSize.x = viewSize.x * deadZoneNormalizedSize.x;
            deadZoneSize.z = viewSize.y * deadZoneNormalizedSize.y;
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