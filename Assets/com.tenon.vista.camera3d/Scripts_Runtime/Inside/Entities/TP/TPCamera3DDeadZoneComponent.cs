using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class TPCamera3DDeadZoneComponent {

        bool enable;
        internal bool IsEnable => enable;

        Vector2 deadZoneFOV;
        internal Vector2 DeadZoneFOV => deadZoneFOV;

        internal TPCamera3DDeadZoneComponent() {
            enable = false;
        }

        internal void Zone_Set(Vector2 deadZoneFOV) {
            this.deadZoneFOV = deadZoneFOV;
            enable = true;
        }

        internal void Enable_Set(bool enable) {
            this.enable = enable;
        }

        Vector2 CalculateOffsetOutOfDeadZone(in TRS3DModel person, in TRS3DModel camera) {
            // 计算相机到角色的方向向量
            Vector3 toCharacterDirection = (person.t - camera.t).normalized;
            Vector3 cameraForward = camera.forward;

            // 计算水平和垂直死区角度
            float horizontalDeadZoneAngle = deadZoneFOV.x / 2;
            float verticalDeadZoneAngle = deadZoneFOV.y / 2;

            // 计算水平和垂直方向的夹角
            float horizontalAngle = Mathf.Acos(Vector3.Dot(toCharacterDirection, cameraForward) / toCharacterDirection.magnitude) * Mathf.Rad2Deg;
            float verticalAngle = Mathf.Asin((person.t.y - camera.t.y) / Vector3.Distance(camera.t, person.t)) * Mathf.Rad2Deg;

            // 计算偏移角度
            float horizontalOffsetAngle = Mathf.Max(0, Mathf.Abs(horizontalAngle) - horizontalDeadZoneAngle);
            float verticalOffsetAngle = Mathf.Max(0, Mathf.Abs(verticalAngle) - verticalDeadZoneAngle);

            // 将偏移角度转换为实际偏移距离
            float distanceToCharacter = Vector3.Distance(camera.t, person.t);
            float horizontalOffset = Mathf.Tan(horizontalOffsetAngle * Mathf.Deg2Rad) * distanceToCharacter;
            float verticalOffset = Mathf.Tan(verticalOffsetAngle * Mathf.Deg2Rad) * distanceToCharacter;

            return new Vector2(horizontalOffset, verticalOffset);
        }

    }

}