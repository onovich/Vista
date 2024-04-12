using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Role3DEntity : MonoBehaviour {

        public float speed = 1;
        [SerializeField] Rigidbody rb;

        public void Ctor() {

        }

        public void Move(Vector2 axis, Camera camera) {
            var move = new Vector3(axis.x, 0, axis.y);
            move = camera.transform.TransformDirection(move);
            // move = Vector3.ProjectOnPlane(move, Vector3.up);
            move.y = 0;

            Vector3 camDir = camera.transform.forward;
            camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
            rb.rotation = Quaternion.LookRotation(camDir);

            rb.velocity = new Vector3(move.x * speed, 0, move.z * speed);
        }

        public void FaceTo(Vector2 axis, Camera camera) {
            // Vector3 faceDir = transform.forward - camera.transform.position;
            // if (faceDir == Vector3.zero) {
            //     return;
            // }

            // faceDir.y = 0;
            // transform.forward = faceDir;

            // if (moveDirection != Vector3.zero) {
            //     Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            //     rb.rotation = newRotation;
            // }
        }

    }

}