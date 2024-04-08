using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DEntity {

        // ID
        int id;
        internal int ID => id;

        // Pos
        Vector3 pos;
        internal Vector3 Pos => pos;

        // Confiner
        Camera3DConfinerComponent confinerComponent;

        // DeadZone
        Camera3DDeadZoneComponent deadZoneComponent;
        Camera3DDeadZoneComponent softZoneComponent;
        float softZoneDampingFactor = 0f;
        internal float SoftZoneDampingFactor => softZoneDampingFactor;

        // FSM
        Camera3DMovingComponent fsmCom;
        internal Camera3DMovingComponent FSMCom => fsmCom;

        // Shake
        Camera3DShakeComponent shakeComponent;
        internal Camera3DShakeComponent ShakeComponent => shakeComponent;

        internal Camera3DEntity() {
            fsmCom = new Camera3DMovingComponent();
            deadZoneComponent = new Camera3DDeadZoneComponent();
            softZoneComponent = new Camera3DDeadZoneComponent();
            shakeComponent = new Camera3DShakeComponent();
        }

        // ID
        internal void SetID(int id) {
            this.id = id;
        }

        // Pos
        internal void SetPos(Vector3 pos) {
            this.pos = pos;
        }

        // DeadZone
        internal void SetDeadZone(Vector3 deadZoneNormalizedSize, Vector3 viewSize) {
            deadZoneComponent.Zone_Set(deadZoneNormalizedSize, viewSize);
        }
        internal Vector3 GetDeadZoneScreenDiff(Vector3 screenPos) {
            return deadZoneComponent.ScreenDiff_Get(screenPos);
        }

        internal Vector3 GetDeadZoneSize() {
            return deadZoneComponent.DeadZoneScreenMax - deadZoneComponent.DeadZoneScreenMin;
        }

        internal bool IsDeadZoneEnable() {
            return deadZoneComponent.Enable;
        }

        internal void EnableDeadZone(bool enable) {
            deadZoneComponent.Enable_Set(enable);
        }

        // SoftZone
        internal void SetSoftZone(Vector3 softZoneNormalizedSize, Vector3 viewSize, float dampingFactor) {
            softZoneComponent.Zone_Set(softZoneNormalizedSize, viewSize);
            this.softZoneDampingFactor = dampingFactor;
        }

        internal Vector3 GetSoftZoneScreenDiff(Vector3 screenPos) {
            return softZoneComponent.ScreenDiff_Get(screenPos);
        }

        internal Vector3 GetSoftZoneSize() {
            return softZoneComponent.DeadZoneScreenMax - softZoneComponent.DeadZoneScreenMin;
        }

        internal bool IsSoftZoneEnable() {
            return softZoneComponent.Enable;
        }

        internal void EnableSoftZone(bool enable) {
            softZoneComponent.Enable_Set(enable);
        }

        // Confiner
        internal void SetConfiner(Vector3 confinerWorldMax, Vector3 confinerWorldMin) {
            this.confinerComponent = new Camera3DConfinerComponent(confinerWorldMax, confinerWorldMin);
        }

        internal bool TryClampByConfiner(Vector3 pos, float orthographicSize, float aspect, out Vector3 dst) {
            return confinerComponent.TryClamp(pos, orthographicSize, aspect, out dst);
        }

        internal Vector3 GetConfinerCenter() {
            return (confinerComponent.ConfinerWorldMax + confinerComponent.ConfinerWorldMin) / 2f;
        }

        internal Vector3 GetConfinerSize() {
            return confinerComponent.ConfinerWorldMax - confinerComponent.ConfinerWorldMin;
        }

        // Shake
        internal void ShakeOnce(float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            shakeComponent.ShakeOnce(frequency, amplitude, duration, type, mode);
        }

    }

}