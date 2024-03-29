using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class Camera2DCore {

        Camera2DContext ctx;

        public Camera2DCore() {
            ctx = new Camera2DContext();
        }

        public void Tick(float dt) {
            Camera2DMovingPhase.FSMTick(ctx, dt);
            Camera2DConstraintPhase.Tick(ctx, dt);
        }

        public Camera2DEntity CreateCamera2D(Vector2 pos, Vector2 confinerSize, Vector2 confinerPos, Vector2 deadZoneSize, Vector2 softZoneSize, Vector2 viewSize) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos, deadZoneSize, softZoneSize, viewSize);
            ctx.AddCamera(camera, camera.ID);
            return camera;
        }

        public void RemoveCamera(Camera2DEntity camera) {
            ctx.RemoveCamera(camera);
        }

        public void SetMoveToTarget(Camera2DEntity camera, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            camera.SetMoveToTarget(target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(Camera2DEntity camera, Transform driver) {
            camera.SetMoveByDriver(driver);
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            ctx.SetCurrentCamera(camera);
        }

        public void Clear() {
            ctx.Clear();
        }

    }

}