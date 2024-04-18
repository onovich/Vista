using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class TPCamera3DFSMComponent {

        // Status
        TPCamera3DFSMStatus status;
        internal TPCamera3DFSMStatus Status => status;

        public bool doNothing_isEntring;
        public bool followXYZ_isEntring;
        public bool followYZAndOrbitalZ_isEntring;
        public bool manualPan_isEntring;
        public bool manualPan_isRecenteringPan;
        public bool manualOrbital_isEntring;
        public bool manualOrbital_isRecentering;

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

        internal void ManualPan_Enter() {
            Reset();
            manualPan_isEntring = true;
            manualPan_isRecenteringPan = false;
            status = TPCamera3DFSMStatus.ManualPanXYZ;
        }

        internal void ManualPan_Recenter() {
            manualPan_isRecenteringPan = true;
        }

        internal void ManualOrbital_Enter() {
            Reset();
            manualOrbital_isEntring = true;
            manualOrbital_isRecentering = false;
            status = TPCamera3DFSMStatus.FollowXYZAndManualOrbitalXZ;
        }

        internal void ManualOrbital_Recenter() {
            manualOrbital_isRecentering = true;
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