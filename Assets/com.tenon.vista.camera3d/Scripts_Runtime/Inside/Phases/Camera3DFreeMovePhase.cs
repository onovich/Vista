using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFreeMovePhase {

        internal static void FSMTick(Camera3DContext ctx, float dt) {

            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            var status = fsmCom.Status;

            if (current == null) {
                return;
            }

            if (!ctx.ConfinerIsVaild) {
                return;
            }

            if (status == Camera3DMovingStatus.Idle) {
                TickFreeMove(ctx, dt);
                return;
            }

        }

        static void TickFreeMove(Camera3DContext ctx, float dt) {
            // TODO
        }

    }

}