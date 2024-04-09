using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DRotateDomain {

        //  Rotate
        internal static void Rotate(Camera3DContext ctx, int id, float yaw, float pitch, float roll) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.Rotate(yaw, pitch, roll);
        }

    }

}