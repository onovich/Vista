using UnityEngine;

namespace MortiseFrame.Vista {

    public static class Camera2DFactory {

        public static Camera2DEntity CreateCamera2D(Camera2DContext ctx, Vector2 pos, Vector2 confinerSize, Vector2 confinerPos, Vector2 deadZoneSize, Vector2 deadZonePos, Vector2 viewSize) {
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