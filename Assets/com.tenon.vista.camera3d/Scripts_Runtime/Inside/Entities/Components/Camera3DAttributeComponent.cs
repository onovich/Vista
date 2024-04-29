using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal struct Camera3DAttributeComponent {

        internal float fov;
        internal float nearClip;
        internal float farClip;
        internal float aspectRatio;
        internal float screenWidth;
        internal float screenHeight => screenWidth / aspectRatio;
        internal Vector2 screenSize => new Vector2(screenWidth, screenHeight);

        internal Camera3DAttributeComponent(float fov, float nearClip, float farClip, float aspectRatio, float screenWidth) {
            this.fov = fov;
            this.nearClip = nearClip;
            this.farClip = farClip;
            this.aspectRatio = aspectRatio;
            this.screenWidth = screenWidth;
        }

    }

}