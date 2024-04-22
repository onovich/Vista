using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera3D {

    public class Camera3DCore {

        Camera3DContext ctx;

        public Camera3DCore(Camera mainCamera, Vector2 viewSize) {
            ctx = new Camera3DContext();
            ctx.Inject(mainCamera);
            ctx.Init(viewSize, mainCamera.fieldOfView);
        }

        // Tick
        public void Tick(float dt) {
            Camera3DBusiness.Tick(ctx, dt);
        }

        // Camera
        public int CreateTPCamera(Vector3 pos, Vector3 offset, Quaternion rot, float fov, Transform person, bool followX = false) {
            var camera = Camera3DFactory.CreateTPCamera(ctx, pos, offset, rot, fov, person, followX);
            ctx.AddTPCamera(camera, camera.id);
            camera.fsmComponent.AutoFollow_Enter();
            return camera.id;
        }

        // Dead Zone
        public void SetTPCameraDeadZone(int cameraID, Vector2 deadZoneNormalizedSize) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetDeadZone Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.deadZone.Zone_Set(deadZoneNormalizedSize, ctx.viewSize);
        }

        public void SetTPCameraDeadZoneEnable(int cameraID, bool enable) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetDeadZoneEnable Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.deadZone.Enable_Set(enable);
        }

        // Soft Zone
        public void SetTPCameraSoftZone(int cameraID, Vector2 softZoneNormalizedSize) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetSoftZone Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.softZone.Zone_Set(softZoneNormalizedSize, ctx.viewSize);
        }

        public void SetTPCameraSoftZoneEnable(int cameraID, bool enable) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetSoftZoneEnable Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.softZone.Enable_Set(enable);
        }

        // Damping Factor
        public void SetTPCameraFollowDamppingFactor(int cameraID, Vector3 followDampingFactor) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetFollowDamppingFactor Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.followDampingFactor = followDampingFactor;
        }

        public void SetTPCameraLookAtDamppingFactor(int cameraID, float lookAtDampingFactor) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetLookAtDamppingFactor Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.lookAtDampingFactor = lookAtDampingFactor;
        }

        // Person
        public void SetTPCameraPersonBounds(int cameraID, Bounds bounds) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"SetPersonBounds Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.personBounds = bounds;
        }

        // Manual Pan
        public void ManualPan_Set(int cameraID, Vector3 speed) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Set Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmComponent.ManualPan_Enter(speed);
        }

        public void ManualPan_Apply(int cameraID, Vector3 axis) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Apply Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.inputComponent.SetManualPanAxis(axis);
        }

        public void ManualPan_Cancle(int cameraID, float duration, EasingType easingType = EasingType.Sine, EasingMode easingMode = EasingMode.EaseIn) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Recenter Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmComponent.ManualPan_Recenter(duration, camera.pos, easingType, easingMode);
        }

        // Manual Orbital
        public void ManualOrbital_Set(int cameraID, Vector2 speed) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Set Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmComponent.ManualOrbital_Enter(speed);
        }

        public void ManualOrbital_Apply(int cameraID, Vector2 axis) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Apply Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.inputComponent.SetManualOrbitalAxis(axis);
        }

        public void ManualOrbital_Cancle(int cameraID, float duration, EasingType easingType = EasingType.Sine, EasingMode easingMode = EasingMode.EaseIn) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Recenter Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmComponent.ManualOrbital_Recenter(duration, camera.pos, camera.rotation, easingType, easingMode);
        }

        // Shake
        public void ShakeOnce(int cameraID, float frequency, float amplitude, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"Shake Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.shakeComponent.ShakeOnce(frequency, amplitude, duration, easingType, easingMode);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void DrawGizmos(int id) {
            DrawGizmos3DHelper.DrawGizmos(ctx, id);
        }

    }

}