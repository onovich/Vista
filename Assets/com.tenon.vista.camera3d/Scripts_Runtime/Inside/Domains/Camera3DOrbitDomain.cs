using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DOrbitDomain {

        internal static void OrbitAroundTarget(Camera3DContext ctx, int id, Transform target, Vector3 axisInput, float rotationSpeed = 1.0f) {
            if (target == null) return;

            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"OrbitAroundTarget Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 direction = (camera.Pos - target.position).normalized;
            float distance = Vector3.Distance(camera.Pos, target.position);

            Quaternion yawRotation = Quaternion.AngleAxis(axisInput.y * rotationSpeed, Vector3.up);
            Quaternion pitchRotation = Quaternion.AngleAxis(axisInput.x * rotationSpeed, Vector3.right);
            Quaternion totalRotation = yawRotation * pitchRotation;

            Vector3 newPosition = target.position + totalRotation * direction * distance;

            Camera3DMoveDomain.SetPos(ctx, id, ctx.MainCamera, newPosition);
            Camera3DLookAtDomain.LookAt(ctx, id, target);
        }

    }

}