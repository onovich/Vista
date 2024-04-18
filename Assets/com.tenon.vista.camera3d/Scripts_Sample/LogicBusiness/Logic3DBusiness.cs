using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public static class Logic3DBusiness {

        // Game
        public static void EnterGame(Main3DContext ctx) {
            ctx.isGameStart = true;
        }

        // Input
        public static void ProcessInput(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            // Role Move
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

            // Role Jump
            if (Input.GetKey(KeyCode.Space)) {
                ctx.roleJumpAxis = 1;
            }

            // Camera Set Pan
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                ctx.isCameraPan = true;
                ctx.isCancleCameraPan = false;
            }

            // Camera Cancle Pan
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                ctx.isCancleCameraPan = true;
                ctx.isCameraPan = false;
            }

            // Camera Apply Pan
            if (Input.GetKey(KeyCode.UpArrow)) {
                ctx.cameraPanAxis.z += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                ctx.cameraPanAxis.z += -1;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                ctx.cameraPanAxis.x += -1;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                ctx.cameraPanAxis.x += 1;
            }
            if (Input.GetKey(KeyCode.Q)) {
                ctx.cameraPanAxis.y += 1;
            }
            if (Input.GetKey(KeyCode.E)) {
                ctx.cameraPanAxis.y += -1;
            }

            ctx.roleMoveAxis.Normalize();
        }

        public static void ResetInput(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            ctx.roleMoveAxis = Vector2.zero;
            ctx.cameraPanAxis = Vector3.zero;
            ctx.roleJumpAxis = 0;

            ctx.isCameraPan = false;
            ctx.isCancleCameraPan = false;
        }

        // Role
        public static void RoleMove(Main3DContext ctx, float dt) {
            if (!ctx.isGameStart) return;

            var role = ctx.roleEntity;
            var camera = ctx.mainCamera;
            var axis = ctx.roleMoveAxis;
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

        // Camera
        public static void CameraPan_ApplySet(Main3DContext ctx) {
            if (!ctx.isGameStart) return;
            var isPan = ctx.isCameraPan;
            if (!isPan) {
                return;
            }

            Debug.Log("CameraPan_ApplySet");

            var speed = ctx.camaraPanSpeed;

            Camera3DInfra.ManualPan_Set(ctx, speed);
        }

        public static void CameraPan_Apply(Main3DContext ctx) {
            if (!ctx.isGameStart) return;

            var axis = ctx.cameraPanAxis;
            var dt = Time.deltaTime;

            Camera3DInfra.ManualPan_Apply(ctx, axis, dt);
        }

        public static void CameraPan_ApplyCancle(Main3DContext ctx) {
            if (!ctx.isGameStart) return;
            var isCancle = ctx.isCancleCameraPan;
            if (!isCancle) {
                return;
            }

            var duration = ctx.manualPanCancleDuration;

            Camera3DInfra.ManualPan_Cancle(ctx, duration);
        }

    }

}