using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        MainContext ctx;

        [Header("Camera2D Config")]
        [SerializeField] Vector2 cameraOriginPos;

        [Header("Confiner Config")]
        [SerializeField] Vector2 confinerWorldMax;
        [SerializeField] Vector2 confinerWorldMin;

        [Header("DeadZone Config")]
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 softZoneSize;
        [SerializeField] float softZoneDampingFactor;

        [Header("Driver Config")]
        [SerializeField] RoleEntity role;

        [Header("Target Config")]
        [SerializeField] Transform[] targets;

        [Header("Shake Config")]
        [SerializeField] float shakeFrequency;
        [SerializeField] float shakeAmplitude;
        [SerializeField] float shakeDuration;
        [SerializeField] EasingType shakeEasingType;
        [SerializeField] EasingMode shakeEasingMode;

        [Header("UI")]
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
            var cameraID = CameraInfra.CreateMainCamera(ctx, cameraOriginPos, confinerWorldMax, confinerWorldMin);
            CameraInfra.SetCurrentCamera(ctx, ctx.mainCameraID);
            ctx.core.SetDeadZone(ctx.mainCameraID, deadZoneSize, Vector2.zero);
            ctx.core.SetSoftZone(ctx.mainCameraID, softZoneSize, Vector2.zero,softZoneDampingFactor);
            ctx.core.EnableDeadZone(ctx.mainCameraID, true);
            ctx.core.EnableSoftZone(ctx.mainCameraID, true);
            ctx.SetRole(role);

            CameraInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);

            Binding();
            RefreshInfo(ctx.mainCameraID);

            LogicBusiness.EnterGame(ctx);
        }

        void Binding() {
            var cameraID = ctx.mainCameraID;
            navPanel.action_enableDeadZone = () => {
                ctx.core.EnableDeadZone(cameraID, true);
                RefreshInfo(cameraID);
            };
            navPanel.action_disableDeadZone = () => {
                ctx.core.EnableDeadZone(cameraID, false);
                RefreshInfo(cameraID);
            };
            navPanel.action_enableSoftZone = () => {
                ctx.core.EnableSoftZone(cameraID, true);
                RefreshInfo(cameraID);
            };
            navPanel.action_disableSoftZone = () => {
                ctx.core.EnableSoftZone(cameraID, false);
                RefreshInfo(cameraID);
            };
            navPanel.action_followDriver = () => {
                CameraInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
                cameraState = 0;
                RefreshInfo(cameraID);
            };
            navPanel.action_moveToNextTarget = () => {
                targetIndex = GetNextTargetIndex(targetIndex);
                var target = targets[targetIndex];
                CameraInfra.SetMoveToTarget(ctx, target.position, 1f);
                cameraState = 1;
                RefreshInfo(cameraID);
            };
            navPanel.action_shakeOnce = () => {
                ShakeOnce(cameraID);
            };
        }

        void ShakeOnce(int cameraID) {
            ctx.core.ShakeOnce(cameraID, shakeFrequency, shakeAmplitude, shakeDuration, shakeEasingType, shakeEasingMode);
        }

        void RefreshInfo(int cameraID) {
            var deadZoneEnable = ctx.core.IsDeadZoneEnable(cameraID);
            var softZoneEnable = ctx.core.IsSoftZoneEnable(cameraID);

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
            navPanel.action_shakeOnce = null;
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
            if (ctx == null || ctx.core == null) {
                return;
            }
            CameraInfra.DrawGizmos(ctx);
        }

    }

}