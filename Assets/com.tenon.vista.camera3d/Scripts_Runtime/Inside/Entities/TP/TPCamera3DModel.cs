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
        internal TRS3DComponent trsCom;
        TRS3DComponent ICamera3D.TRS => trsCom;

        // Input
        internal InputComponent inputCom;

        // Attr
        internal Camera3DAttributeComponent attrCom;

        // Damping Factor
        internal Vector3 followDampingFactor;
        internal float lookAtDampingFactor;

        // Person

        internal Transform person;
        internal Vector3 personFollowPointLocalOffset;
        internal Vector3 PersonWorldFollowPoint => GetPersonWorldFollowPoint();
        internal Quaternion personLocalLookAtRotation;
        internal Quaternion PersonWorldLookAtRotation => person.rotation * personLocalLookAtRotation;

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
            trsCom = new TRS3DComponent(t, r, s);
        }

        #region Functions
        // Rotation
        internal void Rotation_SetByEulerAngle(Vector3 eulerAngle) {
            trsCom.r = Quaternion.Euler(eulerAngle);
        }

        Vector3 GetPersonWorldFollowPoint() {
            Vector3 worldOffset = person.TransformDirection(personFollowPointLocalOffset);
            return person.position + worldOffset;
        }

        // Matrix
        Matrix4x4 ICamera3D.GetProjectionMatrix() {
            return Matrix4x4.Perspective(attrCom.fov, attrCom.aspectRatio, attrCom.nearClip, attrCom.farClip);
        }

        Matrix4x4 ICamera3D.GetViewMatrix() {
            var m = Matrix4x4.TRS(trsCom.t, trsCom.r, trsCom.s);
            m = Matrix4x4.Inverse(m);
            // m.m20 *= -1f;
            // m.m21 *= -1f;
            // m.m22 *= -1f;
            // m.m23 *= -1f;
            return m;
        }
        #endregion

    }

}