using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DConfinerComponent {

        Vector3 confinerWorldMin;
        internal Vector3 ConfinerWorldMin => confinerWorldMin;

        Vector3 confinerWorldMax;
        internal Vector3 ConfinerWorldMax => confinerWorldMax;

        internal Camera3DConfinerComponent(Vector3 confinerWorldMax, Vector3 confinerWorldMin) {
            this.confinerWorldMin = confinerWorldMin;
            this.confinerWorldMax = confinerWorldMax;
        }

        internal bool TryClamp(Camera camera, Vector3 src, float fov, float aspect, out Vector3 dst) {

            Matrix4x4 projectionMatrix = camera.projectionMatrix;
            Matrix4x4 viewMatrix = camera.worldToCameraMatrix;
            Matrix4x4 inverseMVP = (projectionMatrix * viewMatrix).inverse;

            // 生成边界点在NDC空间中的位置
            Vector3[] ndcBounds = new Vector3[] {
                new Vector3(-1, -1, 0), // 左下
                new Vector3(1, 1, 0), // 右上
                new Vector3(-1, 1, 0), // 左上
                new Vector3(1, -1, 0) // 右下
            };

            bool allInside = true;
            foreach (var ndc in ndcBounds) {
                Vector3 worldPoint = inverseMVP.MultiplyPoint(ndc);
                if (worldPoint.x < confinerWorldMin.x || worldPoint.x > confinerWorldMax.x ||
                    worldPoint.y < confinerWorldMin.y || worldPoint.y > confinerWorldMax.y ||
                    worldPoint.z < confinerWorldMin.z || worldPoint.z > confinerWorldMax.z) {
                    allInside = false;
                    break;
                }
            }

            if (!allInside) {
                Debug.LogError("Camera position is out of bounds!");
                dst = src; // Or adjust to a valid position
                return false;
            }

            dst = src; // If everything is fine, return the src
            return true;

        }

    }

}