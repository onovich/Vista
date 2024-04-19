using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyFollowXYZ(Camera3DContext ctx, int id, Camera adgent, Transform person, float deltaTime) {
            TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, id, adgent, person, deltaTime);
            return;
        }

        internal static void ApplyFollowYZ(Camera3DContext ctx, int id, Camera agent, Transform person, float deltaTime) {
            TPCamera3DMoveDomain.ApplyFollowYZ(ctx, id, agent, person, deltaTime);
            return;
        }

    }

}