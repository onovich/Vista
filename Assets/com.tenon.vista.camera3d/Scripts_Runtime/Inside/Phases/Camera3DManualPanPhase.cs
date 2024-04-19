using MortiseFrame.Swing;
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

            TPCamera3DMoveDomain.SetPos(ctx, id, agent, pos);
        }

        internal static void ApplyRecentering(Camera3DContext ctx, int id, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"PanDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            var start = camera.fsmComponent.manualPan_recenterPanStartPos;
            var end = camera.pos;
            var duration = camera.fsmComponent.manualPan_recenterPanDuration;
            var current = camera.fsmComponent.manualPan_recenterPanCurrent;
            var mode = camera.fsmComponent.manualPan_recenterPanEasingMode;
            var type = camera.fsmComponent.manualPan_recenterPanEasingType;

            if (current >= duration) {
                camera.fsmComponent.ManualPanXYZ_Exit();
                return;
            }

            var pos = EasingHelper.Easing3D(start, end, current, duration, type, mode);
            camera.fsmComponent.ManualPanXYZ_IncRecenterTimer(dt);
            TPCamera3DMoveDomain.SetPos(ctx, camera.id, ctx.cameraAgent, pos);
        }

    }

}