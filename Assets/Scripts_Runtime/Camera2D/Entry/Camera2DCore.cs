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

        public void Tick(float dt) {
            Camera2DMovingPhase.FSMTick(ctx, dt);
            Camera2DConstraintPhase.Tick(ctx, dt);
        }

        public Camera2DEntity CreateCamera2D(Vector2 pos, Vector2 confinerSize, Vector2 confinerPos) {
            var camera = Camera2DFactory.CreateCamera2D(ctx, pos, confinerSize, confinerPos);
            ctx.AddCamera(camera, camera.ID);
            return camera;
        }

        public void SetDeadZone(Camera2DEntity camera, Vector2 deadZoneNormalizedSize) {
            Camera2DDomain.DeadZone_Set(ctx, camera, deadZoneNormalizedSize);
        }

        public void SetSoftZone(Camera2DEntity camera, Vector2 softZoneNormalizedSize) {
            Camera2DDomain.SoftZone_Set(ctx, camera, softZoneNormalizedSize);
        }

        public void RemoveCamera(Camera2DEntity camera) {
            ctx.RemoveCamera(camera);
        }

        public void SetMoveToTarget(Camera2DEntity camera, Vector2 target, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None, Action onComplete = null) {
            Camera2DDomain.FSM_SetMoveToTarget(ctx, camera, target, duration, easingType, easingMode, onComplete);
        }

        public void SetMoveByDriver(Camera2DEntity camera, Transform driver) {
            Camera2DDomain.FSM_SetMoveByDriver(ctx, camera, driver);
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            ctx.SetCurrentCamera(camera);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void DrawGizmos() {
            var camera = ctx.CurrentCamera;

            // Confiner 是世界坐标,不会跟随相机动
            Gizmos.color = Color.green;
            var confinerCenter = camera.Confiner_GetCenter();
            var confinerSize = camera.Confiner_GetSize();
            Gizmos.DrawWireCube(confinerCenter, confinerSize);

            // DeadZone, SoftZone, ViewSize 是相对坐标，会随着相机移动
            Gizmos.color = Color.red;
            var deadZoneScreenSize = camera.DeadZone_GetSize();
            var deadZoneWorldSize = PositionUtil.ScreenToWorldSize(Camera.main, deadZoneScreenSize);
            Gizmos.DrawWireCube((Vector2)camera.Pos, deadZoneWorldSize);
        }

    }

}