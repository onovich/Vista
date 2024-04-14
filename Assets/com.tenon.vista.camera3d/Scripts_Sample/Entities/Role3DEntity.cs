using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Role3DEntity : MonoBehaviour {

        public float speed = 1;
        [SerializeField] Rigidbody rb;

        public bool isGround;
        public float jumpForce = 14;

        public Vector3 Velocity => rb.velocity;
        public Vector3 Pos => transform.position;

        public float g = 40f;
        public float fallingSpeedMax = 40;

        public void Move_Jump(float jumpAxis) {
            if (!isGround) {
                return;
            }
            if (jumpAxis <= 0) {
                return;
            }
            var velo = rb.velocity;
            velo.y = jumpForce;
            rb.velocity = velo;
            Move_LeaveGround();
        }

        public void Move_Falling(float dt) {
            var velo = rb.velocity;
            velo.y -= g * dt;
            velo.y = Mathf.Max(velo.y, -fallingSpeedMax);
            rb.velocity = velo;
        }

        public void Move_EnterGround() {
            isGround = true;
        }

        public void Move_LeaveGround() {
            isGround = false;
        }

        public void Move(Vector2 axis, Camera camera) {
            var move = new Vector3(axis.x, 0, axis.y);
            move = camera.transform.TransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);

            var velo = rb.velocity;
            velo.x = move.x * speed;
            velo.z = move.z * speed;
            rb.velocity = velo;
        }

        public void FaceTo(Vector2 axis, Camera camera) {
            Vector3 camDir = camera.transform.forward;
            camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
            rb.rotation = Quaternion.LookRotation(camDir);
        }

    }

}