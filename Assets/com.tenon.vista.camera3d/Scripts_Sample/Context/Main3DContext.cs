using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Main3DContext {

        public Camera3DCore core;
        public int mainCameraID;

        public Camera mainCamera;
        public float cameraYawAxis;
        public float cameraPitchAxis;

        public Role3DEntity roleEntity;
        public Vector2 roleMoveAxis;
        public float roleJumpAxis;

        public bool isGameStart;

        public RaycastHit[] hitResults;

        public Main3DContext(Camera mainCamera, Vector2 viewSize) {
            core = new Camera3DCore(mainCamera, viewSize);
            this.mainCamera = mainCamera;
            isGameStart = false;
            hitResults = new RaycastHit[10];
        }

        public void SetPerson(Role3DEntity role) {
            roleEntity = role;
        }

    }

}