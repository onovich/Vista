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
        public bool followX; // True: Follow X & Y & Z; False: Follow Y & Z, Orbit Z

        // Attr
        internal float fov;
        internal float nearClip;
        internal float farClip;
        internal float aspectRatio;

        // Pos
        internal Vector3 pos;
        Vector3 ICamera3D.Pos => pos;

        // Rotation
        internal Quaternion rotation;

        // Input
        internal InputComponent inputComponent;

        // Damping Factor
        internal Vector3 followDampingFactor;
        internal float lookAtDampingFactor;

        // Person
        internal Transform person;
        internal Vector3 personFollowPointLocalOffset;
        internal Vector3 PersonWorldFollowPoint => GetPersonWorldFollowPoint();
        internal Bounds personBounds;
        internal Quaternion personLocalLookAtRotation;
        internal Quaternion PersonWorldLookAtRotation => person.rotation * personLocalLookAtRotation;

        // FSM
        internal TPCamera3DFSMComponent fsmComponent;

        // Shake
        internal Camera3DShakeComponent shakeComponent;
        Camera3DShakeComponent ICamera3D.ShakeComponent => shakeComponent;
        #endregion

        internal TPCamera3DModel() {
            inputComponent = new InputComponent();
            shakeComponent = new Camera3DShakeComponent();
            fsmComponent = new TPCamera3DFSMComponent();
        }

        #region Functions
        // Rotation
        internal void Rotation_SetByEulerAngle(Vector3 eulerAngle) {
            rotation = Quaternion.Euler(eulerAngle);
        }

        Vector3 GetPersonWorldFollowPoint() {
            Vector3 worldOffset = person.TransformDirection(personFollowPointLocalOffset);
            return person.position + worldOffset;
        }

        // Matrix
        Matrix4x4 ICamera3D.GetProjectionMatrix() {
            return Matrix4x4.Perspective(fov, aspectRatio, nearClip, farClip);
        }

        Matrix4x4 ICamera3D.GetViewMatrix() {
            var m = Matrix4x4.TRS(pos, rotation, Vector3.one);
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