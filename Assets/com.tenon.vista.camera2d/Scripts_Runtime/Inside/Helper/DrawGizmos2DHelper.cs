using System;
using UnityEngine;
using MortiseFrame.Swing;

namespace TenonKit.Vista.Camera2D {

    internal static class DrawGizmos2DHelper {

        internal static void DrawGizmos(Camera2DContext ctx) {
            var camera = ctx.CurrentCamera;

            // Confiner 是世界坐标,不会跟随相机动
            Gizmos.color = Color.green;
            var confinerCenter = camera.GetConfinerCenter();
            var confinerSize = camera.GetConfinerSize();
            Gizmos.DrawWireCube(confinerCenter, confinerSize);

            var screenSize = ctx.ScreenSize;

            // DeadZone, SoftZone 是屏幕坐标
            if (camera.IsDeadZoneEnable()) {
                var lb_scr = camera.GetDeadZoneLB();
                var rt_scr = camera.GetDeadZoneRT();
                var lt_scr = camera.GetDeadZoneLT();
                var rb_scr = camera.GetDeadZoneRB();
                DrawBoxFromScreenPos(camera, screenSize, lb_scr, rt_scr, lt_scr, rb_scr, Color.red);
            }
            if (camera.IsSoftZoneEnable()) {
                var lb_scr = camera.GetSoftZoneLB();
                var rt_scr = camera.GetSoftZoneRT();
                var lt_scr = camera.GetSoftZoneLT();
                var rb_scr = camera.GetSoftZoneRB();
                DrawBoxFromScreenPos(camera, screenSize, lb_scr, rt_scr, lt_scr, rb_scr, Color.blue);
            }
        }

        static void DrawBoxFromScreenPos(Camera2DEntity camera, Vector2 screenSize, Vector2 lb_scr, Vector2 rt_scr, Vector2 lt_scr, Vector2 rb_scr, Color color) {
            Gizmos.color = color;
            var lb = Camera2DMathUtil.ScreenToWorldPoint(camera, lb_scr, screenSize);
            var rt = Camera2DMathUtil.ScreenToWorldPoint(camera, rt_scr, screenSize);
            var lt = Camera2DMathUtil.ScreenToWorldPoint(camera, lt_scr, screenSize);
            var rb = Camera2DMathUtil.ScreenToWorldPoint(camera, rb_scr, screenSize);
            Gizmos.DrawLine(lb, lt);
            Gizmos.DrawLine(lt, rt);
            Gizmos.DrawLine(rt, rb);
            Gizmos.DrawLine(rb, lb);
        }

    }

}