using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DManualOrbitalPhase {

        internal static void ApplyOrbital(Camera3DContext ctx, int id, Camera agent, Vector3 axis, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbitalDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            TPCamera3DOrbitalDomain.ApplyOrbital(ctx, id, agent, axis, dt);
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, id, agent, camera.person, 1, dt);
        }

        internal static void ApplyRecentering(Camera3DContext ctx, int id, float dt) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbitalDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            var startRot = camera.fsmComponent.manualOrbital_recenterOrbitalStartRot;
            var endRot = camera.PersonWorldLookAtRotation;
            var startPos = camera.fsmComponent.manualOrbital_recenterOrbitalStartPos;
            var endPos = camera.PersonWorldFollowPoint;
            var duration = camera.fsmComponent.manualOrbital_recenterOrbitalDuration;
            var current = camera.fsmComponent.manualOrbital_recenterOrbitalCurrent;
            var mode = camera.fsmComponent.manualOrbital_recenterOrbitalEasingMode;
            var type = camera.fsmComponent.manualOrbital_recenterOrbitalEasingType;

            if (current >= duration) {
                camera.fsmComponent.ManualOrbital_Exit();
                return;
            }

            var pos = EasingHelper.Easing3D(startPos, endPos, current, duration, type, mode);
            var rot = EasingHelper.SlerpEasing(startRot, endRot, current, duration, type, mode);
            camera.fsmComponent.ManualOrbital_IncRecenterTimer(dt);
            TPCamera3DMoveDomain.SetPos(ctx, camera.id, ctx.cameraAgent, pos);
            TPCamera3DRotateDomain.SetRotation(ctx, camera.id, rot);
        }

    }

}