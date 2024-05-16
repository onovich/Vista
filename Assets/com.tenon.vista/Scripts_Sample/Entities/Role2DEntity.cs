using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public class Role2DEntity : MonoBehaviour {

        public float speed = 1;

        public void Ctor() {

        }

        public void Move(Vector2 axis, float dt) {
            transform.position += (Vector3)axis * speed * dt;
        }

    }

}