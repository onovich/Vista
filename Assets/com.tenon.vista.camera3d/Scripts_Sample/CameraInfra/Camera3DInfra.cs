using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Camera3DInfra {

        public static void Tick(Main3DContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

        public static void DrawGizmos(Main3DContext ctx) {
            ctx.core.DrawGizmos();
        }

        // Camera
        public static int CreateMainCamera(Main3DContext ctx, Vector2 pos, Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            var mainCameraID = ctx.core.CreateCamera3D(pos, confinerWorldMax, confinerWorldMin);
            ctx.mainCameraID = mainCameraID;
            return mainCameraID;
        }

        public static void SetCurrentCamera(Main3DContext ctx, int cameraID) {
            ctx.core.SetCurrentCamera(cameraID);
        }

        // Move
        public static void SetMoveToTarget(Main3DContext ctx, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, System.Action onComplete = null) {
            ctx.core.SetMoveToTarget(ctx.mainCameraID, target, duration, easingType, easingMode, onComplete);
        }

        public static void SetMoveByDriver(Main3DContext ctx, Transform driver) {
            ctx.core.SetMoveByDriver(ctx.mainCameraID, driver);
        }

        // Rotate
        public static void Rotate(Main3DContext ctx, float yaw, float pitch, float roll) {
            ctx.core.Rotate(ctx.mainCameraID, yaw, pitch, roll);
        }

    }

}