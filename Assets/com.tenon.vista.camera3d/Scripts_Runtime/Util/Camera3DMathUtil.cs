using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public static class Camera3DMathUtil {

        public static Vector3 WorldToScreenPos(Camera camera, Vector3 worldPos) {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            return new Vector3(screenPos.x, screenPos.y);
        }

        public static Vector3 ScreenToWorldPos(Camera camera, Vector3 screenPos) {
            var worldPos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
            return new Vector3(worldPos.x, worldPos.y);
        }

        public static Vector3 ScreenToWorldSize(Camera camera, Vector3 screenSize, Vector3 viewSize) {
            var ppu = (camera.orthographicSize * 2) / viewSize.y;
            return screenSize * ppu;
        }

    }

}