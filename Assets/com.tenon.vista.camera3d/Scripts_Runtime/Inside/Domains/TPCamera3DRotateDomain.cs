using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DRotateDomain {

        internal static void SetRotationByEulerAngle(Camera3DContext ctx, int id, Vector3 eulerAngle) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.Rotation_SetByEulerAngle(eulerAngle);
            var agent = ctx.cameraAgent;
            agent.transform.rotation = camera.rotation;
        }

        internal static void SetRotation(Camera3DContext ctx, int id, Quaternion rotation) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.rotation = rotation;
            var agent = ctx.cameraAgent;
            agent.transform.rotation = camera.rotation;
        }

        internal static void ApplyLookAtPerson(Camera3DContext ctx, int id, Camera agent, Transform person, float rotationDamping, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"LookAtDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 targetPosition = person.position;
            Vector3 currentPosition = agent.transform.position;

            // 计算目标方向
            Vector3 directionToTarget = (targetPosition - currentPosition).normalized;

            // 计算目标Yaw值
            float targetYaw = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;

            // 获取当前相机的旋转的欧拉角
            Vector3 currentEulerAngles = camera.rotation.eulerAngles;

            // 只改变Yaw的新旋转，保留Pitch和Roll
            Quaternion targetWorldRot = Quaternion.Euler(currentEulerAngles.x, targetYaw, currentEulerAngles.z);

            // 使用Slerp进行平滑过渡
            Quaternion rot = Quaternion.Slerp(camera.rotation, targetWorldRot, rotationDamping);

            // 设置相机的新旋转
            SetRotation(ctx, id, rot);
        }

    }

}