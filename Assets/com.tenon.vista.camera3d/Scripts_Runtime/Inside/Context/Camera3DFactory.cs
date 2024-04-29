using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFactory {

        internal static TPCamera3DModel CreateTPCamera(Camera3DContext ctx, Vector3 t, Quaternion r, Vector3 s, float fov, float nearClip, float farClip, float aspectRatio) {
            var id = ctx.idService.PickCameraID();
            var camera = new TPCamera3DModel(id, t, r, s, fov, nearClip, farClip, aspectRatio);
            return camera;
        }

    }

}