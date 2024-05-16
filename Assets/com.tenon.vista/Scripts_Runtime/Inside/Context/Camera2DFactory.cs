using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DFactory {

        internal static Camera2DEntity CreateCamera2D(Camera2DContext ctx,
                                                      Vector3 pos,
                                                      float rot,
                                                      float size,
                                                      float aspect,
                                                      Vector2 confinerWorldMax,
                                                      Vector2 confinerWorldMin,
                                                      Vector2 driverPos) {
            var id = ctx.IDService.PickCameraID();

            // 世界坐标系
            var confiner = new Bounds(confinerWorldMin, confinerWorldMax);

            // 屏幕坐标系
            var camera = new Camera2DEntity(pos, rot, size, aspect, driverPos);
            camera.SetID(id);
            camera.SetPos(pos);
            camera.SetConfiner(confinerWorldMax, confinerWorldMin);
            return camera;
        }

    }

}