using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyAutoFollow(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {
            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, in camera.personTRS, dt);
                return;
            }

            TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, camera.personTRS, dt);
        }

    }

}