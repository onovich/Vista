using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DComposerDomain {

        internal static void SetDampingFactor(Camera3DContext ctx, int id, float dampingFactor) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"SetDampingFactor Error, Camera Not Found: ID = {id}");
                return;
            }
            currentCamera.ComposerComponent.DampingFactor_Set(dampingFactor);
        }

    }

}