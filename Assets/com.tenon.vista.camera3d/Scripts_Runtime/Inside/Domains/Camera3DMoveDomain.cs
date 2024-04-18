using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DMoveDomain {

        internal static void SetPos(Camera3DContext ctx, int id, Camera mainCamera, Vector3 cameraWorldPos) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.pos = cameraWorldPos;
            ctx.cameraAgent.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, cameraWorldPos.z);
        }

    }

}