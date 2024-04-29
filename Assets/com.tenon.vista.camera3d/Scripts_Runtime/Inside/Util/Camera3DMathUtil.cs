using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public static class Camera3DMathUtil {

        public static Vector2 WorldToScreenPos(Camera camera, Vector3 worldPos) {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            return new Vector2(screenPos.x, screenPos.y);
        }

        public static Vector3 ScreenToWorldPos(Camera camera, Vector2 screenPos) {
            var worldPos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
            return new Vector3(worldPos.x, worldPos.y, worldPos.z);
        }

        public static float GetDepth(Camera camera, Vector3 fromCameraToObject) {
            return Vector3.Dot(fromCameraToObject.normalized, camera.transform.forward);
        }

        public static Vector3 ScreenToWorldSize(Camera camera, Vector2 screenLength, float depth) {
            // 确保深度值在相机的裁剪平面之间
            if (depth < camera.nearClipPlane || depth > camera.farClipPlane) {
                Debug.LogWarning("Depth Value Is Out Of The Camera's Clipping Planes");
                return Vector3.zero;
            }

            // 获取从相机到指定深度处的一个点的世界坐标
            Vector3 pointInWorldAtDepth = camera.ScreenToWorldPoint(new Vector3(screenLength.x / 2, screenLength.y / 2, depth));

            // 获取屏幕中心点在指定深度的世界坐标，以便计算尺寸
            Vector3 centerInWorldAtDepth = camera.ScreenToWorldPoint(new Vector3(0, 0, depth));

            // 计算世界尺寸
            Vector3 worldSize = (pointInWorldAtDepth - centerInWorldAtDepth) * 2;
            return new Vector3(Mathf.Abs(worldSize.x), Mathf.Abs(worldSize.y), 0);
        }

    }

}