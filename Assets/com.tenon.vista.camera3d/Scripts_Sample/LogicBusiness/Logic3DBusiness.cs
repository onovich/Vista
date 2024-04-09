using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Logic3DBusiness {

        public static void EnterGame(Main3DContext ctx) {
            ctx.isGameStart = true;
            Camera3DInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
        }

        public static void ProcessInput(Main3DContext ctx) {
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
            if (Input.GetKey(KeyCode.Q)) {
                ctx.cameraYawAxis += -1;
            }
            if (Input.GetKey(KeyCode.E)) {
                ctx.cameraYawAxis += 1;
            }
            if (Input.GetKey(KeyCode.R)) {
                ctx.cameraPitchAxis += 1;
            }
            if (Input.GetKey(KeyCode.F)) {
                ctx.cameraPitchAxis += -1;
            }
            ctx.roleMoveAxis.Normalize();
        }

        public static void ResetInput(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            ctx.roleMoveAxis = Vector2.zero;
        }

        public static void RoleMove(Main3DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var camera = ctx.mainCamera;
            var axis = ctx.roleMoveAxis;
            role.Move(axis, camera);
            role.FaceTo(axis, camera);
        }

        public static void CameraMove(Main3DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var camera = ctx.mainCamera;
            var yaw = ctx.cameraYawAxis;
            var pitch = ctx.cameraPitchAxis;
            Camera3DInfra.Rotate(ctx, yaw, pitch, 0);
        }

    }

}