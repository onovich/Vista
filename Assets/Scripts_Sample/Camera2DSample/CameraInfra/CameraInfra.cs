using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public static class CameraInfra {

        public static void Tick(MainContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

        public static Camera2DEntity CreateCamera2D(MainContext ctx, Vector2 pos, Vector2 confinerSize, Vector2 confinerPos, Vector2 deadZoneSize, Vector2 deadZonePos, Vector2 viewSize) {
            return ctx.core.CreateCamera2D(pos, confinerSize, confinerPos, deadZoneSize, deadZonePos, viewSize);
        }

    }

}