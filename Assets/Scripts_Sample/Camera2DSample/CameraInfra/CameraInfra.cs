using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public static class CameraInfra {

        public static void Tick(MainContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

        public static void DrawGizmos(MainContext ctx) {
            ctx.core.DrawGizmos();
        }

        public static Camera2DEntity CreateMainCamera(MainContext ctx, Vector2 pos, Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            var mainCamera = ctx.core.CreateCamera2D(pos, confinerWorldMax, confinerWorldMin);
            ctx.mainCamera = mainCamera;
            return mainCamera;
        }

        public static void SetCurrentCamera(MainContext ctx, Camera2DEntity camera) {
            ctx.core.SetCurrentCamera(camera);
        }

        public static void SetMoveToTarget(MainContext ctx, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, System.Action onComplete = null) {
            ctx.core.SetMoveToTarget(ctx.mainCamera, target, duration, easingType, easingMode, onComplete);
        }

        public static void SetMoveByDriver(MainContext ctx, Transform driver) {
            ctx.core.SetMoveByDriver(ctx.mainCamera, driver);
        }

    }

}