using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFactory {

        internal static Camera3DEntity CreateCamera3D(Camera3DContext ctx, Vector3 pos, Vector3 eulerRotation) {
            var id = ctx.IDService.PickCameraID();

            // 屏幕坐标系
            var camera = new Camera3DEntity();
            camera.SetID(id);
            camera.SetPos(pos);
            camera.SetDriverFollowPointOffset(pos);
            camera.SetEulerRotation(eulerRotation);
            return camera;
        }

    }

}