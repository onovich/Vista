using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DShakePhase {

        internal static void Tick(Camera3DContext ctx, ICamera3D camera, float dt) {

            var shakeCom = camera.ShakeCom;
            if (shakeCom == null) {
                return;
            }
            if (shakeCom.Current >= shakeCom.Duration) {
                return;
            }
            ApplyShakeOffset(ctx, shakeCom, dt);
        }

        static Vector3 ApplyShakeOffset(Camera3DContext ctx, Camera3DShakeComponent shakeCom, float dt) {
            var offset = shakeCom.GetOffset();
            shakeCom.IncCurrent(dt);
            return offset;
        }

    }

}