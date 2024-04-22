using System;
using UnityEngine;
using MortiseFrame.Swing;
using UnityEditor;

namespace TenonKit.Vista.Camera3D {

    internal static class DrawGizmos3DHelper {

        internal static void DrawGizmos(Camera3DContext ctx, int cameraID) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                return;
            }

            // DeadZone
            if (camera.deadZone.IsEnable) {
                var color = Color.red;
                var LT = camera.deadZone.LT;
                var RT = camera.deadZone.RT;
                var RB = camera.deadZone.RB;
                var LB = camera.deadZone.LB;
                DrawBox(ctx.cameraAgent, LT, RT, LB, RB, color);
            }

            // SoftZone
            if (camera.softZone.IsEnable) {
                var color = Color.blue;
                var LT = camera.softZone.LT;
                var RT = camera.softZone.RT;
                var RB = camera.softZone.RB;
                var LB = camera.softZone.LB;
                DrawBox(ctx.cameraAgent, LT, RT, LB, RB, color);
            }

        }

        static void DrawBox(Camera agent, Vector2 LT, Vector2 RT, Vector2 LB, Vector2 RB, Color color) {
            Gizmos.color = color;

            // 使用调整后的屏幕位置来转换为世界坐标
            LT = agent.ScreenToWorldPoint(new Vector3(LT.x, LT.y, agent.nearClipPlane));
            RT = agent.ScreenToWorldPoint(new Vector3(RT.x, RT.y, agent.nearClipPlane));
            RB = agent.ScreenToWorldPoint(new Vector3(RB.x, RB.y, agent.nearClipPlane));
            LB = agent.ScreenToWorldPoint(new Vector3(LB.x, LB.y, agent.nearClipPlane));

            Gizmos.DrawLine(LT, RT);
            Gizmos.DrawLine(RT, RB);
            Gizmos.DrawLine(RB, LB);
            Gizmos.DrawLine(LB, LT);

            Debug.Log("DrawBox: " + LT + " " + RT + " " + LB + " " + RB);
        }

    }

}