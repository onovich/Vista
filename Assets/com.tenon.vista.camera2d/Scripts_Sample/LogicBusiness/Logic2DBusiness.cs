using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public static class Logic2DBusiness {

        public static void EnterGame(Main2DContext ctx) {
            ctx.isGameStart = true;
            Camera2DInfra.SetMoveByDriver(ctx);
        }

        public static void ProcessInput(Main2DContext ctx) {
            if (!ctx.isGameStart) return;

            if (Input.GetKey(KeyCode.W)) {
                ctx.roleMoveAxis += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S)) {
                ctx.roleMoveAxis += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A)) {
                ctx.roleMoveAxis += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D)) {
                ctx.roleMoveAxis += Vector2.right;
            }
            ctx.roleMoveAxis.Normalize();
        }

        public static void ResetInput(Main2DContext ctx) {
            if (!ctx.isGameStart) return;

            ctx.roleMoveAxis = Vector2.zero;
        }

        public static void RoleMove(Main2DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var axis = ctx.roleMoveAxis;
            role.Move(axis, dt);
        }

    }

}