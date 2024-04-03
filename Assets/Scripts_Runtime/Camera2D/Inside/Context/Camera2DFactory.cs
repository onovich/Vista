using UnityEngine;

namespace MortiseFrame.Vista {

    internal static class Camera2DFactory {

        internal static Camera2DEntity CreateCamera2D(Camera2DContext ctx, Vector2 pos, Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            var id = ctx.IDService.PickCameraID();

            // 世界坐标系
            var confiner = new Bounds(confinerWorldMin, confinerWorldMax);

            // 屏幕坐标系
            var screenSize = ctx.ViewSize;
            var camera = new Camera2DEntity();
            camera.SetID(id);
            camera.SetPos(pos);
            camera.SetConfiner(confinerWorldMax, confinerWorldMin);
            return camera;
        }

    }

}