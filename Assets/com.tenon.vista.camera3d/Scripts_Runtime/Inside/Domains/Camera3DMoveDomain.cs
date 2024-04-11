using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DMoveDomain {

        internal static void EasingToTarget(Camera3DContext ctx, int id, Vector3 startPos, Vector3 targetPos, float current, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            var pos = EasingHelper.Easing3D(startPos, targetPos, current, duration, easingType, easingMode);
            camera.SetPos(pos);
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        internal static void SetPos(Camera3DContext ctx, int id, Camera mainCamera, Vector3 cameraWorldPos) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.SetPos(cameraWorldPos);
            ctx.MainCamera.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, ctx.MainCamera.transform.position.z);
        }

    }

}