using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public class MainContext {

        public Camera2DCore core;
        public Camera2DEntity mainCamera;

        public RoleEntity roleEntity;
        public Vector2 roleMoveAxis;

        public bool isGameStart;

        public MainContext(Camera mainCamera, Vector2 screenSize) {
            core = new Camera2DCore(mainCamera, screenSize);
            isGameStart = false;
        }

        public void SetRole(RoleEntity role) {
            roleEntity = role;
        }

    }

}