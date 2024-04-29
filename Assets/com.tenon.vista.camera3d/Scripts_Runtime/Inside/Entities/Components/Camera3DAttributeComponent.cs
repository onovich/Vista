using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal struct Camera3DAttributeComponent {

        internal float fov;
        internal float nearClip;
        internal float farClip;
        internal float aspectRatio;

        internal Camera3DAttributeComponent(float fov, float nearClip, float farClip, float aspectRatio) {
            this.fov = fov;
            this.nearClip = nearClip;
            this.farClip = farClip;
            this.aspectRatio = aspectRatio;
        }

    }

}