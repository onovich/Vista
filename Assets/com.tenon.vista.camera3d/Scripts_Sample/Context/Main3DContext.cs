using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Main3DContext {

        public Camera3DCore core;
        public int mainCameraID;

        public Camera mainCamera;
        public Vector3 cameraPanAxis;
        public bool isCameraPan;
        public bool isCancleCameraPan;

        public Vector2 cameraOrbitalAxis;
        public bool isCameraOrbital;
        public bool isCancleCameraOrbital;

        public Role3DEntity roleEntity;
        public Vector2 roleMoveAxis;
        public float roleJumpAxis;

        public Vector3 camaraPanSpeed;
        public float manualPanCancleDuration;
        public EasingType manualPanEasingType;
        public EasingMode manualPanEasingMode;

        public Vector2 manualOrbitalSpeed;
        public float manualOrbitalCancleDuration;
        public EasingType manualOrbitalEasingType;
        public EasingMode manualOrbitalEasingMode;

        public bool isGameStart;

        public RaycastHit[] hitResults;

        public Main3DContext(Camera mainCamera,
                             Vector2 viewSize,
                             Vector3 camaraPanSpeed,
                             float manualPanCancleDuration,
                             EasingType manualPanEasingType,
                             EasingMode manualPanEasingMode,
                             Vector2 manualOrbitalSpeed,
                             float manualOrbitalCancleDuration,
                             EasingType manualOrbitalEasingType,
                             EasingMode manualOrbitalEasingMode) {
            core = new Camera3DCore(mainCamera, viewSize);
            this.mainCamera = mainCamera;
            isGameStart = false;
            hitResults = new RaycastHit[10];
            this.camaraPanSpeed = camaraPanSpeed;
            this.manualPanCancleDuration = manualPanCancleDuration;
            this.manualPanEasingType = manualPanEasingType;
            this.manualPanEasingMode = manualPanEasingMode;
            this.manualOrbitalSpeed = manualOrbitalSpeed;
            this.manualOrbitalCancleDuration = manualOrbitalCancleDuration;
            this.manualOrbitalEasingType = manualOrbitalEasingType;
            this.manualOrbitalEasingMode = manualOrbitalEasingMode;
        }

        public void SetPerson(Role3DEntity role) {
            roleEntity = role;
        }

    }

}