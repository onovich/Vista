using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DLookAtPhase {

        internal static void ApplyLookAtPerson(Camera3DContext ctx, int id, Camera agent, Transform person, float deltaTime) {
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

            // 构建只改变Yaw的新旋转，保留Pitch和Roll
            Quaternion targetWorldRot = Quaternion.Euler(currentEulerAngles.x, targetYaw, currentEulerAngles.z);

            // 使用Slerp进行平滑过渡
            float rotationDamping = 1 - camera.lookAtDampingFactor;
            Quaternion rot = Quaternion.Slerp(camera.rotation, targetWorldRot, rotationDamping);

            // 设置相机的新旋转
            TPCamera3DRotateDomain.SetRotation(ctx, id, rot);
        }

    }

}