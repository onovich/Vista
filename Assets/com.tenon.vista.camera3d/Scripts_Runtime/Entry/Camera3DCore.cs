using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera3D {

    public class Camera3DCore {

        Camera3DContext ctx;

        public Camera3DCore() {
            ctx = new Camera3DContext();
        }

        // Tick
        public void Tick(float dt) {
            Camera3DBusiness.Tick(ctx, dt);
        }

        // Camera
        public int CreateTPCamera(Vector3 t, Quaternion r, Vector3 s, float fov, float nearClip, float farClip, float aspectRatio) {
            var camera = Camera3DFactory.CreateTPCamera(ctx, t, r, s, fov, nearClip, farClip, aspectRatio);
            ctx.AddTPCamera(camera, camera.id);
            camera.fsmCom.AutoFollow_Enter();
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

        // Manual Pan
        public void ManualPan_Set(int cameraID, Vector3 speed) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Set Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmCom.ManualPan_Enter(speed);
        }

        public void ManualPan_Apply(int cameraID, Vector3 axis) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Apply Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.inputCom.SetManualPanAxis(axis);
        }

        public void ManualPan_Cancle(int cameraID, float duration, EasingType easingType = EasingType.Sine, EasingMode easingMode = EasingMode.EaseIn) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualPan_Recenter Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmCom.ManualPan_Recenter(duration, camera.trsCom.t, easingType, easingMode);
        }

        // Manual Orbital
        public void ManualOrbital_Set(int cameraID, Vector2 speed) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Set Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmCom.ManualOrbital_Enter(speed);
        }

        public void ManualOrbital_Apply(int cameraID, Vector2 axis) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Apply Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.inputCom.SetManualOrbitalAxis(axis);
        }

        public void ManualOrbital_Cancle(int cameraID, float duration, EasingType easingType = EasingType.Sine, EasingMode easingMode = EasingMode.EaseIn) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"ManualOrbital_Recenter Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.fsmCom.ManualOrbital_Recenter(duration, camera.trsCom.t, camera.trsCom.r, easingType, easingMode);
        }

        // Shake
        public void ShakeOnce(int cameraID, float frequency, float amplitude, float duration, EasingType easingType = EasingType.Linear, EasingMode easingMode = EasingMode.None) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                V3Log.Error($"Shake Error, Camera Not Found: ID = {cameraID}");
                return;
            }
            camera.shakeCom.ShakeOnce(frequency, amplitude, duration, easingType, easingMode);
        }

        public void Clear() {
            ctx.Clear();
        }

        public void OnDrawGUI(int id) {
            DrawGizmos3DHelper.OnDrawGUI(ctx, id);
        }

    }

}