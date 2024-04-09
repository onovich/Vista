using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DMoveDomain {

        internal static void MoveToTarget(Camera2DContext ctx, int id, Vector2 startPos, Vector2 targetPos, float current, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            var pos = EasingHelper.Easing2D(startPos, targetPos, current, duration, easingType, easingMode);
            camera.SetPos(pos);
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        internal static void MoveByDriver(Camera2DContext ctx, int id, Camera mainCamera, Vector2 driverWorldPos, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V2Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            bool deadZoneEnable = currentCamera.IsDeadZoneEnable();
            bool softZoneEnable = currentCamera.IsSoftZoneEnable();
            Vector2 cameraWorldPos = currentCamera.Pos;
            Vector2 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = driverWorldPos;
                RefreshCameraPos(ctx, id, mainCamera, targetPos);
                return;
            }

            var driverScreenPos = Camera2DMathUtil.WorldToScreenPos(mainCamera, driverWorldPos);
            var deadZoneDiff = currentCamera.GetDeadZoneScreenDiff(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return;
            }

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneEnable) {
                var deadZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(mainCamera, deadZoneDiff, ctx.ViewSize);
                targetPos += deadZoneWorldDiff;

                RefreshCameraPos(ctx, id, mainCamera, targetPos);
                return;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneDiff = currentCamera.GetSoftZoneScreenDiff(driverScreenPos);
            if (softZoneDiff == Vector2.zero) {
                var deadZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(mainCamera, deadZoneDiff, ctx.ViewSize);
                targetPos += deadZoneWorldDiff;

                float damping = currentCamera.SoftZoneDampingFactor;
                cameraWorldPos += (targetPos - cameraWorldPos) * damping * deltaTime;
                RefreshCameraPos(ctx, id, mainCamera, cameraWorldPos);
                return;
            }

            // Driver 在 SoftZone 外：硬跟随 SoftZone Diff
            var softZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(mainCamera, softZoneDiff, ctx.ViewSize);
            cameraWorldPos += softZoneWorldDiff;
            RefreshCameraPos(ctx, id, mainCamera, cameraWorldPos);

        }

        static void RefreshCameraPos(Camera2DContext ctx, int id, Camera mainCamera, Vector2 cameraWorldPos) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V2Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }
           
            currentCamera.SetPos(cameraWorldPos);
            ctx.MainCamera.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, ctx.MainCamera.transform.position.z);
        }

    }

}