using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal static class Camera3DFollowPhase {

        internal static void ApplyAutoFollow(Camera3DContext ctx, TPCamera3DModel camera, float dt) {
            if (camera.followX) {
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }

            bool deadZoneIsEnable = camera.deadZone.IsEnable;
            bool softZoneIsEnable = camera.softZone.IsEnable;
            Vector3 cameraWorldPos = camera.person.position;
            float cameraWorldPosX = cameraWorldPos.x;
            float cameraWorldPosY = cameraWorldPos.y;
            Vector3 targetPos = cameraWorldPos;

            // DeadZone 禁用时: 硬跟随 Driver
            if (!deadZoneIsEnable) {
                ApplyWhenDisableDeadZone(ctx, camera, targetPos, dt);
                return;
            }

            var driverScreenPos = Camera3DMathUtil.WorldToScreenPos(ctx.cameraAgent, camera.person.position);
            var deadZoneScreenDiff = camera.deadZone.ScreenDiff_Get(driverScreenPos);

            // Driver 在 DeadZone 内：不跟随
            if (deadZoneScreenDiff == Vector2.zero && softZoneIsEnable) {
                return;
            }

            // Driver 在 SoftZone 内
            //// - SoftZone 禁用时：硬跟随 DeadZone Diff
            if (!softZoneIsEnable) {
                // var deadZoneScreenDiffHorizontalEnd = new Vector2(deadZoneScreenDiff.x, driverScreenPos.y);
                // var deadZoneScreenDiffEnd = Camera3DMathUtil.ScreenToWorldPos(ctx.cameraAgent, deadZoneScreenDiffHorizontalEnd);
                // var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldLength(ctx.cameraAgent, deadZoneScreenDiff, ctx.viewSize);
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(ctx.cameraAgent, deadZoneScreenDiff, camera.person.position.z);
                targetPos += deadZoneWorldDiff;
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }

            //// - SoftZone 未禁用时：阻尼跟随 DeadZone Diff
            var softZoneScreenDiff = camera.softZone.ScreenDiff_Get(driverScreenPos);
            Debug.Log("DeadZoneDiff: " + deadZoneScreenDiff + " SoftZoneDiff: " + softZoneScreenDiff);

            if (softZoneScreenDiff == Vector2.zero) {
                var deadZoneWorldDiff = Camera3DMathUtil.ScreenToWorldSize(ctx.cameraAgent, deadZoneScreenDiff, camera.person.position.z);
                targetPos += deadZoneWorldDiff;
                TPCamera3DMoveDomain.ApplyFollowXYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
                return;
            }

        }

        static void ApplyWhenDisableDeadZone(Camera3DContext ctx, TPCamera3DModel camera, Vector3 targetPos, float dt) {
            TPCamera3DMoveDomain.ApplyFollowYZ(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
            Camera3DLookAtPhase.ApplyLookAtPerson(ctx, camera.id, ctx.cameraAgent, camera.person, dt);
        }

    }

}