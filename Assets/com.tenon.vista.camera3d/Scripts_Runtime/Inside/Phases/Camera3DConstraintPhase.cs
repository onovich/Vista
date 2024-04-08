using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DConstraintPhase {

        internal static void Tick(Camera3DContext ctx, float dt) {
            var camera = ctx.CurrentCamera;
            if (camera == null) {
                return;
            }
            ApplyConfiner(ctx, camera);
            var pos = camera.Pos;
            ctx.MainCamera.transform.position = new Vector3(pos.x, pos.y, ctx.MainCamera.transform.position.z);
        }

        static void ApplyConfiner(Camera3DContext ctx, Camera3DEntity camera) {
            var src = camera.Pos;
            var aspect = ctx.Aspect;
            var orthographicSize = ctx.MainCamera.orthographicSize;
            var succ = camera.TryClampByConfiner(src, orthographicSize, aspect, out Vector3 dst);
            ctx.SetConfinerValid(succ);
            camera.SetPos(dst);
        }

    }

}