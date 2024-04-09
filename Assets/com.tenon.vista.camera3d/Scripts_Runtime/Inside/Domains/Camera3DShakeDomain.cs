using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DShakeDomain {

        internal static void ShakeOnce(Camera3DContext ctx, int id, float frequency, float amplitude, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"Shake Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.ShakeComponent.ShakeOnce(frequency, amplitude, duration, easingType, easingMode);
        }

    }

}