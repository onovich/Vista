using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DFSMController {

        internal static void TickFSM(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

            TickFSM_Any(ctx, camera, dt);

            TPCamera3DFSMStatus status = camera.fsmComponent.Status;
            if (status == TPCamera3DFSMStatus.DoNothing) {
                TickFSM_DoNothing(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.FollowXYZ) {
                TickFSM_FollowXYZ(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.FollowYZAndOrbitalZ) {
                FixedTickFSM_FollowYZAndOrbitalZ(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.ManualPanXYZ) {
                FixedTickFSM_PanXYZ(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.FollowXYZAndManualOrbitalXZ) {
                FixedTickFSM_FollowXYZAndOrbitalXZ(ctx, camera, dt);
            } else {
                V3Log.Error($"TPCamera3DFSMController.TickFSM: unknown status: {status}");
            }

        }

        static void TickFSM_Any(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
        }

        static void TickFSM_DoNothing(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

        }

        static void TickFSM_FollowXYZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            Camera3DFollowPhase.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
        }

        static void FixedTickFSM_FollowYZAndOrbitalZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            Camera3DFollowPhase.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
        }

        static void FixedTickFSM_PanXYZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

        }

        static void FixedTickFSM_FollowXYZAndOrbitalXZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

        }

    }

}