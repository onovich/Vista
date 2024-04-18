using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DShakePhase {

        internal static void Tick(Camera3DContext ctx, ICamera3D camera, float dt) {

            var agent = ctx.cameraAgent;
            if (agent == null) {
                return;
            }

            var shakeCom = camera.ShakeComponent;
            if (shakeCom == null) {
                return;
            }
            if (shakeCom.Current >= shakeCom.Duration) {
                return;
            }
            ApplyShake(ctx, camera, agent, shakeCom, dt);
        }

        static void ApplyShake(Camera3DContext ctx, ICamera3D currentCamera, Camera agent, Camera3DShakeComponent shakeCom, float dt) {
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
            agent.transform.position = new Vector3(pos.x, pos.y, agent.transform.position.z);
        }

    }

}