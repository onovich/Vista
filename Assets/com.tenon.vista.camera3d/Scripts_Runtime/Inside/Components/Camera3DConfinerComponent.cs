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

        internal bool TryClamp(Vector3 src, float fov, float aspect, out Vector3 dst) {

            float verticalFOVHalf = fov * 0.5f;
            float verticalDistance = (confinerWorldMax.z - confinerWorldMin.z) / (2f * Mathf.Tan(verticalFOVHalf * Mathf.Deg2Rad));
            float horizontalDistance = verticalDistance * aspect;

            float minX = confinerWorldMin.x + horizontalDistance;
            float maxX = confinerWorldMax.x - horizontalDistance;
            float minY = confinerWorldMin.y + verticalDistance;
            float maxY = confinerWorldMax.y - verticalDistance;
            float minZ = confinerWorldMin.z;
            float maxZ = confinerWorldMax.z;

            bool valid = maxX > minX && maxY > minY;
            if (!valid) {
                dst = src;
                return false;
            }

            float x = Mathf.Clamp(src.x, minX, maxX);
            float y = Mathf.Clamp(src.y, minY, maxY);
            float z = Mathf.Clamp(src.z, minZ, maxZ);

            dst = new Vector3(x, y, z);
            return true;
        }

    }

}