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
            ctx.MainCamera.transform.position = pos;
        }

        static void ApplyConfiner(Camera2DContext ctx, Camera2DEntity camera) {
            var confiner = camera.Confiner;
            var screenHalfSize = ctx.ScreenSize * 0.5f;
            var pos = camera.Pos;
            var min = (Vector2)confiner.min + screenHalfSize;
            var max = (Vector2)confiner.max - screenHalfSize;
            var x = Mathf.Clamp(pos.x, min.x, max.x);
            var y = Mathf.Clamp(pos.y, min.y, max.y);
            camera.Pos_Set(new Vector2(x, y));
        }

    }

}