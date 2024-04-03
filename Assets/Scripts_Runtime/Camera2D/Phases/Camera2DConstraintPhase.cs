using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DConstraintPhase {

        public static void Tick(Camera2DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            if (camera == null) {
                return;
            }
            ApplyConfiner(ctx, camera);
            var pos = camera.Pos;
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        static void ApplyConfiner(Camera2DContext ctx, Camera2DEntity camera) {
            var pos = camera.Pos;
            var aspect = ctx.Aspect;
            var orthographicSize = ctx.MainCamera.orthographicSize;
            pos = camera.Confiner_Clamp(pos, orthographicSize, aspect);
            camera.Pos_Set(pos);
        }

    }

}