using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    public static class Camera2DMathUtil {

        public static Vector2 WorldToScreenPoint(Camera camera, Vector3 worldPoint) {
            var screenPoint = camera.WorldToScreenPoint(worldPoint);
            return new Vector2(screenPoint.x, screenPoint.y);
        }

        public static Vector2 ScreenToWorldPoint(Camera camera, Vector2 screenPoint) {
            var worldPoint = camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0));
            return new Vector2(worldPoint.x, worldPoint.y);
        }

        public static Vector2 ScreenToWorldLength(Camera camera, Vector2 screenLength, Vector2 screenSize) {
            var ppu = (camera.orthographicSize * 2) / screenSize.y;
            return screenLength * ppu;
        }

    }

}