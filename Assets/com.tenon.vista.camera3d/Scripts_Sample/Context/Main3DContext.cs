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

        public Role3DEntity roleEntity;
        public Vector2 roleMoveAxis;
        public float roleJumpAxis;

        public Vector3 camaraPanSpeed;
        public float manualPanCancleDuration;

        public bool isGameStart;

        public RaycastHit[] hitResults;

        public Main3DContext(Camera mainCamera, Vector2 viewSize, Vector3 camaraPanSpeed, float manualPanCancleDuration) {
            core = new Camera3DCore(mainCamera, viewSize);
            this.mainCamera = mainCamera;
            isGameStart = false;
            hitResults = new RaycastHit[10];
            this.camaraPanSpeed = camaraPanSpeed;
            this.manualPanCancleDuration = manualPanCancleDuration;
        }

        public void SetPerson(Role3DEntity role) {
            roleEntity = role;
        }

    }

}