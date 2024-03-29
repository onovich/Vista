using UnityEngine;

namespace MortiseFrame.Vista {
    public static class PositionUtil {

        public static Vector2 WorldToScreenPos(Camera camera, Vector3 worldPos) {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            return new Vector2(screenPos.x, screenPos.y);
        }

    }

}