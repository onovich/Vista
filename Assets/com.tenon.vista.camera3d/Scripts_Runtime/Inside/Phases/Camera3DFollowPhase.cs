using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyAutoFollow(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }

            bool deadZoneEnable = camera.deadZone.IsEnable;
            bool softZoneEnable = camera.softZone.IsEnable;
            Vector3 cameraWorldPos = camera.person.position;
            float cameraWorldPosX = cameraWorldPos.x;
            float cameraWorldPosY = cameraWorldPos.y;
            Vector3 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneEnable) {
                targetPos = cameraWorldPos;
                TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }

            var driverScreenPos = Camera3DMathUtil.WorldToScreenPos(ctx.cameraAgent, camera.person.position);
            var deadZoneDiff = camera.deadZone.ScreenDiff_Get(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneDiff == Vector2.zero && softZoneEnable) {
                return;
            }

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            // if (!softZoneEnable) {
            //     var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldLength(ctx.cameraAgent, deadZoneDiff, ctx.viewSize);
            //     targetPos += deadZoneWorldDiff;
            //     TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            //     Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            //     return;
            // }

        }

    }

}