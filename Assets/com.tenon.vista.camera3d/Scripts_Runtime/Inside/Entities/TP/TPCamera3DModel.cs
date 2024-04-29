using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    // 第三人称相机
    internal class TPCamera3DModel : ICamera3D {

        #region Properties & Components
        // ID
        internal int id;

        // Mode
        Camera3DMode ICamera3D.Mode => Camera3DMode.TPCamera;
        public bool followX; // When True: Follow X & Y & Z; False: Follow Y & Z, Orbit Z

        // TRS
        internal TRS3DComponent trs;
        TRS3DComponent ICamera3D.TRS => trs;

        internal TRS3DComponent personTRS;
        internal TRS3DComponent personOffsetTRS;

        // Input
        internal InputComponent inputCom;

        // Attr
        internal Camera3DAttributeComponent attrCom;

        // Damping Factor
        internal Vector3 followDampingFactor;
        internal float lookAtDampingFactor;

        // FSM
        internal TPCamera3DFSMComponent fsmCom;

        // Shake
        internal Camera3DShakeComponent shakeCom;
        Camera3DShakeComponent ICamera3D.ShakeCom => shakeCom;
        #endregion

        internal TPCamera3DModel(int id, Vector3 t, Quaternion r, Vector3 s, float fov, float nearClip, float farClip, float aspectRatio) {
            this.id = id;
            inputCom = new InputComponent();
            shakeCom = new Camera3DShakeComponent();
            fsmCom = new TPCamera3DFSMComponent();
            attrCom = new Camera3DAttributeComponent(fov, nearClip, farClip, aspectRatio);
            trs = new TRS3DComponent(t, r, s);
            personTRS = new TRS3DComponent(Vector3.zero, Quaternion.identity, Vector2.zero);
            personOffsetTRS = new TRS3DComponent(Vector3.zero, Quaternion.identity, Vector2.zero);
        }

        #region Functions
        // Rotation
        internal void Rotation_SetByEulerAngle(Vector3 eulerAngle) {
            trs.r = Quaternion.Euler(eulerAngle);
        }

        // Person
        internal void Person_SetTRS(Vector3 t, Quaternion r, Vector3 s) {
            personTRS.t = t;
            personTRS.r = r;
            personTRS.s = s;
        }

        internal void PersonOffset_SetTRS(Vector3 t, Quaternion r, Vector3 s) {
            personOffsetTRS.t = t;
            personOffsetTRS.r = r;
            personOffsetTRS.s = s;
        }

        // Matrix
        Matrix4x4 ICamera3D.GetProjectionMatrix() {
            return Matrix4x4.Perspective(attrCom.fov, attrCom.aspectRatio, attrCom.nearClip, attrCom.farClip);
        }

        Matrix4x4 ICamera3D.GetViewMatrix() {
            var m = Matrix4x4.TRS(trs.t, trs.r, trs.s);
            m = Matrix4x4.Inverse(m);
            // m.m20 *= -1f;
            // m.m21 *= -1f;
            // m.m22 *= -1f;
            // m.m23 *= -1f;
            return m;
        }

        // Target
        public Vector3 GetPersonWorldFollowPoint() {
            return MatrixUtil.ApplyTRSWithOffset(in personTRS, in personOffsetTRS).t;
        }

        public Quaternion GetPersonWorldFollowRotation() {
            return personOffsetTRS.r * trs.r;
        }
        #endregion

    }

}