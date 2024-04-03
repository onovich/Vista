using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista {

    public class Camera2DEntity {

        // ID
        int id;
        public int ID => id;

        // Pos
        Vector2 pos;
        public Vector2 Pos => pos;

        // Confiner
        Bounds confiner;
        public Bounds Confiner => confiner;

        // DeadZone
        Camera2DDeadZoneComponent deadZoneComponent;

        // DampingFactor
        float dampingFactor = 1f;
        public float DampingFactor => dampingFactor;

        // FSM
        CameraMovingComponent fsmCom;
        public CameraMovingComponent FSMCom => fsmCom;

        public Camera2DEntity(int id, Vector2 pos, Bounds confiner, Vector2 deadZoneNormalizedSize, Vector2 screenSize) {
            fsmCom = new CameraMovingComponent();
            this.id = id;
            this.pos = pos;
            this.confiner = confiner;
            this.deadZoneComponent = new Camera2DDeadZoneComponent(deadZoneNormalizedSize, screenSize);
        }

        // Pos
        public void Pos_Set(Vector2 pos) {
            this.pos = pos;
        }

        // DeadZone
        public Vector2 DeadZone_GetScreenDiff(Vector2 screenPos) {
            return deadZoneComponent.GetScreenDiff(screenPos);
        }

        public Vector2 DeadZone_GetSize() {
            return deadZoneComponent.DeadZoneScreenMax - deadZoneComponent.DeadZoneScreenMin;
        }

    }

}