using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyAutoFollow(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, camera.personTRS, dt);
                return;
            }

            Vector3 cameraWorldPos = camera.personTRS.t;
            Vector3 targetPos = cameraWorldPos;

            ApplyWhenDisableDeadZone(ctx, camera, targetPos, dt);

        }

        static void ApplyWhenDisableDeadZone(Camera3DContext ctx, TPCamera3DModel camera, Vector3 targetPos, float dt) {
            TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, camera.personTRS, dt);
            Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, camera.personTRS, dt);
        }

    }

}