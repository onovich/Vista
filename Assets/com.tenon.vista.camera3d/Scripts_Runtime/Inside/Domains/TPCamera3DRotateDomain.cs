using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DRotateDomain {

        internal static void SetRotationByEulerAngle(Camera3DContext ctx, int id, Vector3 eulerAngle) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.Rotation_SetByEulerAngle(eulerAngle);
            var agent = ctx.cameraAgent;
            agent.transform.rotation = camera.rotation;
        }

        internal static void SetRotation(Camera3DContext ctx, int id, Quaternion rotation) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.rotation = rotation;
            var agent = ctx.cameraAgent;
            agent.transform.rotation = camera.rotation;
        }

    }

}