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
            var camera = Camera3DFactory.CreateTPCamera(ctx, pos, offset, rot, fov, person);
            if (followX) {
                camera.fsmComponent.FollowXYZ_Enter();
            } else {
                camera.fsmComponent.FollowYZAndOrbitalZ_Enter();
            }
            ctx.AddTPCamera(camera, camera.id);
            return camera.id;
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
            camera.fsmComponent.ManualPanXYZ_Enter(speed);
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
            camera.fsmComponent.ManualPanXYZ_Recenter(duration, camera.pos, easingType, easingMode);
        }

        // Manual Orbital
        public void ManualOrbital_Set(int cameraID, Vector2 speed) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Set Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmComponent.ManualOrbitalXZ_Enter(speed);
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
            camera.fsmComponent.ManualOrbitalXZ_Recenter(duration, camera.pos, camera.rotation, easingType, easingMode);
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
            // TODO
        }

    }

}