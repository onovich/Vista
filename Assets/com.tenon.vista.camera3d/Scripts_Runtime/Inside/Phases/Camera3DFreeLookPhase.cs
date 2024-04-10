using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFreeLookPhase {

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
                TickFreeLook(ctx, dt);
                return;
            }

        }

        static void TickFreeLook(Camera3DContext ctx, float dt) {
            // TODO
        }

    }

}