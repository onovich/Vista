using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DTransposerDomain {

        internal static void SetDampingFactor(Camera3DContext ctx, int id, Vector3 dampingFactor) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"SetDampingFactor Error, Camera Not Found: ID = {id}");
                return;
            }
            currentCamera.TransposerComponent.DampingFactor_Set(dampingFactor);
        }

    }

}