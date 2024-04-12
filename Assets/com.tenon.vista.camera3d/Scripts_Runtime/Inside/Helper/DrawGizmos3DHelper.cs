using System;
using UnityEngine;
using MortiseFrame.Swing;
using UnityEditor;

namespace TenonKit.Vista.Camera3D {

    internal static class DrawGizmos3DHelper {

        internal static void DrawGizmos(Camera3DContext ctx, Camera mainCamera) {
            var camera = ctx.CurrentCamera;
            var viewSize = ctx.ViewSize;

            // Confiner 是世界坐标,不会跟随相机动
            Gizmos.color = Color.green;
            var confinerCenter = camera.GetConfinerCenter();
            var confinerSize = camera.GetConfinerSize();
            Gizmos.DrawWireCube(confinerCenter, confinerSize);

            // Composer
            //// DeadZone, SoftZone 是屏幕坐标
            if (camera.Composer_DeadZone_IsEnable()) {
                var deadZoneScreenSize = camera.Composer_DeadZone_GetSize();
                DrawBox(mainCamera, deadZoneScreenSize / 2, viewSize, Color.red);
            }
            if (camera.Composer_SoftZone_IsEnable()) {
                var softZoneScreenSize = camera.Composer_SoftZone_GetSize();
                DrawBox(mainCamera, softZoneScreenSize / 2, viewSize, Color.blue);
            }

        }

        static void DrawBox(Camera camera, Vector2 screenPos, Vector2 screenSize, Color color) {
            Gizmos.color = color;

            // 将屏幕位置调整为左上角的基础上进行计算
            Vector2 adjustedLeftTopScreenPos = new Vector2(screenPos.x - screenSize.x / 2, screenPos.y + screenSize.y / 2);
            Vector2 adjustedRightTopScreenPos = new Vector2(screenPos.x + screenSize.x / 2, screenPos.y + screenSize.y / 2);
            Vector2 adjustedRightBottomScreenPos = new Vector2(screenPos.x + screenSize.x / 2, screenPos.y - screenSize.y / 2);
            Vector2 adjustedLeftBottomScreenPos = new Vector2(screenPos.x - screenSize.x / 2, screenPos.y - screenSize.y / 2);

            // 使用调整后的屏幕位置来转换为世界坐标
            Vector3 leftTop = camera.ScreenToWorldPoint(new Vector3(adjustedLeftTopScreenPos.x, adjustedLeftTopScreenPos.y, camera.nearClipPlane));
            Debug.Log("leftTop: " + leftTop + " adjustedLeftTopScreenPos: " + adjustedLeftTopScreenPos + " screenPos: " + screenPos + " screenSize: " + screenSize + " camera.nearClipPlane: " + camera.nearClipPlane);
            Vector3 rightTop = camera.ScreenToWorldPoint(new Vector3(adjustedRightTopScreenPos.x, adjustedRightTopScreenPos.y, camera.nearClipPlane));
            Vector3 rightBottom = camera.ScreenToWorldPoint(new Vector3(adjustedRightBottomScreenPos.x, adjustedRightBottomScreenPos.y, camera.nearClipPlane));
            Vector3 leftBottom = camera.ScreenToWorldPoint(new Vector3(adjustedLeftBottomScreenPos.x, adjustedLeftBottomScreenPos.y, camera.nearClipPlane));

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);

        }

    }

}