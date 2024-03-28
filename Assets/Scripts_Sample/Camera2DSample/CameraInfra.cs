using MortiseFrame.Abacus;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public static class CameraInfra {

        public static void Tick(CameraInfraContext ctx, float dt) {
            var camera = ctx.mainCamera;
            ctx.core.Tick(camera, dt);
        }

        public static Camera2DEntity CreateCamera2D(FVector2 pos, FVector2 confinerSize, FVector2 confinerPos, FVector2 deadZoneSize, FVector2 deadZonePos, FVector2 viewSize) {
            return Camera2DFactory.CreateCamera2D(new Camera2DContext(), pos, confinerSize, confinerPos, deadZoneSize, deadZonePos, viewSize);
        }

    }

}