using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera3D {

    public class Camera3DCore {

        Camera3DContext ctx;

        public Camera3DCore(Camera mainCamera, Vector3 viewSize) {
            ctx = new Camera3DContext();
            ctx.Inject(mainCamera);
            ctx.Init(viewSize);
        }

        // Tick
        public void Tick(float dt) {
            if (!ctx.Inited) {
                return;
            }

            Camera3DTransposerPhase.FSMTick(ctx, dt);
            Camera3DComposerPhase.FSMTick(ctx, dt);
            Camera3DShakePhase.Tick(ctx, dt);
        }

        // Camera
        public int CreateCamera3D(Vector3 pos, Vector3 eulerRotation, Transform driver) {
            var camera = Camera3DFactory.CreateCamera3D(ctx, pos, eulerRotation);
            ctx.AddCamera(camera, camera.ID);
            Camera3DFollowDomain.SetDriver(ctx, camera.ID, driver);
            return camera.ID;
        }

        public void SetCurrentCamera(int cameraID) {
            ctx.SetCurrentCamera(cameraID);
        }

        // Transposer
        public void SetTransposerDampingFactor(int cameraID, Vector3 dampingFactor) {
            Camera3DTransposerDomain.SetDampingFactor(ctx, cameraID, dampingFactor);
        }

        // Composer
        public void SetComposerDampingFactor(int cameraID, float dampingFactor) {
            Camera3DComposerDomain.SetDampingFactor(ctx, cameraID, dampingFactor);
        }

        // Driver
        public void SetDriver(int cameraID, Transform driver) {
            Camera3DFollowDomain.SetDriver(ctx, cameraID, driver);
        }

        // Move
        public void FreeCamera_SetMoveToTarget(int cameraID, Vector3 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera3DMoveDomain.FSM_SetMoveToTarget(ctx, cameraID, target, duration, easingType, easingMode, onComplete);
        }

        // Rotate
        public void Rotate(int cameraID, float yaw, float pitch, float roll) {
            Camera3DRotateDomain.Rotate(ctx, cameraID, yaw, pitch, roll);
        }

        // Shake
        public void ShakeOnce(int cameraID, float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            Camera3DShakeDomain.ShakeOnce(ctx, cameraID, frequency, amplitude, duration, type, mode);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void DrawGizmos() {
            var camera = ctx.CurrentCamera;
            DrawGizmos3DHelper.DrawGizmos(ctx, ctx.MainCamera);
        }

    }

}