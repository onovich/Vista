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
        internal bool manualPan_isRecentering;
        internal Vector3 manualPan_manualPanSpeed;
        internal Vector3 manualPan_recenterPanStartPos;
        internal float manualPan_recenterPanDuration;
        internal float manualPan_recenterPanCurrent;
        internal EasingMode manualPan_recenterPanEasingMode;
        internal EasingType manualPan_recenterPanEasingType;
        internal TPCamera3DFSMStatus manualPan_lastStatus;

        internal bool manualOrbital_isEntring;
        internal Vector3 manualOrbital_originPos;
        internal Quaternion manualOrbital_originRot;
        internal bool manualOrbital_isRecentering;
        internal Vector2 manualOrbital_manualOrbitalSpeed;
        internal Vector3 manualOrbital_recenterOrbitalStartPos;
        internal Quaternion manualOrbital_recenterOrbitalStartRot;
        internal float manualOrbital_recenterOrbitalDuration;
        internal float manualOrbital_recenterOrbitalCurrent;
        internal EasingMode manualOrbital_recenterOrbitalEasingMode;
        internal EasingType manualOrbital_recenterOrbitalEasingType;
        internal TPCamera3DFSMStatus manualOrbital_lastStatus;

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

        internal void ManualPanXYZ_Enter(Vector3 speed) {
            Reset();
            manualOrbital_lastStatus = status;
            manualPan_isEntring = true;
            manualPan_isRecentering = false;
            status = TPCamera3DFSMStatus.ManualPanXYZ;
            manualPan_manualPanSpeed = speed;
        }

        internal void ManualPanXYZ_Recenter(float duration, Vector3 startPos, EasingType easingType, EasingMode easingMode) {
            manualPan_isRecentering = true;
            manualPan_recenterPanDuration = duration;
            manualPan_recenterPanCurrent = 0;
            manualPan_recenterPanStartPos = startPos;
            manualPan_recenterPanEasingMode = easingMode;
            manualPan_recenterPanEasingType = easingType;
        }

        internal void ManualPanXYZ_Exit() {
            ResumeToAuto(manualOrbital_lastStatus);
        }

        internal void ManualPanXYZ_IncRecenterTimer(float dt) {
            manualPan_recenterPanCurrent += dt;
        }

        internal void ManualOrbitalXZ_Enter(Vector2 speed, Vector3 originPos, Quaternion originRot) {
            Reset();
            manualOrbital_isEntring = true;
            manualOrbital_isRecentering = false;
            status = TPCamera3DFSMStatus.ManualOrbitalXZ;
            manualOrbital_manualOrbitalSpeed = speed;
            manualOrbital_originPos = originPos;
            manualOrbital_originRot = originRot;
        }

        internal void ManualOrbitalXZ_Recenter(float duration, Vector3 startPos, Quaternion startRot, EasingType easingType, EasingMode easingMode) {
            manualOrbital_isRecentering = true;
            manualOrbital_recenterOrbitalDuration = duration;
            manualOrbital_recenterOrbitalCurrent = 0;
            manualOrbital_recenterOrbitalStartPos = startPos;
            manualOrbital_recenterOrbitalStartRot = startRot;
            manualOrbital_recenterOrbitalEasingMode = easingMode;
            manualOrbital_recenterOrbitalEasingType = easingType;
        }

        internal void ManualOrbitalXZ_Exit() {
            ResumeToAuto(manualOrbital_lastStatus);
        }

        internal void ManualOrbitalXZ_IncRecenterTimer(float dt) {
            manualOrbital_recenterOrbitalCurrent += dt;
        }

        internal void Reset() {
            doNothing_isEntring = false;
            followXYZ_isEntring = false;
            followYZAndOrbitalZ_isEntring = false;
            manualPan_isEntring = false;
            manualPan_isRecentering = false;
            manualPan_recenterPanDuration = 0;
            manualPan_recenterPanCurrent = 0;
            manualOrbital_isEntring = false;
            manualOrbital_isRecentering = false;
            manualOrbital_recenterOrbitalDuration = 0;
            manualOrbital_recenterOrbitalCurrent = 0;
        }

        void ResumeToAuto(TPCamera3DFSMStatus status) {
            if (status == TPCamera3DFSMStatus.DoNothing) {
                DoNothing_Enter();
            }
            if (status == TPCamera3DFSMStatus.FollowXYZ) {
                FollowXYZ_Enter();
            }
            if (status == TPCamera3DFSMStatus.FollowYZAndOrbitalZ) {
                FollowYZAndOrbitalZ_Enter();
            }
        }

    }

}