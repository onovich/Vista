using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DMoveDomain {

        internal static void SetPos(Camera3DContext ctx, int id, Camera agent, Vector3 cameraWorldPos) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.pos = cameraWorldPos;
            agent.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, cameraWorldPos.z);
        }

        internal static void ApplyFollowXYZ(Camera3DContext ctx, int id, Camera adgent, Transform person, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 cameraWorldPos = currentCamera.pos;
            Vector3 targetWorldPos = currentCamera.PersonWorldFollowPoint;

            // 将目标位置转换为基于角色的局部坐标系
            var driverRotation = person.rotation;
            var driverWorldPos = person.position;
            Vector3 targetLocalPos = Quaternion.Inverse(driverRotation) * (targetWorldPos - driverWorldPos);
            Vector3 cameraLocalPos = Quaternion.Inverse(driverRotation) * (cameraWorldPos - driverWorldPos);

            // 使用 Lerp 实现阻尼
            var dampingFactor = Vector3.one - currentCamera.followDampingFactor;
            cameraLocalPos.x = Mathf.Lerp(cameraLocalPos.x, targetLocalPos.x, dampingFactor.x);
            cameraLocalPos.y = Mathf.Lerp(cameraLocalPos.y, targetLocalPos.y, dampingFactor.y);
            cameraLocalPos.z = Mathf.Lerp(cameraLocalPos.z, targetLocalPos.z, dampingFactor.z);

            // 将修改后的局部坐标转换回全局坐标系
            cameraWorldPos = driverWorldPos + (driverRotation * cameraLocalPos);

            TPCamera3DMoveDomain.SetPos(ctx, id, adgent, cameraWorldPos);
            return;
        }

        internal static void ApplyFollowYZ(Camera3DContext ctx, int id, Camera agent, Transform person, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 cameraWorldPos = currentCamera.pos;
            Vector3 targetWorldPos = currentCamera.PersonWorldFollowPoint;

            // 将目标位置转换为基于角色的局部坐标系
            var driverRotation = person.rotation;
            var driverWorldPos = person.position;
            Vector3 targetLocalPos = Quaternion.Inverse(driverRotation) * (targetWorldPos - driverWorldPos);
            Vector3 cameraLocalPos = Quaternion.Inverse(driverRotation) * (cameraWorldPos - driverWorldPos);

            // 使用 Lerp 实现阻尼，仅修改局部 y 和 z 坐标
            var dampingFactor = Vector3.one - currentCamera.followDampingFactor;
            cameraLocalPos.y = Mathf.Lerp(cameraLocalPos.y, targetLocalPos.y, dampingFactor.y);
            cameraLocalPos.z = Mathf.Lerp(cameraLocalPos.z, targetLocalPos.z, dampingFactor.z);

            // 将修改后的局部坐标转换回全局坐标系
            cameraWorldPos = driverWorldPos + (driverRotation * cameraLocalPos);

            TPCamera3DMoveDomain.SetPos(ctx, id, agent, cameraWorldPos);
            return;
        }

    }

}