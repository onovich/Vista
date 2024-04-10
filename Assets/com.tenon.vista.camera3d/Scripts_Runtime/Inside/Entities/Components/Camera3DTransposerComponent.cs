
using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DTransposerComponent {

        Camera3DDeadZoneModel deadZoneModel;
        internal Camera3DDeadZoneModel DeadZoneModel => deadZoneModel;

        Camera3DDeadZoneModel softZoneModel;
        internal Camera3DDeadZoneModel SoftZoneModel => softZoneModel;

        Vector3 softZoneDampingFactor = Vector2.zero;
        internal Vector3 SoftZoneDampingFactor => softZoneDampingFactor;

        public Camera3DTransposerComponent() {
            deadZoneModel = new Camera3DDeadZoneModel();
            softZoneModel = new Camera3DDeadZoneModel();
        }

        // DeadZone
        internal void SetDeadZone(Vector2 deadZoneNormalizedSize, Vector2 viewSize) {
            deadZoneModel.Zone_Set(deadZoneNormalizedSize, viewSize);
        }
        internal Vector2 GetDeadZoneScreenDiff(Vector2 screenPos) {
            return deadZoneModel.ScreenDiff_Get(screenPos);
        }

        internal Vector3 GetDeadZoneSize() {
            return deadZoneModel.DeadZoneScreenMax - deadZoneModel.DeadZoneScreenMin;
        }

        internal bool IsDeadZoneEnable() {
            return deadZoneModel.Enable;
        }

        internal void EnableDeadZone(bool enable) {
            deadZoneModel.Enable_Set(enable);
        }

        // SoftZone
        internal void SetSoftZone(Vector2 softZoneNormalizedSize, Vector2 viewSize, Vector3 dampingFactor) {
            softZoneModel.Zone_Set(softZoneNormalizedSize, viewSize);
            this.softZoneDampingFactor = dampingFactor;
        }

        internal Vector2 GetSoftZoneScreenDiff(Vector2 screenPos) {
            return softZoneModel.ScreenDiff_Get(screenPos);
        }

        internal Vector2 GetSoftZoneSize() {
            return softZoneModel.DeadZoneScreenMax - softZoneModel.DeadZoneScreenMin;
        }

        internal bool IsSoftZoneEnable() {
            return softZoneModel.Enable;
        }

        internal void EnableSoftZone(bool enable) {
            softZoneModel.Enable_Set(enable);
        }

    }

}