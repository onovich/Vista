using UnityEngine;

namespace MortiseFrame.Vista {

    public class Camera2DConfinerComponent {

        Vector2 confinerWorldMin;
        public Vector2 ConfinerWorldMin => confinerWorldMin;

        Vector2 confinerWorldMax;
        public Vector2 ConfinerWorldMax => confinerWorldMax;

        public Camera2DConfinerComponent(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerWorldMin = confinerWorldMin;
            this.confinerWorldMax = confinerWorldMax;
        }

        public Vector2 Clamp(Vector2 pos, float orthographicSize, float aspect) {

            // 计算相机的正交视图尺寸
            float verticalExtents = orthographicSize;
            float horizontalExtents = orthographicSize * aspect;

            // 根据相机视图尺寸调整限制逻辑
            float minX = confinerWorldMin.x + horizontalExtents;
            float maxX = confinerWorldMax.x - horizontalExtents;
            float minY = confinerWorldMin.y + verticalExtents;
            float maxY = confinerWorldMax.y - verticalExtents;

            // 使用调整后的限制进行坐标夹紧
            float x = Mathf.Clamp(pos.x, minX, maxX);
            float y = Mathf.Clamp(pos.y, minY, maxY);

            return new Vector2(x, y);

        }

    }

}