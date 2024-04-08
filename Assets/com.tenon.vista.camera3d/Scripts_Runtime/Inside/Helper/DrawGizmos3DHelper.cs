using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera3D {

    internal static class DrawGizmos3DHelper {

        internal static void DrawGizmos(Camera3DContext ctx, Camera mainCamera) {
            var camera = ctx.CurrentCamera;

            // Confiner 是世界坐标,不会跟随相机动
            Gizmos.color = Color.green;
            var confinerCenter = camera.GetConfinerCenter();
            var confinerSize = camera.GetConfinerSize();
            Gizmos.DrawWireCube(confinerCenter, confinerSize);

            // DeadZone, SoftZone 是屏幕坐标
            if (camera.IsDeadZoneEnable()) {
                Gizmos.color = Color.red;
                var deadZoneScreenSize = camera.GetDeadZoneSize();
                var deadZoneWorldSize = Camera3DMathUtil.ScreenToWorldSize(Camera.main, deadZoneScreenSize, ctx.ViewSize);
                Gizmos.DrawWireCube((Vector3)camera.Pos, deadZoneWorldSize);
            }
            if (camera.IsSoftZoneEnable()) {
                Gizmos.color = Color.blue;
                var softZoneScreenSize = camera.GetSoftZoneSize();
                var softZoneWorldSize = Camera3DMathUtil.ScreenToWorldSize(Camera.main, softZoneScreenSize, ctx.ViewSize);
                Gizmos.DrawWireCube((Vector3)camera.Pos, softZoneWorldSize);
            }
        }


    }

}