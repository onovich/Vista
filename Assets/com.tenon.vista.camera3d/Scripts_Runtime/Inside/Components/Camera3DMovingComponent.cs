using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DMovingComponent {

        internal Camera3DMovingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool MovingByDriver_isEntering { get; set; }
        internal Transform MovingByDriver_driver { get; set; }

        internal bool MovingToTarget_isEntering { get; set; }
        internal Vector3 MovingToTarget_startPos { get; set; }
        internal Vector3 MovingToTarget_targetPos { get; set; }
        internal float MovingToTarget_current { get; set; }
        internal float MovingToTarget_duration { get; set; }
        internal EasingType MovingToTarget_easingType { get; set; }
        internal EasingMode MovingToTarget_easingMode { get; set; }
        internal Action MovingToTarget_onComplete { get; set; }

        internal Camera3DMovingComponent() { }

        internal void EnterIdle() {
            Status = Camera3DMovingStatus.Idle;
            Idle_isEntering = true;
        }

        internal void EnterMovingByDriver(Transform driver) {
            Status = Camera3DMovingStatus.MovingByDriver;
            MovingByDriver_isEntering = true;
            MovingByDriver_driver = driver;
        }

        internal void EnterMovingToTarget(Vector3 startPos, Vector3 targetPos, float duration, EasingType easingType, EasingMode easingMode, Action onComplete = null) {
            Status = Camera3DMovingStatus.MovingToTarget;
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