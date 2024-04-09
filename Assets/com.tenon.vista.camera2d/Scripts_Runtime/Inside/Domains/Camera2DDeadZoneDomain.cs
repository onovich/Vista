using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal static class Camera2DDeadZoneDomain {

        internal static void SetDeadZone(Camera2DContext ctx, int id, Vector2 normalizedSize, Vector2 offset) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"SetDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetDeadZone(normalizedSize, ctx.ViewSize);
        }

        internal static void EnableDeadZone(Camera2DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"EnableDeadZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableDeadZone(enable);
        }

        internal static bool IsDeadZoneEnable(Camera2DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"IsDeadZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsDeadZoneEnable();
        }

        // SoftZone
        internal static void SetSoftZone(Camera2DContext ctx, int id, Vector2 normalizedSize, Vector2 offset, float dampingFactor) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"SetSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.SetSoftZone(normalizedSize, ctx.ViewSize, dampingFactor);
        }

        internal static void EnableSoftZone(Camera2DContext ctx, int id, bool enable) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"EnableSoftZone Error, Camera Not Found: ID = {id}");
                return;
            }
            camera.EnableSoftZone(enable);
        }

        internal static bool IsSoftZoneEnable(Camera2DContext ctx, int id) {
            var has = ctx.TryGetCamera(id, out var camera);
            if (!has) {
                V2Log.Error($"IsSoftZoneEnable Error, Camera Not Found: ID = {id}");
                return false;
            }
            return camera.IsSoftZoneEnable();
        }

    }

}