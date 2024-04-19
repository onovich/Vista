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

            var speed = camera.fsmComponent.manualOrbital_manualOrbitalSpeed;

            var currentPos = camera.pos;
            var person = camera.person;

            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            } else {
                TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            }

            if (axis == Vector3.zero) {
                return;
            }

            // 投影 Person 到 xz 平面
            Vector3 projCenter = new Vector3(person.position.x, camera.pos.y, person.position.z);
            Vector3 localOffset = axis * speed * dt;
            Vector3 targetPos = currentPos + camera.rotation * localOffset;
            bool isClockWise = axis.x < 0;
            Vector3 pos = OrbitHelper.Round3D(currentPos, targetPos, projCenter, 1, 1, isClockWise);

            TPCamera3DMoveDomain.SetPos(ctx, id, agent, pos);
            TPCamera3DRotateDomain.ApplyLookAtPerson(ctx, id, agent, person, 1, dt);
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