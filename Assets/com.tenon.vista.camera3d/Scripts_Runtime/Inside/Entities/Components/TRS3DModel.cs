using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public struct TRS3DModel {

        public Vector3 t;
        public Quaternion r;
        public Vector3 s;
        public Vector3 forward => GetForward();

        public TRS3DModel(Vector3 t, Quaternion r, Vector3 s) {
            this.t = t;
            this.r = r;
            this.s = s;
        }

        Vector3 GetForward() {
            return r * Vector3.forward;
        }

    }

}