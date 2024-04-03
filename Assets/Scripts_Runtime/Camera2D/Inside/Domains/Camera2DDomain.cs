using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista {

    internal static class Camera2DDomain {

        // FSM
        internal static void FSM_SetMoveToTarget(Camera2DContext ctx, Camera2DEntity camera, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            var fsmCom = camera.FSMCom;
            var pos = camera.Pos;
            fsmCom.EnterMovingToTarget(pos, target, duration, easingType, easingMode, onComplete);
        }

        internal static void FSM_SetMoveByDriver(Camera2DContext ctx, Camera2DEntity camera, Transform driver) {
            var fsmCom = camera.FSMCom;
            fsmCom.EnterMovingByDriver(driver);
        }

        //  Move
        internal static void MoveToTarget(Camera2DContext ctx, Camera2DEntity camera, Vector2 startPos, Vector2 targetPos, float current, float duration, EasingType easingType, EasingMode easingMode) {
            var pos = EasingHelper.Easing2D(startPos, targetPos, current, duration, easingType, easingMode);
            camera.SetPos(pos);
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        internal static void MoveByDriver(Camera2DContext ctx, Camera2DEntity currentCamera, Camera mainCamera, Vector2 driverWorldPos, float deltaTime) {
            bool deadZoneEnable = currentCamera.IsDeadZoneEnable();
            bool softZoneEnable = currentCamera.IsSoftZoneEnable();
            Vector2 cameraWorldPos = currentCamera.Pos;
            Vector2 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = driverWorldPos;
                RefreshCameraPos(ctx, currentCamera, mainCamera, targetPos);
                return;
            }

            var driverScreenPos = CameraMathUtil.WorldToScreenPos(mainCamera, driverWorldPos);
            var deadZoneDiff = currentCamera.GetDeadZoneScreenDiff(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return;
            }

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneEnable) {
                var deadZoneWorldDiff = CameraMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, ctx.ViewSize);
                targetPos += deadZoneWorldDiff;

                RefreshCameraPos(ctx, currentCamera, mainCamera, targetPos);
                return;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneDiff = currentCamera.GetSoftZoneScreenDiff(driverScreenPos);
            if (softZoneDiff == Vector2.zero) {
                var deadZoneWorldDiff = CameraMathUtil.ScreenToWorldSize(mainCamera, deadZoneDiff, ctx.ViewSize);
                targetPos += deadZoneWorldDiff;

                float damping = currentCamera.SoftZoneDampingFactor;
                cameraWorldPos += (targetPos - cameraWorldPos) * damping * deltaTime;
                RefreshCameraPos(ctx, currentCamera, mainCamera, cameraWorldPos);
                return;
            }

            // Driver 在 SoftZone 外：硬跟随 SoftZone Diff
            var softZoneWorldDiff = CameraMathUtil.ScreenToWorldSize(mainCamera, softZoneDiff, ctx.ViewSize);
            cameraWorldPos += softZoneWorldDiff;
            RefreshCameraPos(ctx, currentCamera, mainCamera, cameraWorldPos);

        }

        static void RefreshCameraPos(Camera2DContext ctx, Camera2DEntity currentCamera, Camera mainCamera, Vector2 cameraWorldPos) {
            currentCamera.SetPos(cameraWorldPos);
            ctx.MainCamera.transform.position = new Vector3(cameraWorldPos.x, cameraWorldPos.y, ctx.MainCamera.transform.position.z);
        }

    }

}