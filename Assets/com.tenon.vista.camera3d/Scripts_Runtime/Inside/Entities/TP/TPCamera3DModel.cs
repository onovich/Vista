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

        // Attr
        internal float fov;

        // Pos
        internal Vector3 pos;
        Vector3 ICamera3D.Pos => pos;

        // Rotation
        internal Quaternion rotation;

        // Dead Zone
        internal TPCamera3DDeadZoneComponent deadZone;
        internal TPCamera3DDeadZoneComponent softZone;

        internal float followDeadZoneYMax;
        internal float followDeadZoneYMin;

        // Damping Factor
        internal Vector3 followDampingFactor;
        internal float lookAtDampingFactor;

        // Person
        internal Transform person;
        internal Vector3 personFollowPointLocalOffset;
        internal Vector3 PersonWorldFollowPoint => GetPersonWorldFollowPoint();
        internal Bounds personBounds;

        // FSM
        internal TPCamera3DFSMComponent fsmComponent;

        // Shake
        internal Camera3DShakeComponent shakeComponent;
        Camera3DShakeComponent ICamera3D.ShakeComponent => shakeComponent;
        #endregion

        internal TPCamera3DModel() {
            deadZone = new TPCamera3DDeadZoneComponent();
            softZone = new TPCamera3DDeadZoneComponent();
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
        #endregion

    }

}