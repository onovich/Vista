using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class MainContext {

        public Camera2DCore core;
        public Camera2DEntity mainCamera;

        public RoleEntity roleEntity;
        public Vector2 roleMoveAxis;

        public bool isGameStart;

        public MainContext() {
            core = new Camera2DCore();
            isGameStart = false;
        }

        public Camera2DEntity CreateMainCamera(Vector2 pos, Vector2 confinerSize, Vector2 confinerPos, Vector2 deadZoneSize, Vector2 softZoneSize, Vector2 viewSize) {
            mainCamera = core.CreateCamera2D(pos, confinerSize, confinerPos, deadZoneSize, softZoneSize, viewSize);
            return mainCamera;
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            core.SetCurrentCamera(camera);
        }

        public void SetRole(RoleEntity role) {
            roleEntity = role;
        }

    }

}