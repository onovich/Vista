using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal class Camera2DMovingComponent {

        internal Camera2DMovingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool MovingByDriver_isEntering { get; set; }

        internal bool MovingByDriverRelease_isEntering { get; set; }
        internal Vector2 MovingByDriverRelease_startPos { get; set; }
        internal Vector2 MovingByDriverRelease_targetPos { get; set; }
        internal float MovingByDriverRelease_current { get; set; }
        internal float MovingByDriverRelease_duration { get; set; }
        internal EasingType MovingByDriverRelease_easingType { get; set; }
        internal EasingMode MovingByDriverRelease_easingMode { get; set; }

        internal bool MovingToTarget_isEntering { get; set; }
        internal Vector2 MovingToTarget_startPos { get; set; }
        internal Vector2 MovingToTarget_targetPos { get; set; }
        internal float MovingToTarget_current { get; set; }
        internal float MovingToTarget_duration { get; set; }
        internal EasingType MovingToTarget_easingType { get; set; }
        internal EasingMode MovingToTarget_easingMode { get; set; }
        internal Action MovingToTarget_onComplete { get; set; }

        internal Camera2DMovingComponent() { }

        internal void EnterIdle() {
            Status = Camera2DMovingStatus.Idle;
            Idle_isEntering = true;
            Debug.Log("Camera2DMovingComponent.EnterIdle");
        }

        internal void EnterMovingByDriver() {
            Reset();
            Status = Camera2DMovingStatus.MovingByDriver;
            MovingByDriver_isEntering = true;
        }

        internal void EnterMovingByDriverRelease(Vector2 startPos,
                                                 Vector2 targetPos,
                                                 float duration,
                                                 EasingType easingType,
                                                 EasingMode easingMode) {
            Reset();
            Status = Camera2DMovingStatus.MovingByDriverRelease;
            MovingByDriverRelease_isEntering = true;
            MovingByDriverRelease_startPos = startPos;
            MovingByDriverRelease_targetPos = targetPos;
            MovingByDriverRelease_current = 0f;
            MovingByDriverRelease_duration = duration;
            MovingByDriverRelease_easingType = easingType;
            MovingByDriverRelease_easingMode = easingMode;
        }

        internal void MovingByDriverRelease_IncTimer(float dt) {
            MovingByDriverRelease_current += dt;
        }

        internal bool MovingByDriverRelease_IsDone() {
            return MovingByDriverRelease_current >= MovingByDriverRelease_duration;
        }

        internal void EnterMovingToTarget(Vector2 startPos,
                                          Vector2 targetPos,
                                          float duration,
                                          EasingType easingType,
                                          EasingMode easingMode,
                                          Action onComplete = null) {
            Reset();
            Status = Camera2DMovingStatus.MovingToTarget;
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

        void Reset() {
            Idle_isEntering = false;
            MovingByDriver_isEntering = false;
            MovingByDriverRelease_isEntering = false;
            MovingByDriverRelease_startPos = Vector2.zero;
            MovingByDriverRelease_targetPos = Vector2.zero;
            MovingByDriverRelease_current = 0f;
            MovingByDriverRelease_duration = 0f;
            MovingByDriverRelease_easingType = EasingType.Linear;
            MovingByDriverRelease_easingMode = EasingMode.None;
            MovingToTarget_isEntering = false;
            MovingToTarget_startPos = Vector2.zero;
            MovingToTarget_targetPos = Vector2.zero;
            MovingToTarget_current = 0f;
            MovingToTarget_duration = 0f;
            MovingToTarget_easingType = EasingType.Linear;
            MovingToTarget_easingMode = EasingMode.None;
            MovingToTarget_onComplete = null;
        }

    }

}