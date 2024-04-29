using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class TPCamera3DMoveDomain {

        internal static void SetPos(Camera3DContext ctx, int id, Vector3 cameraWorldPoint) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.trs.t = cameraWorldPoint;
        }

        internal static void ApplyFollowXYZ(Camera3DContext ctx, int id, in TRS3DModel personTRS, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 cameraWorldPoint = currentCamera.trs.t;
            Vector3 targetWorldPoint = currentCamera.GetPersonWorldFollowPoint();

            // 将目标位置转换为基于角色的局部坐标系
            var driverRotation = personTRS.r;
            var driverWorldPoint = personTRS.t;
            Vector3 targetLocalPoint = Quaternion.Inverse(driverRotation) * (targetWorldPoint - driverWorldPoint);
            Vector3 cameraLocalPoint = Quaternion.Inverse(driverRotation) * (cameraWorldPoint - driverWorldPoint);

            // 使用 Lerp 实现阻尼
            var dampingFactor = Vector3.one - currentCamera.followDampingFactor;
            cameraLocalPoint.x = Mathf.Lerp(cameraLocalPoint.x, targetLocalPoint.x, dampingFactor.x);
            cameraLocalPoint.y = Mathf.Lerp(cameraLocalPoint.y, targetLocalPoint.y, dampingFactor.y);
            cameraLocalPoint.z = Mathf.Lerp(cameraLocalPoint.z, targetLocalPoint.z, dampingFactor.z);

            // 将修改后的局部坐标转换回全局坐标系
            cameraWorldPoint = driverWorldPoint + (driverRotation * cameraLocalPoint);

            TPCamera3DMoveDomain.SetPos(ctx, id, cameraWorldPoint);
            return;
        }

        internal static void ApplyFollowYZ(Camera3DContext ctx, int id, in TRS3DModel person, float deltaTime) {
            var has = ctx.TryGetTPCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }

            Vector3 cameraWorldPoint = currentCamera.trs.t;
            Vector3 targetWorldPoint = currentCamera.GetPersonWorldFollowPoint();

            // 将目标位置转换为基于角色的局部坐标系
            var driverRotation = person.r;
            var driverWorldPoint = person.t;
            Vector3 targetLocalPoint = Quaternion.Inverse(driverRotation) * (targetWorldPoint - driverWorldPoint);
            Vector3 cameraLocalPoint = Quaternion.Inverse(driverRotation) * (cameraWorldPoint - driverWorldPoint);

            // 使用 Lerp 实现阻尼，仅修改局部 y 和 z 坐标
            var dampingFactor = Vector3.one - currentCamera.followDampingFactor;
            cameraLocalPoint.y = Mathf.Lerp(cameraLocalPoint.y, targetLocalPoint.y, dampingFactor.y);
            cameraLocalPoint.z = Mathf.Lerp(cameraLocalPoint.z, targetLocalPoint.z, dampingFactor.z);

            // 将修改后的局部坐标转换回全局坐标系
            cameraWorldPoint = driverWorldPoint + (driverRotation * cameraLocalPoint);

            SetPos(ctx, id, cameraWorldPoint);
            return;
        }

    }

}