using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DRotateDomain {

        internal static void Rotate(Camera3DContext ctx, int id, float yaw, float pitch, float roll) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.Rotate(yaw, pitch, roll);
            var mainCamera = ctx.MainCamera;
            mainCamera.transform.rotation = camera.Rotation;
        }

        internal static void SetRotation(Camera3DContext ctx, int id, Quaternion rotation) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetRotation(rotation);
            var mainCamera = ctx.MainCamera;
            mainCamera.transform.rotation = camera.Rotation;
        }

    }

}