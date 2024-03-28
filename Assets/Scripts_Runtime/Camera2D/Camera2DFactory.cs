using MortiseFrame.Abacus;

namespace MortiseFrame.Vista {

    public static class Camera2DFactory {

        public static Camera2DEntity CreateCamera2D(Camera2DContext ctx, FVector2 pos, FVector2 confinerSize, FVector2 confinerPos, FVector2 deadZoneSize, FVector2 deadZonePos, FVector2 viewSize) {
            var id = ctx.IDService.PickCameraID();
            var confiner = new Bounds(confinerPos, confinerSize);
            VLog.Log("CreateCamera2D confiner: " + confinerSize.ToString());
            var deadZone = new Bounds(deadZonePos, deadZoneSize);
            var viewSizeBound = new Bounds(pos, viewSize);
            var camera = new Camera2DEntity(id, pos, confiner, deadZone, viewSizeBound);
            return camera;
        }

    }

}