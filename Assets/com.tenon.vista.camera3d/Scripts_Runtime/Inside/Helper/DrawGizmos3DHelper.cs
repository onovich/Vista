using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class DrawGizmos3DHelper {

        static GUIStyle guiStyle;

        internal static void OnDrawGUI(Camera3DContext ctx, int cameraID) {
            var has = ctx.TryGetTPCamera(cameraID, out var camera);
            if (!has) {
                return;
            }

            // DeadZone
            if (camera.deadZoneCom.IsEnable) {
                var color = Color.red;
                var fov = camera.deadZoneCom.DeadZoneFOV;
                var dist = Vector3.Distance(camera.trs.t, camera.personTRS.t);
                var screenSize = camera.attrCom.screenSize;
                DrawFOVRectangle(camera, dist, screenSize, fov, color);
            }

            // SoftZone
            if (camera.softZoneCom.IsEnable) {
                var color = Color.blue;
                var fov = camera.softZoneCom.DeadZoneFOV;
                var dist = Vector3.Distance(camera.trs.t, camera.personTRS.t);
                var screenSize = camera.attrCom.screenSize;
                DrawFOVRectangle(camera, dist, screenSize, fov, color);
            }

        }

        static void DrawFOVRectangle(TPCamera3DEntity camera, float dist, Vector2 screenSize, Vector2 fov, Color color) {
            float deadZoneVerticalFOV = fov.y;
            float deadZoneHorizontalFOV = fov.x;

            float cameraVerticalFOV = camera.attrCom.fov;
            float cameraHorizontalFOV = cameraVerticalFOV * camera.attrCom.aspectRatio;

            float deadZoneHeight = screenSize.y * (deadZoneVerticalFOV / cameraVerticalFOV);
            float deadZoneWidth = screenSize.x * (deadZoneHorizontalFOV / cameraHorizontalFOV);

            Vector2 bottomLeft = new Vector2((screenSize.x - deadZoneWidth) / 2, (screenSize.y - deadZoneHeight) / 2);
            DrawBox(bottomLeft, new Vector2(deadZoneWidth, deadZoneHeight), color);
        }

        static void DrawBox(Vector2 lb, Vector2 size, Color color) {
            if (guiStyle == null) {
                InitStyle();
            }

            guiStyle.normal.background = MakeTex(2, 2, color);
            guiStyle.border = new RectOffset(0, 0, 0, 0);
            GUI.Box(new Rect(lb.x, lb.y, size.x, size.y), "", guiStyle);
        }

        static void InitStyle() {
            guiStyle = new GUIStyle(GUI.skin.box);
            guiStyle.normal.background = MakeTex(1, 1, Color.white);
        }

        static Texture2D MakeTex(int width, int height, Color col) {
            col = new Color(col.r, col.g, col.b, 0.2f);
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i) {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

    }

}