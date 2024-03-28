using MortiseFrame.Abacus;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class CameraInfraContext {

        public Camera2DCore core;
        public Camera2DEntity mainCamera;

        public CameraInfraContext() {
            core = new Camera2DCore();
        }

        public Camera2DEntity CreateMainCamera(FVector2 pos, FVector2 confinerSize, FVector2 confinerPos, FVector2 deadZoneSize, FVector2 deadZonePos, FVector2 viewSize) {
            mainCamera = core.CreateCamera2D(pos, confinerSize, confinerPos, deadZoneSize, deadZonePos, viewSize);
            return mainCamera;
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            core.SetCurrentCamera(camera);
        }

    }

}