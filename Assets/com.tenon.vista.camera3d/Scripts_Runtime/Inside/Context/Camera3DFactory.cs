using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFactory {

        internal static TPCamera3DModel CreateTPCamera(Camera3DContext ctx, Vector3 pos, Vector3 offset, Quaternion rot, float fov, Transform person) {
            var id = ctx.idService.PickCameraID();

            // 屏幕坐标系
            var camera = new TPCamera3DModel();
            camera.id = id;
            camera.pos = pos;
            camera.personFollowPointLocalOffset = offset;
            camera.rotation = rot;
            camera.personLocalLookAtRotation = rot;
            camera.fov = fov;
            camera.person = person;
            return camera;
        }

    }

}