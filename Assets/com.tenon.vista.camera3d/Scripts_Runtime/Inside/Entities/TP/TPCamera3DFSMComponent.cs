using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class TPCamera3DFSMComponent {

        // Status
        TPCamera3DFSMStatus status;
        internal TPCamera3DFSMStatus Status => status;

        internal bool doNothing_isEntring;
        internal bool followXYZ_isEntring;
        internal bool followYZAndOrbitalZ_isEntring;

        internal bool manualPan_isEntring;
        internal Vector3 manualPan_originPos;
        internal bool manualPan_isRecenteringPan;
        internal Vector3 manualPan_manualPanSpeed;
        internal float manualPan_recenterPanDuration;
        internal float manualPan_recenterPanCurrent;

        internal bool manualOrbital_isEntring;
        internal Vector3 manualOrbital_originPos;
        internal Quaternion manualOrbital_originRot;
        internal bool manualOrbital_isRecentering;
        internal Vector2 manualOrbital_manualOrbitalSpeed;
        internal float manualOrbital_recenterOrbitalDuration;
        internal float manualOrbital_recenterOrbitalCurrent;

        internal TPCamera3DFSMComponent() { }

        internal void DoNothing_Enter() {
            Reset();
            doNothing_isEntring = true;
        }

        internal void FollowXYZ_Enter() {
            Reset();
            followXYZ_isEntring = true;
            status = TPCamera3DFSMStatus.FollowXYZ;
        }

        internal void FollowYZAndOrbitalZ_Enter() {
            Reset();
            followYZAndOrbitalZ_isEntring = true;
            status = TPCamera3DFSMStatus.FollowYZAndOrbitalZ;
        }

        internal void ManualPanXYZ_Enter(Vector3 speed, Vector3 originPos) {
            Reset();
            manualPan_isEntring = true;
            manualPan_isRecenteringPan = false;
            status = TPCamera3DFSMStatus.ManualPanXYZ;
            manualPan_manualPanSpeed = speed;
            manualPan_originPos = originPos;
        }

        internal void ManualPanXYZ_Recenter(float duration) {
            manualPan_isRecenteringPan = true;
            manualPan_recenterPanDuration = duration;
            manualPan_recenterPanCurrent = 0;
        }

        internal void ManualPanXYZ_IncRecenterTimer(float dt) {
            manualPan_recenterPanCurrent += dt;
        }

        internal void ManualOrbitalXZ_Enter(Vector2 speed, Vector3 originPos, Quaternion originRot) {
            Reset();
            manualOrbital_isEntring = true;
            manualOrbital_isRecentering = false;
            status = TPCamera3DFSMStatus.FollowXYZAndManualOrbitalXZ;
            manualOrbital_manualOrbitalSpeed = speed;
            manualOrbital_originPos = originPos;
            manualOrbital_originRot = originRot;
        }

        internal void ManualOrbitalXZ_Recenter(float duration) {
            manualOrbital_isRecentering = true;
            manualOrbital_recenterOrbitalDuration = duration;
            manualOrbital_recenterOrbitalCurrent = 0;
        }

        internal void ManualOrbitalXZ_IncRecenterTimer(float dt) {
            manualOrbital_recenterOrbitalCurrent += dt;
        }

        internal void Reset() {
            doNothing_isEntring = false;
            followXYZ_isEntring = false;
            followYZAndOrbitalZ_isEntring = false;
            manualPan_isEntring = false;
            manualPan_isRecenteringPan = false;
            manualOrbital_isEntring = false;
            manualOrbital_isRecentering = false;
        }

    }

}