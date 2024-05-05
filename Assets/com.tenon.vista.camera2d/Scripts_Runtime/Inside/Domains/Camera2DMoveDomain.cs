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
        }

        internal static Vector2 GetDriverTarget(Camera2DContext ctx, int id, Vector2 driverWorldPoint, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V2Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return Vector2.zero;
            }
            bool deadZoneEnable = currentCamera.IsDeadZoneEnable();
            bool softZoneEnable = currentCamera.IsSoftZoneEnable();
            Vector2 cameraWorldPoint = currentCamera.Pos;
            float cameraWorldPointX = cameraWorldPoint.x;
            float cameraWorldPointY = cameraWorldPoint.y;
            Vector2 targetPos = cameraWorldPoint;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = driverWorldPoint;
                return targetPos;
            }

            var driverScreenPoint = Camera2DMathUtil.WorldToScreenPoint(currentCamera, driverWorldPoint, ctx.ScreenSize);
            var deadZoneDiff = currentCamera.GetDeadZoneScreenDiff(driverScreenPoint);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return Vector2.zero;
            }

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneEnable) {
                var deadZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(currentCamera, deadZoneDiff, ctx.ScreenSize);
                targetPos += deadZoneWorldDiff;
                return targetPos;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneDiff = currentCamera.GetSoftZoneScreenDiff(driverScreenPoint);
            if (softZoneDiff == Vector2.zero) {
                var deadZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(currentCamera, deadZoneDiff, ctx.ScreenSize);

                float dampingX = currentCamera.SoftZoneDampingFactor.x;
                float dampingY = currentCamera.SoftZoneDampingFactor.y;

                var dampingOffsetX = EasingHelper.Easing(0f,
                                                            deadZoneWorldDiff.x,
                                                            1 - dampingX,
                                                            1,
                                                            EasingType.Linear,
                                                            EasingMode.None);
                var dampingOffsetY = EasingHelper.Easing(0f,
                                                            deadZoneWorldDiff.y,
                                                            1 - dampingY,
                                                            1,
                                                            EasingType.Linear,
                                                            EasingMode.None);

                var dampingOffset = new Vector2(dampingOffsetX, dampingOffsetY);

                targetPos = cameraWorldPoint + dampingOffset;
                return targetPos;
            }

            // Driver 在 SoftZone 外：硬跟随 SoftZone Diff
            var softZoneWorldDiff = Camera2DMathUtil.ScreenToWorldLength(currentCamera, softZoneDiff, ctx.ScreenSize);
            targetPos = cameraWorldPoint + softZoneWorldDiff;
            return targetPos;
        }

        internal static void MoveByDriver(Camera2DContext ctx, int id, Vector2 targetPos) {
            RefreshCameraPos(ctx, id, targetPos);
        }

        static void RefreshCameraPos(Camera2DContext ctx, int id, Vector2 cameraWorldPoint) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V2Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.SetPos(cameraWorldPoint);
        }

    }

}