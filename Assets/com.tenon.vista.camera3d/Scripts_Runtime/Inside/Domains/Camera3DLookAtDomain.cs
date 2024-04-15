using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DLookAtDomain {

        internal static void LookAt(Camera3DContext ctx, int id, Vector3 targetPos) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"LookAt Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 direction = targetPos - camera.Pos;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Vector3 euler = targetRotation.eulerAngles;
            Camera3DRotateDomain.Rotate(ctx, id, euler.y, euler.x, euler.z);
        }

    }

}