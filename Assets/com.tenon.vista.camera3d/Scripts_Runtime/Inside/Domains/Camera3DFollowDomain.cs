using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowDomain {

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

    }

}