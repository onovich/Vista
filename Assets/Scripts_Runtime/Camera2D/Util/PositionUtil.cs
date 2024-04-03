using UnityEngine;

namespace MortiseFrame.Vista {
    public static class PositionUtil {

        public static Vector2 WorldToScreen(Camera camera, Vector3 worldPos) {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            return new Vector2(screenPos.x, screenPos.y);
        }

        public static Vector2 ScreenToWorld(Camera camera, Vector2 screenPos) {
            var worldPos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
            return new Vector2(worldPos.x, worldPos.y);
        }

        static float PixelToWorldUnit(Camera camera, int screenHeight) {
            return (camera.orthographicSize * 2) / screenHeight;
        }

        public static Vector2 ScreenToWorldSize(Camera camera, Vector2 screenSize) {
            float ppu = PixelToWorldUnit(camera, Screen.height);
            return screenSize * ppu;
        }

    }

}