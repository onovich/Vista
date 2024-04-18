using MortiseFrame.Swing;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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
                TickFSM_FollowYZAndOrbitalZ(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.ManualPanXYZ) {
                TickFSM_PanXYZ(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.FollowXYZAndManualOrbitalXZ) {
                TickFSM_FollowXYZAndOrbitalXZ(ctx, camera, dt);
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

        static void TickFSM_FollowYZAndOrbitalZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            Camera3DFollowPhase.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
        }

        static void TickFSM_PanXYZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

            if (!camera.fsmComponent.manualPan_isRecenteringPan) {
                return;
            }

            var start = camera.fsmComponent.manualPan_recenterPanStartPos;
            var end = camera.fsmComponent.manualPan_originPos;
            var duration = camera.fsmComponent.manualPan_recenterPanDuration;
            var current = camera.fsmComponent.manualPan_recenterPanCurrent;
            var mode = camera.fsmComponent.manualPan_recenterPanEasingMode;
            var type = camera.fsmComponent.manualPan_recenterPanEasingType;

            if (current >= duration) {
                camera.fsmComponent.ManualPanXYZ_Exit();
                return;
            }

            var pos = EasingHelper.Easing3D(start, end, current, duration, type, mode);
            camera.fsmComponent.ManualPanXYZ_IncRecenterTimer(dt);
            TPCamera3DMoveDomain.SetPos(ctx, camera.id, ctx.cameraAgent, pos);
        }

        static void TickFSM_FollowXYZAndOrbitalXZ(Camera3DContext ctx, TPCamera3DModel camera, float dt) {

        }

    }

}