using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DFollowDomain {

        internal static void FSM_SetMoveToTarget(Camera2DContext ctx, int id, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"SetMoveToTarget Error, Camera Not Found: ID = {id}");
                return;
            }
            var fsmCom = camera.FSMCom;
            var pos = camera.Pos;
            fsmCom.EnterMovingToTarget(pos, target, duration, easingType, easingMode, onComplete);
        }

        internal static void FSM_SetMoveByDriver(Camera2DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"SetMoveByDriver Error, Camera Not Found: ID = {id}");
                return;
            }
            var fsmCom = camera.FSMCom;
            fsmCom.EnterMovingByDriver();
        }

        internal static void FSM_SetMoveByDriverRelease(Camera2DContext ctx, int id, Vector2 startPos, Vector2 targetPos, float duration, EasingType easingType, EasingMode easingMode) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"SetMoveByDriverRelease Error, Camera Not Found: ID = {id}");
                return;
            }
            var fsmCom = camera.FSMCom;
            fsmCom.EnterMovingByDriverRelease(startPos, targetPos, duration, easingType, easingMode);
        }

        internal static void RecordDriverPos(Camera2DContext ctx, int id, Vector2 driverPos) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"RecordDriverPos Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.RecordDriverPos(driverPos);
        }

    }

}