using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DBusiness {

        internal static void Tick(Camera3DContext ctx, Vector3 personT, Quaternion personR, Vector3 personS, float fixdt) {

            ctx.TPCamera_ForEach((camera) => {
                camera.Person_SetTRS(personT, personR, personS);
                TPCamera3DFSMController.TickFSM(ctx, camera, fixdt);
                Camera3DShakePhase.Tick(ctx, camera, fixdt);
                camera.inputCom.Reset();
            });

        }

    }

}