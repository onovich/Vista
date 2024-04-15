
using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DComposerComponent {

        Vector3 softZoneDampingFactor = Vector2.zero;
        internal Vector3 SoftZoneDampingFactor => softZoneDampingFactor;

        public Camera3DComposerComponent() {
        }

        internal void SoftZoneDampingFactor_Set(Vector3 dampingFactor) {
            this.softZoneDampingFactor = dampingFactor;
        }

    }

}