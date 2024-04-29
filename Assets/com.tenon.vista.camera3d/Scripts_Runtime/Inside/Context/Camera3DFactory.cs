using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFactory {

        internal static TPCamera3DEntity CreateTPCamera(Camera3DContext ctx, Vector3 t, Quaternion r, Vector3 s, float fov, float nearClip, float farClip, float aspectRatio, float screenWidth) {
            var id = ctx.idService.PickCameraID();
            var camera = new TPCamera3DEntity(id, t, r, s, fov, nearClip, farClip, aspectRatio, screenWidth);
            return camera;
        }

    }

}