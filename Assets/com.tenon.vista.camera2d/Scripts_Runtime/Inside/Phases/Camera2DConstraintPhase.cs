using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DConstraintPhase {

        internal static void Tick(Camera2DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            if (camera == null) {
                return;
            }
            ApplyConfiner(ctx, camera);
            var pos = camera.Pos;
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        static void ApplyConfiner(Camera2DContext ctx, Camera2DEntity camera) {
            var src = camera.Pos;
            var aspect = ctx.Aspect;
            var orthographicSize = ctx.MainCamera.orthographicSize;
            var succ = camera.TryClampByConfiner(src, orthographicSize, aspect, out Vector2 dst);
            ctx.SetConfinerValid(succ);
            camera.SetPos(dst);
        }

    }

}