using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace MortiseFrame.Vista {

    public class Camera2DCore {

        Camera2DContext ctx;

        public Camera2DCore(Camera mainCamera, Vector2 screenSize) {
            ctx = new Camera2DContext();
            ctx.Inject(mainCamera);
            ctx.Init(screenSize);
        }

        // Tick
        public void Tick(float dt) {
            if (!ctx.Inited) {
                return;
            }
            Camera2DMovingPhase.FSMTick(ctx, dt);
            Camera2DConstraintPhase.Tick(ctx, dt);
        }

        // Camera
        public Camera2DEntity CreateCamera2D(Vector2 pos, Vector2 confinerSize, Vector2 confinerPos) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos);
            ctx.AddCamera(camera, camera.ID);
            return camera;
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            ctx.SetCurrentCamera(camera);
        }

        // Move
        public void SetMoveToTarget(Camera2DEntity camera, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera2DDomain.FSM_SetMoveToTarget(ctx, camera, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(Camera2DEntity camera, Transform driver) {
            Camera2DDomain.FSM_SetMoveByDriver(ctx, camera, driver);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void DrawGizmos() {
            var camera = ctx.CurrentCamera;
            DrawGizmosHelper.DrawGizmos(ctx, ctx.MainCamera);
        }

    }

}