using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera2D {

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
            Camera2DShakePhase.Tick(ctx, dt);
        }

        // Camera
        public int CreateCamera2D(Vector2 pos, Vector2 confinerSize, Vector2 confinerPos) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos);
            ctx.AddCamera(camera, camera.ID);
            return camera.ID;
        }

        public void SetCurrentCamera(int cameraID) {
            ctx.SetCurrentCamera(cameraID);
        }

        // DeadZone
        public void SetDeadZone(int cameraID, Vector2 size, Vector2 offset) {
            Camera2DDomain.SetDeadZone(ctx, cameraID, size, offset);
        }

        public void EnableDeadZone(int cameraID, bool enable) {
            Camera2DDomain.EnableDeadZone(ctx, cameraID, enable);
        }

        public bool IsDeadZoneEnable(int cameraID) {
            return Camera2DDomain.IsDeadZoneEnable(ctx, cameraID);
        }

        // SoftZone
        public void SetSoftZone(int cameraID, Vector2 size, Vector2 offset,float dampingFactor) {
            Camera2DDomain.SetSoftZone(ctx, cameraID, size, offset,dampingFactor);
        }

        public void EnableSoftZone(int cameraID, bool enable) {
            Camera2DDomain.EnableSoftZone(ctx, cameraID, enable);
        }

        public bool IsSoftZoneEnable(int cameraID) {
            return Camera2DDomain.IsSoftZoneEnable(ctx, cameraID);
        }

        // Move
        public void SetMoveToTarget(int cameraID, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera2DDomain.FSM_SetMoveToTarget(ctx, cameraID, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(int cameraID, Transform driver) {
            Camera2DDomain.FSM_SetMoveByDriver(ctx, cameraID, driver);
        }

        // Shake
        public void ShakeOnce(int cameraID, float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            Camera2DDomain.ShakeOnce(ctx, cameraID, frequency, amplitude, duration, type, mode);
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