using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public class Main2DContext {

        public Camera2DCore core;
        public int mainCameraID;

        public Role2DEntity roleEntity;
        public Vector2 roleMoveAxis;

        public bool isGameStart;

        public Main2DContext(Camera mainCamera, Vector2 screenSize) {
            core = new Camera2DCore(mainCamera, screenSize);
            isGameStart = false;
        }

        public void SetRole(Role2DEntity role) {
            roleEntity = role;
        }

    }

}