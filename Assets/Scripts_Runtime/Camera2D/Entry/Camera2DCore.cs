using System;
using MortiseFrame.Abacus;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class Camera2DCore {

        Camera2DContext ctx;

        public Camera2DCore() {
            ctx = new Camera2DContext();
        }

        public void Tick(Camera2DEntity camera, float dt) {
            Camera2DStatePhase.FSMTick(camera, dt);
            Camera2DConstraintPhase.Tick(ctx, dt);
        }

        public Camera2DEntity CreateCamera2D(FVector2 pos, FVector2 confinerSize, FVector2 confinerPos, FVector2 deadZoneSize, FVector2 deadZonePos, FVector2 viewSize) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos, deadZoneSize, deadZonePos, viewSize);
            ctx.AddCamera(camera, camera.ID);
            return camera;
        }

        public void RemoveCamera(Camera2DEntity camera) {
            ctx.RemoveCamera(camera);
        }

        public void MoveCameraToTarget(Camera2DEntity camera, FVector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            camera.MoveToTarget(target, duration, easingType, easingMode, onComplete);
        }

        public void MoveCameraByDriver(Camera2DEntity camera, Camera2DDriver driver) {
            camera.MoveByDriver(driver);
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            ctx.SetCurrentCamera(camera);
        }

        public void Clear() {
            ctx.Clear();
        }

    }

}