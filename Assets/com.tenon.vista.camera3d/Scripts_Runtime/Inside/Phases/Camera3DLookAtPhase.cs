using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DLookAtPhase {

        internal static void ApplyLookAtPerson(Camera3DContext ctx, int id, Camera agent, Transform person, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"LookAtDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            float rotationDamping = 1 - camera.lookAtDampingFactor;
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, id, agent, person, rotationDamping, deltaTime);
        }

    }

}