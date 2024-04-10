using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DMovingComponent {

        internal Camera3DMovingStatus Status { get; private set; }

        internal bool Idle_isEntering { get; set; }

        internal bool MovingByDriver_isEntering { get; set; }
        internal Transform MovingByDriver_driver { get; set; }

        internal bool MovingByDollyTrack_isEntering { get; set; }
        internal Vector3 MovingByDollyTrack_startPos { get; set; }
        internal Vector3 MovingByDollyTrack_targetPos { get; set; }
        internal float MovingByDollyTrack_current { get; set; }
        internal float MovingByDollyTrack_duration { get; set; }
        internal EasingType MovingByDollyTrack_easingType { get; set; }
        internal EasingMode MovingByDollyTrack_easingMode { get; set; }
        internal Action MovingByDollyTrack_onComplete { get; set; }

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

        internal void EnterMovingByDollyTrack(Vector3 startPos, Vector3 targetPos, float duration, EasingType easingType, EasingMode easingMode, Action onComplete = null) {
            Status = Camera3DMovingStatus.MovingByDollyTrack;
            MovingByDollyTrack_isEntering = true;
            MovingByDollyTrack_startPos = startPos;
            MovingByDollyTrack_targetPos = targetPos;
            MovingByDollyTrack_current = 0f;
            MovingByDollyTrack_duration = duration;
            MovingByDollyTrack_easingType = easingType;
            MovingByDollyTrack_onComplete = onComplete;
        }

        internal void MovingByDollyTrack_IncTimer(float dt) {
            MovingByDollyTrack_current += dt;
        }

        internal bool MovingByDollyTrack_IsDone() {
            return MovingByDollyTrack_current >= MovingByDollyTrack_duration;
        }

        internal void MovingByDollyTrack_OnComplete() {
            MovingByDollyTrack_onComplete?.Invoke();
        }

    }

}