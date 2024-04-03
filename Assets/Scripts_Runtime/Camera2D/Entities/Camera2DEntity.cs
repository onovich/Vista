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
        float softZoneDampingFactor = 0f;
        public float SoftZoneDampingFactor => softZoneDampingFactor;

        // FSM
        Camera2DMovingComponent fsmCom;
        internal Camera2DMovingComponent FSMCom => fsmCom;

        // Shake
        Camera2DShakeComponent shakeComponent;
        internal Camera2DShakeComponent ShakeComponent => shakeComponent;

        internal Camera2DEntity() {
            fsmCom = new Camera2DMovingComponent();
            deadZoneComponent = new Camera2DDeadZoneComponent();
            softZoneComponent = new Camera2DDeadZoneComponent();
            shakeComponent = new Camera2DShakeComponent();
        }

        // ID
        internal void SetID(int id) {
            this.id = id;
        }

        // Pos
        internal void SetPos(Vector2 pos) {
            this.pos = pos;
        }

        // DeadZone
        public void SetDeadZone(Vector2 deadZoneNormalizedSize, Vector2 viewSize) {
            deadZoneComponent.Zone_Set(deadZoneNormalizedSize, viewSize);
        }
        public Vector2 GetDeadZoneScreenDiff(Vector2 screenPos) {
            return deadZoneComponent.ScreenDiff_Get(screenPos);
        }

        public Vector2 GetDeadZoneSize() {
            return deadZoneComponent.DeadZoneScreenMax - deadZoneComponent.DeadZoneScreenMin;
        }

        public bool IsDeadZoneEnable() {
            return deadZoneComponent.Enable;
        }

        public void EnableDeadZone(bool enable) {
            deadZoneComponent.Enable_Set(enable);
        }

        // SoftZone
        public void SetSoftZone(Vector2 softZoneNormalizedSize, Vector2 viewSize, float dampingFactor) {
            softZoneComponent.Zone_Set(softZoneNormalizedSize, viewSize);
            this.softZoneDampingFactor = dampingFactor;
        }

        public Vector2 GetSoftZoneScreenDiff(Vector2 screenPos) {
            return softZoneComponent.ScreenDiff_Get(screenPos);
        }

        public Vector2 GetSoftZoneSize() {
            return softZoneComponent.DeadZoneScreenMax - softZoneComponent.DeadZoneScreenMin;
        }

        public bool IsSoftZoneEnable() {
            return softZoneComponent.Enable;
        }

        public void EnableSoftZone(bool enable) {
            softZoneComponent.Enable_Set(enable);
        }

        // Confiner
        public void SetConfiner(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerComponent = new Camera2DConfinerComponent(confinerWorldMax, confinerWorldMin);
        }

        internal Vector2 ClampByConfiner(Vector2 pos, float orthographicSize, float aspect) {
            return confinerComponent.Clamp(pos, orthographicSize, aspect);
        }

        public Vector2 GetConfinerCenter() {
            return (confinerComponent.ConfinerWorldMax + confinerComponent.ConfinerWorldMin) / 2f;
        }

        public Vector2 GetConfinerSize() {
            return confinerComponent.ConfinerWorldMax - confinerComponent.ConfinerWorldMin;
        }

        // Shake
        public void ShakeOnce(float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            shakeComponent.ShakeOnce(frequency, amplitude, duration, type, mode);
        }

    }

}