using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Camera3DInfra {

        public static void Tick(Main3DContext ctx, Transform person, float dt) {
            ctx.core.Tick(dt, person.position, person.rotation, person.localScale);
        }

        public static void OnDrawGUI(Main3DContext ctx) {
            ctx.core.OnDrawGUI(ctx.mainCameraID);
        }

        // Person
        public static void SetPersonOffset(Main3DContext ctx, Vector3 t, Quaternion r, Vector3 s) {
            ctx.core.SetPersonOffset(ctx.mainCameraID, t, r, s);
        }

        // Camera
        public static int CreateTPCamera(Main3DContext ctx, Vector3 t, Quaternion r, Vector3 s, float fov, float nearClip, float farClip, float aspectRatio, float screenWidth) {
            var mainCameraID = ctx.core.CreateTPCamera(t, r, s, fov, nearClip, farClip, aspectRatio, screenWidth);
            ctx.mainCameraID = mainCameraID;
            return mainCameraID;
        }

        public static void GetTPCameraTR(Main3DContext ctx, out Vector3 t, out Quaternion r) {
            ctx.core.GetTPCameraTR(ctx.mainCameraID, out t, out r);
        }

        public static void SetTPCameraFollowX(Main3DContext ctx, bool followX) {
            ctx.core.SetTPCameraFollowX(ctx.mainCameraID, followX);
        }

        // DeadZone
        public static void SetTPCameraDeadZone(Main3DContext ctx, Vector2 deadZoneFOV) {
            ctx.core.SetTPCameraDeadZone(ctx.mainCameraID, deadZoneFOV);
        }

        // SoftZone
        public static void SetTPCameraSoftZone(Main3DContext ctx, Vector2 softZoneFOV) {
            ctx.core.SetTPCameraSoftZone(ctx.mainCameraID, softZoneFOV);
        }

        // Damping Factor
        public static void SetTPCameraFollowDamppingFactor(Main3DContext ctx, Vector3 followDampingFactor) {
            ctx.core.SetTPCameraFollowDamppingFactor(ctx.mainCameraID, followDampingFactor);
        }

        public static void SetTPCameraLookAtDamppingFactor(Main3DContext ctx, float lookAtDampingFactor) {
            ctx.core.SetTPCameraLookAtDamppingFactor(ctx.mainCameraID, lookAtDampingFactor);
        }

        // Manual Pan
        public static void ManualPan_Set(Main3DContext ctx, Vector3 speed) {
            ctx.core.ManualPan_Set(ctx.mainCameraID, speed);
        }

        public static void ManualPan_Apply(Main3DContext ctx, Vector3 axis) {
            ctx.core.ManualPan_Apply(ctx.mainCameraID, axis);
        }

        public static void ManualPan_Cancle(Main3DContext ctx, float duration, EasingType easingType, EasingMode easingMode) {
            ctx.core.ManualPan_Cancle(ctx.mainCameraID, duration, easingType, easingMode);
        }

        // Manual Orbit
        public static void ManualOrbital_Set(Main3DContext ctx, Vector2 speed) {
            ctx.core.ManualOrbital_Set(ctx.mainCameraID, speed);
        }

        public static void ManualOrbital_Apply(Main3DContext ctx, Vector3 axis) {
            ctx.core.ManualOrbital_Apply(ctx.mainCameraID, axis);
        }

        public static void ManualOrbital_Cancle(Main3DContext ctx, float duration, EasingType easingType, EasingMode easingMode) {
            ctx.core.ManualOrbital_Cancle(ctx.mainCameraID, duration, easingType, easingMode);
        }

    }

}