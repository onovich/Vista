using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DShakePhase {

        internal static void Tick(Camera3DContext ctx, float dt) {
            var currentCamera = ctx.CurrentCamera;
            if (currentCamera == null) {
                return;
            }

            var mainCamera = ctx.MainCamera;
            if (mainCamera == null) {
                return;
            }

            var shakeCom = currentCamera.ShakeComponent;
            if (shakeCom == null) {
                return;
            }
            if (shakeCom.Current >= shakeCom.Duration) {
                return;
            }
            ApplyShake(ctx, currentCamera, mainCamera, shakeCom, dt);
        }

        static void ApplyShake(Camera3DContext ctx, Camera3DEntity currentCamera, Camera mainCamera, Camera3DShakeComponent shakeCom, float dt) {
            var current = shakeCom.Current;
            var duration = shakeCom.Duration;
            var frequency = shakeCom.Frequency;
            var amplitude = shakeCom.Amplitude;
            var waveType = shakeCom.WaveType;
            var easingType = shakeCom.EasingType;
            var easingMode = shakeCom.EasingMode;
            var phase = shakeCom.Phase;

            var offset = shakeCom.GetOffset();
            shakeCom.IncCurrent(dt);
            var pos = currentCamera.Pos;
            pos += offset;
            mainCamera.transform.position = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
        }

    }

}