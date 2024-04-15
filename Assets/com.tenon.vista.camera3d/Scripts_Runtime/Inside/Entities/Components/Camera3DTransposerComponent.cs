
using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DTransposerComponent {

        Vector3 dampingFactor = Vector2.zero;
        internal Vector3 DampingFactor => dampingFactor;

        public Camera3DTransposerComponent() {
        }

        internal void DampingFactor_Set(Vector3 dampingFactor) {
            this.dampingFactor = dampingFactor;
        }

    }

}