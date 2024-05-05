using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal class Camera2DEntity {

        // ID
        int id;
        internal int ID => id;

        // Pos
        Vector2 pos;
        internal Vector2 Pos => pos;

        float z;
        internal float Z => z;

        // Rotation
        float rot;
        internal float Rot => rot;

        // Size
        float size;
        internal float Size => size;

        // AspectRatio
        float aspect;
        internal float Aspect => aspect;

        // Confiner
        Camera2DConfinerComponent confinerComponent;

        // DeadZone
        Camera2DDeadZoneComponent deadZoneComponent;
        Camera2DDeadZoneComponent softZoneComponent;
        Vector2 softZoneDampingFactor = Vector2.zero;
        internal Vector2 SoftZoneDampingFactor => softZoneDampingFactor;

        // FSM
        Camera2DMovingComponent fsmCom;
        internal Camera2DMovingComponent FSMCom => fsmCom;

        // Follow
        Vector2 driverPos;
        public Vector2 DriverPos => driverPos;

        Vector2 lastFrameDriverPos;
        public Vector2 LastFrameDriverPos => lastFrameDriverPos;

        // Shake
        Camera2DShakeComponent shakeComponent;
        internal Camera2DShakeComponent ShakeComponent => shakeComponent;

        internal Camera2DEntity(Vector3 pos, float rot, float size, float aspect, Vector2 driverPos) {
            fsmCom = new Camera2DMovingComponent();
            deadZoneComponent = new Camera2DDeadZoneComponent();
            softZoneComponent = new Camera2DDeadZoneComponent();
            shakeComponent = new Camera2DShakeComponent();
            this.pos = pos;
            this.z = pos.z;
            SetRotation(rot);
            SetSize(size);
            SetAspectRatio(aspect);
            lastFrameDriverPos = driverPos;
            this.driverPos = driverPos;
        }

        // Driver
        internal void RecordDriverPos(Vector2 pos) {
            lastFrameDriverPos = driverPos;
            driverPos = pos;
        }

        // ID
        internal void SetID(int id) {
            this.id = id;
        }

        // Pos
        internal void SetPos(Vector2 pos) {
            this.pos = pos;
        }

        // Rotation
        internal void SetRotation(float rotation) {
            this.rot = rotation;
        }

        // Size
        internal void SetSize(float size) {
            this.size = size;
        }

        // AspectRatio
        internal void SetAspectRatio(float aspectRatio) {
            this.aspect = aspectRatio;
        }

        // DeadZone
        internal void SetDeadZone(Vector2 deadZoneNormalizedSize, Vector2 screenSize) {
            deadZoneComponent.Zone_Set(deadZoneNormalizedSize, screenSize);
        }
        internal Vector2 GetDeadZoneScreenDiff(Vector2 screenPoint) {
            return deadZoneComponent.ScreenDiff_Get(screenPoint);
        }

        internal Vector2 GetDeadZoneLT() {
            return deadZoneComponent.LT;
        }

        internal Vector2 GetDeadZoneRB() {
            return deadZoneComponent.RB;
        }

        internal Vector2 GetDeadZoneLB() {
            return deadZoneComponent.LB;
        }

        internal Vector2 GetDeadZoneRT() {
            return deadZoneComponent.RT;
        }

        internal bool IsDeadZoneEnable() {
            return deadZoneComponent.Enable;
        }

        internal void EnableDeadZone(bool enable) {
            deadZoneComponent.Enable_Set(enable);
        }

        // SoftZone
        internal void SetSoftZone(Vector2 softZoneNormalizedSize, Vector2 screenSize, Vector2 dampingFactor) {
            softZoneComponent.Zone_Set(softZoneNormalizedSize, screenSize);
            this.softZoneDampingFactor = dampingFactor;
        }

        internal Vector2 GetSoftZoneScreenDiff(Vector2 screenPoint) {
            return softZoneComponent.ScreenDiff_Get(screenPoint);
        }

        internal Vector2 GetSoftZoneLT() {
            return softZoneComponent.LT;
        }

        internal Vector2 GetSoftZoneRB() {
            return softZoneComponent.RB;
        }

        internal Vector2 GetSoftZoneLB() {
            return softZoneComponent.LB;
        }

        internal Vector2 GetSoftZoneRT() {
            return softZoneComponent.RT;
        }

        internal bool IsSoftZoneEnable() {
            return softZoneComponent.Enable;
        }

        internal void EnableSoftZone(bool enable) {
            softZoneComponent.Enable_Set(enable);
        }

        // Confiner
        internal void SetConfiner(Vector2 confinerWorldMax, Vector2 confinerWorldMin) {
            this.confinerComponent = new Camera2DConfinerComponent(confinerWorldMax, confinerWorldMin);
        }

        internal bool TryClampByConfiner(Vector2 pos, float orthographicSize, float aspect, out Vector2 dst) {
            return confinerComponent.TryClamp(pos, orthographicSize, aspect, out dst);
        }

        internal Vector2 GetConfinerCenter() {
            return (confinerComponent.ConfinerWorldMax + confinerComponent.ConfinerWorldMin) / 2f;
        }

        internal Vector2 GetConfinerSize() {
            return confinerComponent.ConfinerWorldMax - confinerComponent.ConfinerWorldMin;
        }

        // Shake
        internal void ShakeOnce(float frequency, float amplitude, float duration, EasingType type = EasingType.Linear, EasingMode mode = EasingMode.None) {
            shakeComponent.ShakeOnce(frequency, amplitude, duration, type, mode);
        }

    }

}