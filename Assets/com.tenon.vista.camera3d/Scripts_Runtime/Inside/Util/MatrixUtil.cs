using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    public static class MatrixUtil {

        // TRS
        internal static TRS3DComponent ApplyTRSWithOffset(in TRS3DComponent src, in TRS3DComponent offset) {
            Matrix4x4 m = Matrix4x4.identity;
            m.SetTRS(src.t, src.r, src.s);
            TRS3DComponent dst = new TRS3DComponent(
                m.MultiplyPoint(offset.t),
                src.r * offset.r,
                new Vector3(
                    src.s.x * offset.s.x,
                    src.s.y * offset.s.y,
                    src.s.z * offset.s.z
                )
            );

            return dst;
        }

        // MVP
        internal static Matrix4x4 GetModelMatrix(in TRS3DComponent trs) {
            return Matrix4x4.TRS(trs.t, trs.r, trs.s);
        }

        internal static Matrix4x4 GetViewMatrix(ICamera3D camera) {
            return camera.GetViewMatrix();
        }

        internal static Matrix4x4 GetProjectionMatrix(ICamera3D camera) {
            return camera.GetProjectionMatrix();
        }

        // VP
        internal static Vector3 WorldToScreenPoint(ICamera3D camera, Vector3 worldSpacePoint, Vector2 screenSize) {

            // World -> View
            Matrix4x4 viewMatrix = camera.GetViewMatrix();
            Vector3 cameraSpacePoint = viewMatrix * new Vector4(worldSpacePoint.x, worldSpacePoint.y, worldSpacePoint.z, 1);

            // View -> Projection
            Matrix4x4 projectionMatrix = camera.GetProjectionMatrix();
            Vector4 clipSpacePoint = projectionMatrix * cameraSpacePoint;

            // Projection -> NDC
            Vector3 ndcPoint = clipSpacePoint / clipSpacePoint.w;

            // NDC -> ViewPort
            Vector3 viewportPoint = new Vector3(
                (-ndcPoint.x + 1) * 0.5f,
                (-ndcPoint.y + 1) * 0.5f,
                cameraSpacePoint.z
            );

            // ViewPort -> Screen
            Vector3 screenPos = new Vector3(
                viewportPoint.x * screenSize.x,
                viewportPoint.y * screenSize.y,
                viewportPoint.z
            );

            return screenPos;
        }

    }

}