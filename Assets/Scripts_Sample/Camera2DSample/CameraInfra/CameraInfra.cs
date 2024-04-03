using MortiseFrame.Swing;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public static class CameraInfra {

        public static void Tick(MainContext ctx, float dt) {
            ctx.core.Tick(dt);
        }

    }

}