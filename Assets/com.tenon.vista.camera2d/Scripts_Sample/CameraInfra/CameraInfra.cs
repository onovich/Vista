using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public static class CameraInfra {

        public static void Tick(MainContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

        public static void DrawGizmos(MainContext ctx) {
            ctx.core.DrawGizmos();
        }

        // Camera
        public static int CreateMainCamera(MainContext ctx, Vector2 pos, Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            var mainCameraID = ctx.core.CreateCamera2D(pos, confinerWorldMax, confinerWorldMin);
            ctx.mainCameraID = mainCameraID;
            return mainCameraID;
        }

        public static void SetCurrentCamera(MainContext ctx, int cameraID) {
            ctx.core.SetCurrentCamera(cameraID);
        }

        // Move
        public static void SetMoveToTarget(MainContext ctx, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, System.Action onComplete = null) {
            ctx.core.SetMoveToTarget(ctx.mainCameraID, target, duration, easingType, easingMode, onComplete);
        }

        public static void SetMoveByDriver(MainContext ctx, Transform driver) {
            ctx.core.SetMoveByDriver(ctx.mainCameraID, driver);
        }

    }

}