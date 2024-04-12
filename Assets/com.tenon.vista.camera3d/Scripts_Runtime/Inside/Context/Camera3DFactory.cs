using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFactory {

        internal static Camera3DEntity CreateCamera3D(Camera3DContext ctx, Vector3 pos, Vector3 eulerRotation, Vector3 confinerWorldMax, Vector3 confinerWorldMin) {
            var id = ctx.IDService.PickCameraID();

            // 世界坐标系
            var confiner = new Bounds(confinerWorldMin, confinerWorldMax);

            // 屏幕坐标系
            var camera = new Camera3DEntity();
            camera.SetID(id);
            camera.SetPos(pos);
            camera.SetDriverFollowPointOffset(pos);
            camera.SetEulerRotation(eulerRotation);
            camera.SetConfiner(confinerWorldMax, confinerWorldMin);
            return camera;
        }

    }

}