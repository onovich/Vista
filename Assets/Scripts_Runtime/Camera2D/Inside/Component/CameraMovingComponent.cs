using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista {

    internal class CameraMovingComponent {

        internal CameraMovingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool MovingByDriver_isEntering { get; set; }
        internal Transform MovingByDriver_driver { get; set; }

        internal bool MovingToTarget_isEntering { get; set; }
        internal Vector2 MovingToTarget_startPos { get; set; }
        internal Vector2 MovingToTarget_targetPos { get; set; }
        internal float MovingToTarget_current { get; set; }
        internal float MovingToTarget_duration { get; set; }
        internal EasingType MovingToTarget_easingType { get; set; }
        internal EasingMode MovingToTarget_easingMode { get; set; }
        internal Action MovingToTarget_onComplete { get; set; }

        internal CameraMovingComponent() { }

        internal void EnterIdle() {
            Status = CameraMovingStatus.Idle;
            Idle_isEntering = true;
        }

        internal void EnterMovingByDriver(Transform driver) {
            Status = CameraMovingStatus.MovingByDriver;
            MovingByDriver_isEntering = true;
            MovingByDriver_driver = driver;
        }

        internal void EnterMovingToTarget(Vector2 startPos, Vector2 targetPos, float duration, EasingType easingType, EasingMode easingMode, Action onComplete = null) {
            Status = CameraMovingStatus.MovingToTarget;
            MovingToTarget_isEntering = true;
            MovingToTarget_startPos = startPos;
            MovingToTarget_targetPos = targetPos;
            MovingToTarget_current = 0f;
            MovingToTarget_duration = duration;
            MovingToTarget_easingType = easingType;
            MovingToTarget_onComplete = onComplete;
        }

        internal void MovingToTarget_IncTimer(float dt) {
            MovingToTarget_current += dt;
        }

        internal bool MovingToTarget_IsDone() {
            return MovingToTarget_current >= MovingToTarget_duration;
        }

        internal void MovingToTarget_OnComplete() {
            MovingToTarget_onComplete?.Invoke();
        }

    }

}