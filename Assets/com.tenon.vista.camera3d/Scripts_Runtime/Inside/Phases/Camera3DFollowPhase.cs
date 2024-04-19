using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyAutoFollow(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }
            TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
        }

    }

}