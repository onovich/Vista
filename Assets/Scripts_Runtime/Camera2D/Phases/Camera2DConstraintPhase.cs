using MortiseFrame.Abacus;

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
            var halfSize = viewSize.Size * 0.5f;
            var min = confiner.Min + halfSize;
            var max = confiner.Max - halfSize;
            var x = FMath.Clamp(pos.x, min.x, max.x);
            var y = FMath.Clamp(pos.y, min.y, max.y);
            camera.Pos_Set(new FVector2(x, y));
        }

    }

}