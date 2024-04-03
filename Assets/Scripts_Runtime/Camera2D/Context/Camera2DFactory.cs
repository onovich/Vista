using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DFactory {

        public static Camera2DEntity CreateCamera2D(Camera2DContext ctx, Vector2 pos, Vector2 confinerSize, Vector2 confinerPos, Vector2 deadZoneNormalizedSize) {
            var id = ctx.IDService.PickCameraID();

            // 世界坐标系
            var confiner = new Bounds(confinerPos, confinerSize);

            // 屏幕坐标系
            var screenSize = ctx.ViewSize;
            var camera = new Camera2DEntity(id, pos, confiner, deadZoneNormalizedSize, screenSize);
            return camera;
        }

    }

}