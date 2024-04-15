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

        // Rotate
        Quaternion rotation;
        public Quaternion Rotation => rotation;

        // Confiner
        Camera3DConfinerComponent confinerComponent;

        // Transposer
        Camera3DTransposerComponent transposerComponent;
        internal Camera3DTransposerComponent TransposerComponent => transposerComponent;
        internal Vector3 Transposer_SoftZone_DampingFactor => transposerComponent.DampingFactor;

        // Driver
        Transform driver;
        public Transform Driver => driver;

        Vector3 followPointLocalOffset;
        public Vector3 FollowPointLocalOffset => followPointLocalOffset;

        // Composer
        Camera3DComposerComponent composerComponent;
        internal Camera3DComposerComponent ComposerComponent => composerComponent;
        internal Vector3 Composer_SoftZone_DampingFactor => composerComponent.SoftZoneDampingFactor;

        // FSM
        Camera3DMovingComponent fsmCom;
        internal Camera3DMovingComponent FSMCom => fsmCom;

        // Shake
        Camera3DShakeComponent shakeComponent;
        internal Camera3DShakeComponent ShakeComponent => shakeComponent;

        internal Camera3DEntity() {
            fsmCom = new Camera3DMovingComponent();
            transposerComponent = new Camera3DTransposerComponent();
            composerComponent = new Camera3DComposerComponent();
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

        // Rotation
        internal void SetEulerRotation(Vector3 eulerRotation) {
            rotation = Quaternion.Euler(eulerRotation);
        }

        // Rotate
        internal void Rotate(float yaw, float pitch, float roll) {
            var eulerRotation = new Vector3(pitch, yaw, roll);
            var quaterRotation = Quaternion.Euler(eulerRotation);
            rotation = quaterRotation;
        }

        // Driver
        internal void SetDriver(Transform driver) {
            this.driver = driver;
            fsmCom.EnterMovingByDriver(driver);
        }

        internal void SetDriverFollowPointOffset(Vector3 followPointLocalOffset) {
            this.followPointLocalOffset = followPointLocalOffset;
        }

        internal Vector3 GetDriverWorldFollowPoint() {
            Vector3 worldOffset = driver.TransformDirection(followPointLocalOffset);
            return driver.position + worldOffset;
        }

        // Transposer
        internal void SetTransposerDampingFactor(Vector3 dampingFactor) {
            transposerComponent.DampingFactor_Set(dampingFactor);
        }

        // Composer
        internal void SetComposerSoftZoneDampingFactor(Vector3 dampingFactor) {
            composerComponent.SoftZoneDampingFactor_Set(dampingFactor);
        }

        // Confiner
        internal void SetConfiner(Vector3 confinerWorldMax, Vector3 confinerWorldMin) {
            this.confinerComponent = new Camera3DConfinerComponent(confinerWorldMax, confinerWorldMin);
        }

        internal bool TryClampByConfiner(Camera camera, Vector3 pos, float fov, float aspect, out Vector3 dst) {
            return confinerComponent.TryClamp(camera, pos, fov, aspect, out dst);
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