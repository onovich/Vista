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
            Camera3DMovingPhase.FSMTick(ctx, dt);
            Camera3DConstraintPhase.Tick(ctx, dt);
            Camera3DShakePhase.Tick(ctx, dt);
        }

        // Camera
        public int CreateCamera3D(Vector3 pos, Vector3 confinerSize, Vector3 confinerPos) {
            var camera = Camera3DFactory.CreateCamera3D(ctx, pos, confinerSize, confinerPos);
            ctx.AddCamera(camera, camera.ID);
            return camera.ID;
        }

        public void SetCurrentCamera(int cameraID) {
            ctx.SetCurrentCamera(cameraID);
        }

        // DeadZone
        public void SetDeadZone(int cameraID, Vector3 normalizedSize, Vector3 offset) {
            Camera3DDeadZoneDomain.SetDeadZone(ctx, cameraID, normalizedSize, offset);
        }

        public void EnableDeadZone(int cameraID, bool enable) {
            Camera3DDeadZoneDomain.EnableDeadZone(ctx, cameraID, enable);
        }

        public bool IsDeadZoneEnable(int cameraID) {
            return Camera3DDeadZoneDomain.IsDeadZoneEnable(ctx, cameraID);
        }

        // SoftZone
        public void SetSoftZone(int cameraID, Vector3 normalizedSize, Vector3 offset, float dampingFactor) {
            Camera3DDeadZoneDomain.SetSoftZone(ctx, cameraID, normalizedSize, offset, dampingFactor);
        }

        public void EnableSoftZone(int cameraID, bool enable) {
            Camera3DDeadZoneDomain.EnableSoftZone(ctx, cameraID, enable);
        }

        public bool IsSoftZoneEnable(int cameraID) {
            return Camera3DDeadZoneDomain.IsSoftZoneEnable(ctx, cameraID);
        }

        // Move
        public void SetMoveToTarget(int cameraID, Vector3 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera3DFollowDomain.FSM_SetMoveToTarget(ctx, cameraID, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(int cameraID, Transform driver) {
            Camera3DFollowDomain.FSM_SetMoveByDriver(ctx, cameraID, driver);
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