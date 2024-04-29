using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DManualOrbitalPhase {

        internal static void ApplyOrbital(Camera3DContext ctx, int id, Vector3 axis, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbitalDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            TPCamera3DOrbitalDomain.ApplyOrbital(ctx, id, axis, dt);
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, id, camera.personTRS, 1, dt);
        }

        internal static void ApplyRecentering(Camera3DContext ctx, int id, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbitalDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            var startRot = camera.fsmCom.manualOrbital_recenterOrbitalStartRot;
            var endRot = camera.GetPersonWorldFollowRotation();
            var startPos = camera.fsmCom.manualOrbital_recenterOrbitalStartPos;
            var endPos = camera.GetPersonWorldFollowPoint();
            var duration = camera.fsmCom.manualOrbital_recenterOrbitalDuration;
            var current = camera.fsmCom.manualOrbital_recenterOrbitalCurrent;
            var mode = camera.fsmCom.manualOrbital_recenterOrbitalEasingMode;
            var type = camera.fsmCom.manualOrbital_recenterOrbitalEasingType;

            if (current >= duration) {
                camera.fsmCom.ManualOrbital_Exit();
                return;
            }

            var pos = EasingHelper.Easing3D(startPos, endPos, current, duration, type, mode);
            var rot = EasingHelper.SlerpEasing(startRot, endRot, current, duration, type, mode);
            camera.fsmCom.ManualOrbital_IncRecenterTimer(dt);
            TPCamera3DMoveDomain.SetPos(ctx, camera.id, pos);
            TPCamera3DRotateDomain.SetRotation(ctx, camera.id, rot);
        }

    }

}