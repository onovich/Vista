using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Role3DEntity : MonoBehaviour {

        public float speed = 1;
        [SerializeField] Rigidbody rb;

        public void Ctor() {

        }

        public void Move(Vector2 axis, Camera camera) {
            Vector3 faceDir = transform.forward - camera.transform.position;
            faceDir.y = 0;

            Vector3 verticalDirection = faceDir.normalized;
            Vector3 horizontalDirection = Quaternion.Euler(0, 90, 0) * verticalDirection;

            Vector3 moveDirection = verticalDirection * axis.y + horizontalDirection * axis.x;

            // Move
            var velocity = moveDirection * speed;
            rb.velocity = new Vector3(velocity.x, 0, velocity.z);

            // 转向角色
            if (velocity != Vector3.zero) {
                transform.forward = faceDir;
            }
        }

        public void FaceTo(Vector2 axis, Camera camera) {
            Vector3 faceDir = transform.forward - camera.transform.position;
            if (faceDir == Vector3.zero) {
                return;
            }

            faceDir.y = 0;
            transform.forward = faceDir;
        }

    }

}