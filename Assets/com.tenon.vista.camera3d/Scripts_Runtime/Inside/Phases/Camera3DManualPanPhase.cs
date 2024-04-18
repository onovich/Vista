using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DManualPanPhase {

        internal static void ApplyPan(Camera3DContext ctx, int id, Camera agent, Vector3 axis, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"PanDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            if (camera.fsmComponent.Status != TPCamera3DFSMStatus.ManualPanXYZ) {
                return;
            }

            var currentPos = camera.pos;
            var speed = camera.fsmComponent.manualPan_manualPanSpeed;
            var deltaDistance = new Vector3(axis.x * speed.x, axis.y * speed.y, axis.z * speed.z) * deltaTime;
            var pos = currentPos + deltaDistance;

            Camera3DMoveDomain.SetPos(ctx, id, agent, pos);
        }

    }

}