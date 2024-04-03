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
        Camera2DConfinerComponent confinerComponent;

        // DeadZone
        Camera2DDeadZoneComponent deadZoneComponent;
        Camera2DDeadZoneComponent softZoneComponent;

        // DampingFactor
        float dampingFactor = 1f;
        public float DampingFactor => dampingFactor;

        // FSM
        CameraMovingComponent fsmCom;
        public CameraMovingComponent FSMCom => fsmCom;

        public Camera2DEntity() {
            fsmCom = new CameraMovingComponent();
            deadZoneComponent = new Camera2DDeadZoneComponent();
            softZoneComponent = new Camera2DDeadZoneComponent();
        }

        // ID
        public void ID_Set(int id) {
            this.id = id;
        }

        // Pos
        public void Pos_Set(Vector2 pos) {
            this.pos = pos;
        }

        // DeadZone
        public void DeadZone_Set(Vector2 deadZoneNormalizedSize, Vector2 viewSize) {
            deadZoneComponent.Zone_Set(deadZoneNormalizedSize, viewSize);
        }
        public Vector2 DeadZone_GetScreenDiff(Vector2 screenPos) {
            return deadZoneComponent.ScreenDiff_Get(screenPos);
        }

        public Vector2 DeadZone_GetSize() {
            return deadZoneComponent.DeadZoneScreenMax - deadZoneComponent.DeadZoneScreenMin;
        }

        public bool DeadZone_IsEnable() {
            return deadZoneComponent.Enable;
        }

        // SoftZone
        public void SoftZone_Set(Vector2 softZoneNormalizedSize, Vector2 viewSize) {
            softZoneComponent.Zone_Set(softZoneNormalizedSize, viewSize);
        }

        public Vector2 SoftZone_GetScreenDiff(Vector2 screenPos) {
            return softZoneComponent.ScreenDiff_Get(screenPos);
        }

        public Vector2 SoftZone_GetSize() {
            return softZoneComponent.DeadZoneScreenMax - softZoneComponent.DeadZoneScreenMin;
        }

        public bool SoftZone_IsEnable() {
            return softZoneComponent.Enable;
        }

        // Confiner
        public void Confiner_Set(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerComponent = new Camera2DConfinerComponent(confinerWorldMax, confinerWorldMin);
        }

        public Vector2 Confiner_Clamp(Vector2 pos, float orthographicSize, float aspect) {
            return confinerComponent.Clamp(pos, orthographicSize, aspect);
        }

        public Vector2 Confiner_GetCenter() {
            return (confinerComponent.ConfinerWorldMax + confinerComponent.ConfinerWorldMin) / 2f;
        }

        public Vector2 Confiner_GetSize() {
            return confinerComponent.ConfinerWorldMax - confinerComponent.ConfinerWorldMin;
        }

    }

}