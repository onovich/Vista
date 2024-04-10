using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DDollyTrackPhase {

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
            if (status == Camera3DMovingStatus.MovingByDollyTrack) {
                TickMovingByDollyTrack(ctx, dt);
                return;
            }

        }

        static void TickIdle(Camera3DContext ctx, float dt) {
            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            if (fsmCom.Idle_isEntering) {
                fsmCom.Idle_isEntering = false;
            }
        }

        static void TickMovingByDollyTrack(Camera3DContext ctx, float dt) {
            // TODO
        }

    }

}