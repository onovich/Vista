using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DManualOrbitalPhase {

        internal static void ApplyOrbital(Camera3DContext ctx, int id, Camera agent, Vector3 axis, float deltaTime) {

            var has = ctx.TryGetTPCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbitalDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            // 获取当前相机的旋转的欧拉角
            Vector3 currentEulerAngles = camera.rotation.eulerAngles;

            // 只改变Yaw的新旋转，保留Pitch和Roll
            Quaternion targetWorldRot = Quaternion.Euler(currentEulerAngles.x, currentEulerAngles.y + axis.x, currentEulerAngles.z);

            // 使用Slerp进行平滑过渡
            float rotationDamping = 1 - camera.lookAtDampingFactor;
            Quaternion rot = Quaternion.Slerp(camera.rotation, targetWorldRot, rotationDamping);

            // 设置相机的新旋转
            TPCamera3DRotateDomain.SetRotation(ctx, id, rot);

            // 计算目标方向
            Vector3 directionToTarget = camera.rotation * Vector3.forward;

            // 计算目标位置
            Vector3 targetPosition = camera.pos + directionToTarget * axis.y;

            // 使用Slerp进行平滑过渡
            float positionDamping = 1 - camera.lookAtDampingFactor;
            Vector3 pos = Vector3.Slerp(camera.pos, targetPosition, positionDamping);

            // 设置相机的新位置
            TPCamera3DMoveDomain.SetPos(ctx, id, agent, pos);

        }

    }

}