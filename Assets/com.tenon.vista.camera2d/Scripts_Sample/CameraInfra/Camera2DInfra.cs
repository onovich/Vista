using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public static class Camera2DInfra {

        public static Vector2 TickPos(Main2DContext ctx, float dt) {
            return ctx.core.TickPos(dt);
        }

        public static Vector2 TickShakeOffset(Main2DContext ctx, float dt) {
            return ctx.core.TickShakeOffset(dt);
        }

        public static void DrawGizmos(Main2DContext ctx) {
            ctx.core.DrawGizmos();
        }

        // Camera
        public static int CreateMainCamera(Main2DContext ctx, Vector3 pos, float rot, float size, float aspect, Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            var mainCameraID = ctx.core.CreateCamera2D(pos, rot, size, aspect, confinerWorldMax, confinerWorldMin);
            ctx.mainCameraID = mainCameraID;
            return mainCameraID;
        }

        public static void SetCurrentCamera(Main2DContext ctx, int cameraID) {
            ctx.core.SetCurrentCamera(cameraID);
        }

        // Move
        public static void SetMoveToTarget(Main2DContext ctx, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, System.Action onComplete = null) {
            ctx.core.SetMoveToTarget(ctx.mainCameraID, target, duration, easingType, easingMode, onComplete);
        }

        public static void SetMoveByDriver(Main2DContext ctx, Transform driver) {
            ctx.core.SetMoveByDriver(ctx.mainCameraID, driver);
        }

    }

}