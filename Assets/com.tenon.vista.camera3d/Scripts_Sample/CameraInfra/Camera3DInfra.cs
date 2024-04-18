using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Camera3DInfra {

        public static void Tick(Main3DContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

        public static void DrawGizmos(Main3DContext ctx) {
            // ctx.core.DrawGizmos();
        }

        // Camera
        public static int CreateTPCamera(Main3DContext ctx, Vector3 pos, Vector3 offset, Vector3 eulerRotation, float fov, Transform person, bool followX) {
            var mainCameraID = ctx.core.CreateTPCamera(pos, offset, eulerRotation, fov, person, followX);
            ctx.mainCameraID = mainCameraID;
            return mainCameraID;
        }

        public static void SetTPCameraFollowDamppingFactor(Main3DContext ctx, Vector3 followDampingFactor) {
            ctx.core.SetTPCameraFollowDamppingFactor(ctx.mainCameraID, followDampingFactor);
        }

        public static void SetTPCameraLookAtDamppingFactor(Main3DContext ctx, float lookAtDampingFactor) {
            ctx.core.SetTPCameraLookAtDamppingFactor(ctx.mainCameraID, lookAtDampingFactor);
        }

    }

}