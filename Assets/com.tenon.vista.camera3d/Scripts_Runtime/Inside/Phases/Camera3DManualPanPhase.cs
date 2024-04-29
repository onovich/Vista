using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DManualPanPhase {

        internal static void ApplyPan(Camera3DContext ctx, int id, Vector3 axis, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"PanDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            if (camera.fsmCom.Status != TPCamera3DFSMStatus.ManualPan) {
                return;
            }

            var currentPos = camera.trs.t;
            var speed = camera.fsmCom.manualPan_manualPanSpeed;
            var deltaDistance = new Vector3(axis.x * speed.x, axis.y * speed.y, axis.z * speed.z) * deltaTime;
            var pos = currentPos + deltaDistance;

            TPCamera3DMoveDomain.SetPos(ctx, id, pos);
        }

        internal static void ApplyRecentering(Camera3DContext ctx, int id, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"PanDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            var start = camera.fsmCom.manualPan_recenterPanStartPos;
            var end = camera.GetPersonWorldFollowPoint();
            var duration = camera.fsmCom.manualPan_recenterPanDuration;
            var current = camera.fsmCom.manualPan_recenterPanCurrent;
            var mode = camera.fsmCom.manualPan_recenterPanEasingMode;
            var type = camera.fsmCom.manualPan_recenterPanEasingType;

            if (current >= duration) {
                camera.fsmCom.ManualPan_Exit();
                return;
            }

            var pos = EasingHelper.Easing3D(start, end, current, duration, type, mode);
            camera.fsmCom.ManualPan_IncRecenterTimer(dt);
            TPCamera3DMoveDomain.SetPos(ctx, camera.id, pos);
        }

    }

}