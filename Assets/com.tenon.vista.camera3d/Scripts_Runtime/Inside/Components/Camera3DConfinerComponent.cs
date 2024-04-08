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

        internal bool TryClamp(Vector3 src, float orthographicSize, float aspect, out Vector3 dst) {

            float verticalExtents = orthographicSize;
            float horizontalExtents = orthographicSize * aspect;

            float minX = confinerWorldMin.x + horizontalExtents;
            float maxX = confinerWorldMax.x - horizontalExtents;
            float minY = confinerWorldMin.y + verticalExtents;
            float maxY = confinerWorldMax.y - verticalExtents;

            bool valid = maxX > minX;
            valid &= maxY > minY;
            if (!valid) {
                dst = src;
                return false;
            }

            float x = Mathf.Clamp(src.x, minX, maxX);
            float y = Mathf.Clamp(src.y, minY, maxY);

            dst = new Vector3(x, y);
            return true;

        }

    }

}