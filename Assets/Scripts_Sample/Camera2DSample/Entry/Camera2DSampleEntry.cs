using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        MainContext ctx;
        [SerializeField] Vector2 cameraOriginPos;
        [SerializeField] Vector2 confinerWorldMax;
        [SerializeField] Vector2 confinerWorldMin;
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 softZoneSize;
        [SerializeField] float softZoneDampingFactor;

        [SerializeField] RoleEntity role;
        [SerializeField] Transform[] targets;
        [SerializeField] Panel_2DSampleNavigation navPanel;

        int targetIndex = 0;
        int cameraState = 0;

        void Start() {
            VLog.Log = Debug.Log;
            VLog.Warning = Debug.LogWarning;
            VLog.Error = Debug.LogError;

            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

            var viewSize = new Vector2(Screen.width, Screen.height);
            ctx = new MainContext(mainCamera, viewSize);
            var camera = CameraInfra.CreateMainCamera(ctx, cameraOriginPos, confinerWorldMax, confinerWorldMin);
            CameraInfra.SetCurrentCamera(ctx, ctx.mainCamera);
            camera.SetDeadZone(deadZoneSize, viewSize);
            camera.SetSoftZone(softZoneSize, viewSize, softZoneDampingFactor);
            camera.EnableDeadZone(true);
            camera.EnableSoftZone(true);
            ctx.SetRole(role);

            CameraInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);

            Binding();
            RefreshInfo(camera);

            LogicBusiness.EnterGame(ctx);
        }

        void Binding() {
            var camera = ctx.mainCamera;
            navPanel.action_enableDeadZone = () => {
                camera.EnableDeadZone(true);
                RefreshInfo(camera);
            };
            navPanel.action_disableDeadZone = () => {
                camera.EnableDeadZone(false);
                RefreshInfo(camera);
            };
            navPanel.action_enableSoftZone = () => {
                camera.EnableSoftZone(true);
                RefreshInfo(camera);
            };
            navPanel.action_disableSoftZone = () => {
                camera.EnableSoftZone(false);
                RefreshInfo(camera);
            };
            navPanel.action_followDriver = () => {
                CameraInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
                cameraState = 0;
                RefreshInfo(camera);
            };
            navPanel.action_moveToNextTarget = () => {
                targetIndex = GetNextTargetIndex(targetIndex);
                var target = targets[targetIndex];
                CameraInfra.SetMoveToTarget(ctx, target.position, 1f);
                cameraState = 1;
                RefreshInfo(camera);
            };
        }

        void RefreshInfo(Camera2DEntity camera) {
            var deadZoneEnable = camera.IsDeadZoneEnable();
            var softZoneEnable = camera.IsSoftZoneEnable();

            if (cameraState == 0) {
                if (!deadZoneEnable) {
                    navPanel.SetInfoTxt("DeadZone 禁用时: 硬跟随 Driver");
                }

                if (deadZoneEnable && softZoneEnable) {
                    navPanel.SetInfoTxt("DeadZone 激活, 且 SoftZone 激活时: 在 DeadZone 内不跟随, SoftZone 内阻尼跟随, SoftZone 外硬跟随");
                }

                if (deadZoneEnable && !softZoneEnable) {
                    navPanel.SetInfoTxt("DeadZone 激活, 且 SoftZone 禁用时: 在 DeadZone 内不跟随, DeadZone 外硬跟随");
                }
            }

            if (cameraState == 1) {
                navPanel.SetInfoTxt("缓动跟随 Target 时, 不受 DeadZone 和 SoftZone 影响");
            }
        }

        int GetNextTargetIndex(int current) {
            return (current + 1) % targets.Length;
        }

        void Unbinding() {
            navPanel.action_enableDeadZone = null;
            navPanel.action_disableDeadZone = null;
            navPanel.action_enableSoftZone = null;
            navPanel.action_disableSoftZone = null;
            navPanel.action_followDriver = null;
            navPanel.action_moveToNextTarget = null;
        }

        void Update() {
            var dt = Time.deltaTime;
            LogicBusiness.ProcessInput(ctx);
            LogicBusiness.RoleMove(ctx, dt);
            LogicBusiness.ResetInput(ctx);
        }

        void LateUpdate() {
            var dt = Time.deltaTime;
            CameraInfra.Tick(ctx, dt);
        }

        void OnDestroy() {
            Unbinding();
        }

        void OnDrawGizmos() {
            if (ctx == null || ctx.core == null || ctx.mainCamera == null) {
                return;
            }
            CameraInfra.DrawGizmos(ctx);
        }

    }

}