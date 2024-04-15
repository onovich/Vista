
using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DComposerComponent {

        float dampingFactor = 0f;
        internal float DampingFactor => dampingFactor;

        public Camera3DComposerComponent() {
        }

        internal void DampingFactor_Set(float dampingFactor) {
            this.dampingFactor = dampingFactor;
        }

    }

}