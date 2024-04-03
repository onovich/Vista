using UnityEngine;

namespace MortiseFrame.Vista {

    public class Camera2DDeadZoneComponent {

        Vector2 deadZoneScreenMin;
        public Vector2 DeadZoneScreenMin => deadZoneScreenMin;

        Vector2 deadZoneScreenMax;
        public Vector2 DeadZoneScreenMax => deadZoneScreenMax;

        public Camera2DDeadZoneComponent(Vector2 deadZoneNormalizedSize, Vector2 screenSize) {
            var deadZoneSize = new Vector2(screenSize.x * deadZoneNormalizedSize.x, screenSize.y * deadZoneNormalizedSize.y);
            var screenCenter = screenSize / 2f;
            var deadZoneHalfSize = deadZoneSize / 2f;
            deadZoneScreenMin = screenCenter - deadZoneHalfSize;
            deadZoneScreenMax = screenCenter + deadZoneHalfSize;
        }

        public Vector2 GetScreenDiff(Vector2 screenPos) {
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