using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal class Camera2DConfinerComponent {

        Vector2 confinerWorldMin;
        internal Vector2 ConfinerWorldMin => confinerWorldMin;

        Vector2 confinerWorldMax;
        internal Vector2 ConfinerWorldMax => confinerWorldMax;

        internal Camera2DConfinerComponent(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerWorldMin = confinerWorldMin;
            this.confinerWorldMax = confinerWorldMax;
        }

        internal bool TryClamp(Vector2 src, float orthographicSize, float aspect, out Vector2 dst) {

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

            dst = new Vector2(x, y);
            return true;

        }

    }

}