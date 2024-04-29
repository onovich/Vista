using MortiseFrame.Swing;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DFSMController {

        internal static void TickFSM(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {

            TickFSM_Any(ctx, camera, dt);

            TPCamera3DFSMStatus status = camera.fsmCom.Status;
            if (status == TPCamera3DFSMStatus.DoNothing) {
                TickFSM_DoNothing(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.AutoFollow) {
                TickFSM_AutoFollow(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.ManualPan) {
                TickFSM_ManualPan(ctx, camera, dt);
            } else if (status == TPCamera3DFSMStatus.ManualOrbital) {
                TickFSM_ManualOrbital(ctx, camera, dt);
            } else {
                V3Log.Error($"TPCamera3DFSMController.TickFSM: unknown status: {status}");
            }

        }

        static void TickFSM_Any(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {
        }

        static void TickFSM_DoNothing(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {

        }

        static void TickFSM_AutoFollow(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {
            Camera3DFollowPhase.ApplyAutoFollow(ctx, camera, dt);
            Camera3DLookAtPhase.ApplyAutoLookAtPerson(ctx, camera, dt);
        }

        static void TickFSM_ManualPan(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {
            if (!camera.fsmCom.manualPan_isRecentering) {
                Camera3DManualPanPhase.ApplyPan(ctx, camera.id, camera.inputCom.manualPanAxis, dt);
                return;
            }

            Camera3DManualPanPhase.ApplyRecentering(ctx, camera.id, dt);
        }

        static void TickFSM_ManualOrbital(Camera3DContext ctx, TPCamera3DEntity camera, float dt) {
            if (!camera.fsmCom.manualOrbital_isRecentering) {
                Camera3DManualOrbitalPhase.ApplyOrbital(ctx, camera.id, camera.inputCom.manualOrbitalAxis, dt);
                return;
            }

            Camera3DManualOrbitalPhase.ApplyRecentering(ctx, camera.id, dt);
        }

    }

}