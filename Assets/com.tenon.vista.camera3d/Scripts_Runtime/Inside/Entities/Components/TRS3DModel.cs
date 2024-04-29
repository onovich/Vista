using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public class TRS3DModel {

        public Vector3 t;
        public Quaternion r;
        public Vector3 s;

        public TRS3DModel(Vector3 t, Quaternion r, Vector3 s) {
            this.t = t;
            this.r = r;
            this.s = s;
        }

    }

}