using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowDomain {

        internal static void SetDriver(Camera3DContext ctx, int id, Transform driver) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetMoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetDriver(driver);
        }

    }

}