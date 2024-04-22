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
                var screenPos = camera.deadZone.Center;
                var screenSize = camera.deadZone.Size;
                DrawBox(ctx.cameraAgent, screenPos, screenSize, color);
            }

            // SoftZone
            if (camera.softZone.IsEnable) {
                var color = Color.blue;
                var screenPos = camera.softZone.Center;
                var screenSize = camera.softZone.Size;
                DrawBox(ctx.cameraAgent, screenPos, screenSize, color);
            }

        }

        static void DrawBox(Camera agent, Vector2 screenPos, Vector2 screenSize, Color color) {
            Gizmos.color = color;

            // 将屏幕位置调整为左上角的基础上进行计算
            Vector2 adjustedLeftTopScreenPos = new Vector2(screenPos.x - screenSize.x / 2, screenPos.y + screenSize.y / 2);
            Vector2 adjustedRightTopScreenPos = new Vector2(screenPos.x + screenSize.x / 2, screenPos.y + screenSize.y / 2);
            Vector2 adjustedRightBottomScreenPos = new Vector2(screenPos.x + screenSize.x / 2, screenPos.y - screenSize.y / 2);
            Vector2 adjustedLeftBottomScreenPos = new Vector2(screenPos.x - screenSize.x / 2, screenPos.y - screenSize.y / 2);

            // 使用调整后的屏幕位置来转换为世界坐标
            Vector3 leftTop = agent.ScreenToWorldPoint(new Vector3(adjustedLeftTopScreenPos.x, adjustedLeftTopScreenPos.y, agent.nearClipPlane));
            Vector3 rightTop = agent.ScreenToWorldPoint(new Vector3(adjustedRightTopScreenPos.x, adjustedRightTopScreenPos.y, agent.nearClipPlane));
            Vector3 rightBottom = agent.ScreenToWorldPoint(new Vector3(adjustedRightBottomScreenPos.x, adjustedRightBottomScreenPos.y, agent.nearClipPlane));
            Vector3 leftBottom = agent.ScreenToWorldPoint(new Vector3(adjustedLeftBottomScreenPos.x, adjustedLeftBottomScreenPos.y, agent.nearClipPlane));

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);
        }

    }

}