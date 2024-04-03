using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DFactory {

        public static Camera2DEntity CreateCamera2D(Camera2DContext ctx, Vector2 pos, Vector2 confinerWorldMax, Vector2 confinerWorldMin, Vector2 deadZoneNormalizedSize) {
            var id = ctx.IDService.PickCameraID();

            // 世界坐标系
            var confiner = new Bounds(confinerWorldMin, confinerWorldMax);

            // 屏幕坐标系
            var screenSize = ctx.ViewSize;
            var camera = new Camera2DEntity(id, pos, confinerWorldMax, confinerWorldMin, deadZoneNormalizedSize, screenSize);
            return camera;
        }

    }

}