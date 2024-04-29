using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public struct TRS3DModel {

        public Vector3 t;
        public Quaternion r;
        public Vector3 s;
        public Vector3 forward => GetForward();
        public Vector3 right => GetRight();
        public Vector3 up => GetUp();
        public Vector3 back => GetBack();
        public Vector3 left => GetLeft();
        public Vector3 down => GetDown();

        public TRS3DModel(Vector3 t, Quaternion r, Vector3 s) {
            this.t = t;
            this.r = r;
            this.s = s;
        }

        Vector3 GetForward() {
            return r * Vector3.forward;
        }

        Vector3 GetRight() {
            return r * Vector3.right;
        }

        Vector3 GetUp() {
            return r * Vector3.up;
        }

        Vector3 GetBack() {
            return r * Vector3.back;
        }

        Vector3 GetLeft() {
            return r * Vector3.left;
        }

        Vector3 GetDown() {
            return r * Vector3.down;
        }

    }

}