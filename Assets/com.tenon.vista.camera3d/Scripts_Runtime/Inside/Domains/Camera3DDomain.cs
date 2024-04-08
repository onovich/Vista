using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DDomain {

        // FSM
        internal static void FSM_SetMoveToTarget(Camera3DContext ctx, int id, Vector3 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetMoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            var fsmCom = camera.FSMCom;
            var pos = camera.Pos;
            fsmCom.EnterMovingToTarget(pos, target, duration, easingType, easingMode, onComplete);
        }

        internal static void FSM_SetMoveByDriver(Camera3DContext ctx, int id, Transform driver) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetMoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            var fsmCom = camera.FSMCom;
            fsmCom.EnterMovingByDriver(driver);
        }

        //  Move
        internal static void MoveToTarget(Camera3DContext ctx, int id, Vector3 startPos, Vector3 targetPos, float current, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"MoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            var pos = EasingHelper.Easing3D(startPos, targetPos, current, duration, easingType, easingMode);
            camera.SetPos(pos);
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        internal static void MoveByDriver(Camera3DContext ctx, int id, Camera mainCamera, Vector3 driverWorldPos, float deltaTime) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"MoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            bool deadZoneEnable = currentCamera.IsDeadZoneEnable();
            bool softZoneEnable = currentCamera.IsSoftZoneEnable();
            Vector3 cameraWorldPos = currentCamera.Pos;
            Vector3 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = driverWorldPos;
                RefreshCameraPos(ctx, id, mainCamera, targetPos);
                return;
            }

            var driverScreenPos = Camera3DMathUtil.WorldToScreenPos(mainCamera, driverWorldPos);
            var deadZoneDiff = currentCamera.GetDeadZoneScreenDiff(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return;
            }

            // Get Depth
            var depth = Camera3DMathUtil.GetDepth(mainCamera, driverWorldPos - cameraWorldPos);

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneEnable) {
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, depth);
                targetPos += deadZoneWorldDiff;

                RefreshCameraPos(ctx, id, mainCamera, targetPos);
                return;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneDiff = currentCamera.GetSoftZoneScreenDiff(driverScreenPos);
            if (softZoneDiff == Vector2.zero) {
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, depth);
                targetPos += deadZoneWorldDiff;

                float damping = currentCamera.SoftZoneDampingFactor;
                cameraWorldPos += (targetPos - cameraWorldPos) * damping * deltaTime;
                RefreshCameraPos(ctx, id, mainCamera, cameraWorldPos);
                return;
            }

            // Driver 在 SoftZone 外：硬跟随 SoftZone Diff
            var softZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(mainCamera, softZoneDiff, depth);
            cameraWorldPos += softZoneWorldDiff;
            RefreshCameraPos(ctx, id, mainCamera, cameraWorldPos);

        }

        static void RefreshCameraPos(Camera3DContext ctx, int id, Camera mainCamera, Vector3 cameraWorldPos) {
            var has = ctx.TryGetCamera(id, out var currentCamera);
            if (!has) {
                V3Log.Error($"RefreshCameraPos Error, Camera Not Found: ID = {id}");
                return;
            }

            currentCamera.SetPos(cameraWorldPos);
            ctx.MainCamera.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, ctx.MainCamera.transform.position.z);
        }

        // DeadZone
        internal static void SetDeadZone(Camera3DContext ctx, int id, Vector3 normalizedSize, Vector3 offset) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetDeadZone(normalizedSize, ctx.ViewSize);
        }

        internal static void EnableDeadZone(Camera3DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"EnableDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableDeadZone(enable);
        }

        internal static bool IsDeadZoneEnable(Camera3DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"IsDeadZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsDeadZoneEnable();
        }

        // SoftZone
        internal static void SetSoftZone(Camera3DContext ctx, int id, Vector3 normalizedSize, Vector3 offset, float dampingFactor) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetSoftZone(normalizedSize, ctx.ViewSize, dampingFactor);
        }

        internal static void EnableSoftZone(Camera3DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"EnableSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableSoftZone(enable);
        }

        internal static bool IsSoftZoneEnable(Camera3DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"IsSoftZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsSoftZoneEnable();
        }

        // Shake
        internal static void ShakeOnce(Camera3DContext ctx, int id, float frequency, float amplitude, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"Shake Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.ShakeComponent.ShakeOnce(frequency, amplitude, duration, easingType, easingMode);
        }

    }

}