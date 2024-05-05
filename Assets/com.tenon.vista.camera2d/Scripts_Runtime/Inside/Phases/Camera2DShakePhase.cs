using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DShakePhase {

        internal static Vector2 TickShakeOffset(Camera2DContext ctx, float dt) {
            var currentCamera = ctx.CurrentCamera;
            if (currentCamera == null) {
                return Vector2.zero;
            }
            var shakeCom = currentCamera.ShakeComponent;
            if (shakeCom == null) {
                return Vector2.zero;
            }
            if (shakeCom.Current >= shakeCom.Duration) {
                return Vector2.zero;
            }
            var offset = shakeCom.GetOffset();
            shakeCom.IncCurrent(dt);
            return offset;
        }

    }

}