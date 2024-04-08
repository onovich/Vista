using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DShakeComponent {

        float frequency;
        public float Frequency => frequency;

        float amplitude;
        public float Amplitude => amplitude;

        float current;
        public float Current => current;

        float duration;
        public float Duration => duration;

        float phase;
        public float Phase => phase;

        WaveType waveType;
        public WaveType WaveType => waveType;

        EasingType easingType;
        public EasingType EasingType => easingType;

        EasingMode easingMode;
        public EasingMode EasingMode => easingMode;

        public Camera3DShakeComponent() { }

        public void ShakeOnce(float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.duration = duration;
            this.waveType = WaveType.Sine;
            this.easingType = type;
            this.easingMode = mode;
            this.phase = 0;
            this.current = 0;
        }

        public void IncCurrent(float dt) {
            current += dt;
        }

        public Vector3 GetOffset() {
            var x = WaveHelper.EasingOutWave(frequency, amplitude, current, duration, phase, waveType, easingType, easingMode);
            var y = WaveHelper.EasingOutWave(frequency, amplitude, current, duration, phase, waveType, easingType, easingMode);
            return new Vector3(x, y);
        }

    }

}