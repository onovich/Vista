using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DConstraintPhase {

        public static void Tick(Camera2DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            if (camera == null) {
                return;
            }
            ApplyConfiner(camera);
        }

        static void ApplyConfiner(Camera2DEntity camera) {
            var confiner = camera.Confiner;
            var viewSize = camera.ViewSize;
            var pos = camera.Pos;
            var halfSize = viewSize.size * 0.5f;
            var min = confiner.min + halfSize;
            var max = confiner.max - halfSize;
            var x = Mathf.Clamp(pos.x, min.x, max.x);
            var y = Mathf.Clamp(pos.y, min.y, max.y);
            camera.Pos_Set(new Vector2(x, y));
        }

    }

}