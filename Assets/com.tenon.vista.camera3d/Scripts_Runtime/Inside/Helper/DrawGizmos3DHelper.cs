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
            if (camera.deadZone.IsEnable) {
                var color = Color.red;
                var lb = camera.deadZone.LB;
                var size = camera.deadZone.Size;
                DrawBox(lb, size, color);
            }

            // SoftZone
            if (camera.softZone.IsEnable) {
                var color = Color.blue;
                var lb = camera.softZone.LB;
                var size = camera.softZone.Size;
                DrawBox(lb, size, color);
            }

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