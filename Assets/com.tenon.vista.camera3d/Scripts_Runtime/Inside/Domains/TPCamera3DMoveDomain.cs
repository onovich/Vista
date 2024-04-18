using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DMoveDomain {

        internal static void SetPos(Camera3DContext ctx, int id, Camera agent, Vector3 cameraWorldPos) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.pos = cameraWorldPos;
            agent.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, cameraWorldPos.z);
        }

    }

}