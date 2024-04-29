using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public struct TRSModel {

        public Vector3 t;
        public Quaternion r;
        public Vector3 s;

        public TRSModel(Vector3 t, Quaternion r, Vector3 s) {
            this.t = t;
            this.r = r;
            this.s = s;
        }

    }

}