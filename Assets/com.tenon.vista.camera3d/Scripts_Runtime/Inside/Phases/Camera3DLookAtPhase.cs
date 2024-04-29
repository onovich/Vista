using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DLookAtPhase {

        internal static void ApplyAutoLookAtPerson(Camera3DContext ctx, TPCamera3DEntity camera, float deltaTime) {
            if (camera.followX) {
                return;
            }

            float rotationDamping = 1 - camera.lookAtDampingFactor;
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, camera.id, camera.personTRS, rotationDamping, deltaTime);
        }

        internal static void ApplyLookAtPerson(Camera3DContext ctx, int id, TRS3DModel person, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"LookAtDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            float rotationDamping = 1 - camera.lookAtDampingFactor;
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, id, person, rotationDamping, deltaTime);
        }

    }

}