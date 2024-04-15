using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DComposerPhase {

        internal static void FSMTick(Camera3DContext ctx, float dt) {

            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            var status = fsmCom.Status;

            if (current == null) {
                return;
            }

            if (!ctx.ConfinerIsVaild) {
                return;
            }

            if (status == Camera3DMovingStatus.MovingByDriver) {
                TickMovingByDriver(ctx, dt);
                return;
            }

        }

        static void TickMovingByDriver(Camera3DContext ctx, float dt) {
            var current = ctx.CurrentCamera;
            var fsmCom = current.FSMCom;
            if (fsmCom.MovingByDriver_isEntering) {
                fsmCom.MovingByDriver_isEntering = false;
            }

            var driver = fsmCom.MovingByDriver_driver;
            if (driver == null) {
                fsmCom.EnterIdle();
                return;
            }

            var mainCamera = ctx.MainCamera;
            var driverWorldPos = driver.position;

            LookAtDriverWithComposer(ctx, current.ID, mainCamera, driverWorldPos, dt);
        }

        // Aim Driver
        static void LookAtDriverWithComposer(Camera3DContext ctx, int id, Camera mainCamera, Vector3 driverWorldPos, float deltaTime) {

        }

    }

}