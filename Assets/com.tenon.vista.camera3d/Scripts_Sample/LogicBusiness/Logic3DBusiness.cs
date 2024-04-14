using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Logic3DBusiness {

        public static void EnterGame(Main3DContext ctx) {
            ctx.isGameStart = true;
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
            if (Input.GetKey(KeyCode.Space)) {
                ctx.roleJumpAxis = 1;
            }
            ctx.roleMoveAxis.Normalize();
        }

        public static void ResetInput(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            ctx.roleMoveAxis = Vector2.zero;
            ctx.cameraYawAxis = 0;
            ctx.cameraPitchAxis = 0;
            ctx.roleJumpAxis = 0;
        }

        public static void RoleMove(Main3DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var camera = ctx.mainCamera;
            var axis = ctx.roleMoveAxis;
            var jumpAxis = ctx.roleJumpAxis;
            role.Move(axis, camera);
            role.FaceTo(axis, camera);
        }

        public static void RoleJump(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var jumpAxis = ctx.roleJumpAxis;
            role.Move_Jump(jumpAxis);
        }

        public static void RoleFalling(Main3DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            role.Move_Falling(dt);
        }

        public static void BoxCast(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var pos = role.Pos;
            var dir = Vector3.down;

            Debug.DrawRay(pos, dir * 1.3f, Color.red);

            var hitResults = ctx.hitResults;
            var ray = new Ray(pos, dir);
            var hitCount = Physics.RaycastNonAlloc(ray, hitResults, 1.3f);
            for (int i = 0; i < hitCount; i++) {
                var hit = hitResults[i];
                var hitGo = hit.collider.gameObject;
                if (hitGo.CompareTag("Ground")) {
                    RoleEnterGroundOrBlock(ctx);
                }
            }
        }

        static void RoleEnterGroundOrBlock(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            if (role.Velocity.y <= 0) {
                role.Move_EnterGround();
            }
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