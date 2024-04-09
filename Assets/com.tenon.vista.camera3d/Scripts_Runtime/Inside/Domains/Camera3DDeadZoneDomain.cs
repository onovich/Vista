using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DDeadZoneDomain {

        // DeadZone
        internal static void SetDeadZone(Camera3DContext ctx, int id, Vector3 normalizedSize, Vector3 offset) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetDeadZone(normalizedSize, ctx.ViewSize);
        }

        internal static void EnableDeadZone(Camera3DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"EnableDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableDeadZone(enable);
        }

        internal static bool IsDeadZoneEnable(Camera3DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"IsDeadZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsDeadZoneEnable();
        }

        // SoftZone
        internal static void SetSoftZone(Camera3DContext ctx, int id, Vector3 normalizedSize, Vector3 offset, float dampingFactor) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"SetSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetSoftZone(normalizedSize, ctx.ViewSize, dampingFactor);
        }

        internal static void EnableSoftZone(Camera3DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"EnableSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableSoftZone(enable);
        }

        internal static bool IsSoftZoneEnable(Camera3DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V3Log.Error($"IsSoftZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsSoftZoneEnable();
        }

    }

}