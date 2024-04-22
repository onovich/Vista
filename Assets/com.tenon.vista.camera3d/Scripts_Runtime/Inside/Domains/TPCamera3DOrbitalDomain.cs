using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DOrbitalDomain {

        public static void ApplyOrbital(Camera3DContext ctx, int id, Camera agent, Vector3 axis, float dt) {
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
        }

    }

}