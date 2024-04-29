using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DBusiness {

        internal static void Tick(Camera3DContext ctx, float fixdt) {

            ctx.TPCamera_ForEach((camera) => {
                TPCamera3DFSMController.TickFSM(ctx, camera, fixdt);
                Camera3DShakePhase.Tick(ctx, camera, fixdt);
                camera.inputCom.Reset();
            });

        }

    }

}