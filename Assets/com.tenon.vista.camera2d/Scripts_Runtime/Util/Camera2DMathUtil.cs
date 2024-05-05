using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    public static class Camera2DMathUtil {

        internal static Vector2 WorldToScreenPoint(Camera2DEntity camera, Vector2 worldSpacePoint, Vector2 screenSize) {
            Vector2 cameraViewSize = CalculateCameraViewAreaSize(camera.Size, camera.Aspect);
            Vector2 relativePosition = worldSpacePoint - camera.Pos;
            float cos = Mathf.Cos(-camera.Rot * Mathf.Deg2Rad);
            float sin = Mathf.Sin(-camera.Rot * Mathf.Deg2Rad);
            Vector2 rotatedPosition = new Vector2(
                relativePosition.x * cos - relativePosition.y * sin,
                relativePosition.x * sin + relativePosition.y * cos
            );
            float scaleX = screenSize.x / cameraViewSize.x;
            float scaleY = screenSize.y / cameraViewSize.y;
            Vector3 screenPosition = new Vector2(
                rotatedPosition.x * scaleX + screenSize.x / 2,
                rotatedPosition.y * scaleY + screenSize.y / 2
            );

            return screenPosition;
        }

        static Vector2 CalculateCameraViewAreaSize(float orthographicSize, float aspect) {
            float height = orthographicSize * 2;
            float width = height * aspect;
            return new Vector2(width, height);
        }

        internal static Vector2 ScreenToWorldLength(Camera2DEntity camera, Vector2 screenLength, Vector2 screenSize) {
            Vector2 cameraViewSize = CalculateCameraViewAreaSize(camera.Size, camera.Aspect);
            float scaleX = cameraViewSize.x / screenSize.x;
            float scaleY = cameraViewSize.y / screenSize.y;

            Vector2 worldLength = new Vector2(
                screenLength.x * scaleX,
                screenLength.y * scaleY
            );

            return worldLength;
        }

        internal static Vector2 ScreenToWorldPoint(Camera2DEntity camera, Vector2 screenPoint, Vector2 screenSize) {
            Vector2 cameraViewSize = CalculateCameraViewAreaSize(camera.Size, camera.Aspect);
            Vector2 centeredScreen = new Vector2(
                screenPoint.x - screenSize.x / 2,
                screenPoint.y - screenSize.y / 2
            );
            float scaleX = cameraViewSize.x / screenSize.x;
            float scaleY = cameraViewSize.y / screenSize.y;

            Vector2 worldPos = new Vector2(
                centeredScreen.x * scaleX,
                centeredScreen.y * scaleY
            );
            float cos = Mathf.Cos(camera.Rot * Mathf.Deg2Rad);
            float sin = Mathf.Sin(camera.Rot * Mathf.Deg2Rad);
            Vector2 rotated = new Vector2(
                worldPos.x * cos + worldPos.y * sin,
                -worldPos.x * sin + worldPos.y * cos
            );

            Vector2 finalPosition = rotated + camera.Pos;
            return finalPosition;
        }

    }

}