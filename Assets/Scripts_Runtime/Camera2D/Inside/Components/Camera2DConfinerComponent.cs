using UnityEngine;

namespace MortiseFrame.Vista {

    internal class Camera2DConfinerComponent {

        Vector2 confinerWorldMin;
        internal Vector2 ConfinerWorldMin => confinerWorldMin;

        Vector2 confinerWorldMax;
        internal Vector2 ConfinerWorldMax => confinerWorldMax;

        internal Camera2DConfinerComponent(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerWorldMin = confinerWorldMin;
            this.confinerWorldMax = confinerWorldMax;
        }

        internal Vector2 Clamp(Vector2 pos, float orthographicSize, float aspect) {

            float verticalExtents = orthographicSize;
            float horizontalExtents = orthographicSize * aspect;

            float minX = confinerWorldMin.x + horizontalExtents;
            float maxX = confinerWorldMax.x - horizontalExtents;
            float minY = confinerWorldMin.y + verticalExtents;
            float maxY = confinerWorldMax.y - verticalExtents;

            float x = Mathf.Clamp(pos.x, minX, maxX);
            float y = Mathf.Clamp(pos.y, minY, maxY);

            return new Vector2(x, y);

        }

    }

}