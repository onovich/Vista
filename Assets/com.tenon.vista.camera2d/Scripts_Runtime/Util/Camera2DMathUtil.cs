using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    public static class Camera2DMathUtil {

        public static Vector2 WorldToScreenPos(Camera camera, Vector3 worldPos) {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            return new Vector2(screenPos.x, screenPos.y);
        }

        public static Vector2 ScreenToWorldPos(Camera camera, Vector2 screenPos) {
            var worldPos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
            return new Vector2(worldPos.x, worldPos.y);
        }

        public static Vector2 ScreenToWorldLength(Camera camera, Vector2 screenLength, Vector2 viewSize) {
            var ppu = (camera.orthographicSize * 2) / viewSize.y;
            return screenLength * ppu;
        }

    }

}