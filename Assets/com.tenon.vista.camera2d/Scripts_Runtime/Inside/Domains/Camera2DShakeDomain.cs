using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DShakeDomain {

        internal static void ShakeOnce(Camera2DContext ctx, int id, float frequency, float amplitude, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"Shake Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.ShakeComponent.ShakeOnce(frequency, amplitude, duration, easingType, easingMode);
        }

    }

}