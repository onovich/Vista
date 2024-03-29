using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista {

    public class CameraMovingComponent {

        public CameraMovingStatus Status { get; private set; }

        public bool Idle_isEntering { get; set; }

        public bool MovingByDriver_isEntering { get; set; }
        public Transform MovingByDriver_driver { get; set; }

        public bool MovingToTarget_isEntering { get; set; }
        public Vector2 MovingToTarget_startPos { get; set; }
        public Vector2 MovingToTarget_targetPos { get; set; }
        public float MovingToTarget_current { get; set; }
        public float MovingToTarget_duration { get; set; }
        public EasingType MovingToTarget_easingType { get; set; }
        public EasingMode MovingToTarget_easingMode { get; set; }
        public Action MovingToTarget_onComplete { get; set; }

        public CameraMovingComponent() { }

        public void EnterIdle() {
            Status = CameraMovingStatus.Idle;
            Idle_isEntering = true;
        }

        public void EnterMovingByDriver(Transform driver) {
            Status = CameraMovingStatus.MovingByDriver;
            MovingByDriver_isEntering = true;
            MovingByDriver_driver = driver;
        }

        public void EnterMovingToTarget(Vector2 startPos, Vector2 targetPos, float duration, EasingType easingType, EasingMode easingMode, Action onComplete = null) {
            Status = CameraMovingStatus.MovingToTarget;
            MovingToTarget_isEntering = true;
            MovingToTarget_startPos = startPos;
            MovingToTarget_targetPos = targetPos;
            MovingToTarget_current = 0f;
            MovingToTarget_duration = duration;
            MovingToTarget_easingType = easingType;
            MovingToTarget_onComplete = onComplete;
        }

        public void MovingToTarget_IncTimer(float dt) {
            MovingToTarget_current += dt;
        }

        public bool MovingToTarget_IsDone() {
            return MovingToTarget_current >= MovingToTarget_duration;
        }

        public void MovingToTarget_OnComplete() {
            MovingToTarget_onComplete?.Invoke();
        }

    }

}