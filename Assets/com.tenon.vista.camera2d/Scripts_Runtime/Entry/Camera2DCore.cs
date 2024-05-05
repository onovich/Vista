using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera2D {

    public class Camera2DCore {

        Camera2DContext ctx;

        public Camera2DCore(Vector2 screenSize) {
            ctx = new Camera2DContext(screenSize);
        }

        // Tick
        public Vector3 Tick(float dt) {
            Camera2DMovingPhase.FSMTick(ctx, dt);
            Camera2DConstraintPhase.Tick(ctx, dt);
            var pos = ctx.CurrentCamera.Pos;
            var offset = Camera2DShakePhase.TickShakeOffset(ctx, dt);
            var z = ctx.CurrentCamera.Z;
            pos += offset;
            return new Vector3(pos.x, pos.y, z);
        }

        // Camera
        public int CreateCamera2D(Vector3 pos, float rot, float size, float aspect, Vector2 confinerMax, Vector2 confinerMin) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, rot, size, aspect, confinerMax, confinerMin);
            ctx.AddCamera(camera, camera.ID);
            return camera.ID;
        }

        public void SetCurrentCamera(int cameraID) {
            ctx.SetCurrentCamera(cameraID);
        }

        // DeadZone
        public void SetDeadZone(int cameraID, Vector2 normalizedSize, Vector2 offset) {
            Camera2DDeadZoneDomain.SetDeadZone(ctx, cameraID, normalizedSize, offset);
        }

        public void EnableDeadZone(int cameraID, bool enable) {
            Camera2DDeadZoneDomain.EnableDeadZone(ctx, cameraID, enable);
        }

        public bool IsDeadZoneEnable(int cameraID) {
            return Camera2DDeadZoneDomain.IsDeadZoneEnable(ctx, cameraID);
        }

        // SoftZone
        public void SetSoftZone(int cameraID, Vector2 normalizedSize, Vector2 offset, Vector2 dampingFactor) {
            Camera2DDeadZoneDomain.SetSoftZone(ctx, cameraID, normalizedSize, offset, dampingFactor);
        }

        public void EnableSoftZone(int cameraID, bool enable) {
            Camera2DDeadZoneDomain.EnableSoftZone(ctx, cameraID, enable);
        }

        public bool IsSoftZoneEnable(int cameraID) {
            return Camera2DDeadZoneDomain.IsSoftZoneEnable(ctx, cameraID);
        }

        // Move
        public void SetMoveToTarget(int cameraID, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera2DFollowDomain.FSM_SetMoveToTarget(ctx, cameraID, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(int cameraID, Transform driver) {
            Camera2DFollowDomain.FSM_SetMoveByDriver(ctx, cameraID, driver);
        }

        // Shake
        public void ShakeOnce(int cameraID, float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            Camera2DShakeDomain.ShakeOnce(ctx, cameraID, frequency, amplitude, duration, type, mode);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void DrawGizmos() {
            var camera = ctx.CurrentCamera;
            DrawGizmos2DHelper.DrawGizmos(ctx);
        }

    }

}