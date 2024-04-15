using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Camera3DSampleEntry : MonoBehaviour {

        Main3DContext ctx;

        [Header("Confiner Config")]
        [SerializeField] Vector3 confinerWorldMax;
        [SerializeField] Vector3 confinerWorldMin;

        [Header("Composer Config")]
        [SerializeField] float composer_dampingFactor;

        [Header("Transposer Config")]
        [SerializeField] Vector3 transposer_dampingFactor;

        [Header("Driver Config")]
        [SerializeField] Role3DEntity role;

        [Header("Target Config")]
        [SerializeField] Transform[] targets;

        [Header("Shake Config")]
        [SerializeField] float shakeFrequency;
        [SerializeField] float shakeAmplitude;
        [SerializeField] float shakeDuration;
        [SerializeField] EasingType shakeEasingType;
        [SerializeField] EasingMode shakeEasingMode;

        [Header("UI")]
        [SerializeField] Panel_3DSampleNavigation navPanel;

        int targetIndex = 0;
        int cameraState = 0;

        void Start() {
            V3Log.Log = Debug.Log;
            V3Log.Warning = Debug.LogWarning;
            V3Log.Error = Debug.LogError;

            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

            var viewSize = new Vector2(Screen.width, Screen.height);
            ctx = new Main3DContext(mainCamera, viewSize);
            var cameraOriginPos = mainCamera.transform.position;
            var cameraOriginRot = mainCamera.transform.eulerAngles;
            var cameraID = Camera3DInfra.CreateTrackCamera(ctx, cameraOriginPos, cameraOriginRot, confinerWorldMax, confinerWorldMin, role.transform);
            Camera3DInfra.SetComposerDampingFactor(ctx, composer_dampingFactor);
            Camera3DInfra.SetTransposerDampingFactor(ctx, transposer_dampingFactor);
            Camera3DInfra.SetCurrentCamera(ctx, ctx.mainCameraID);

            ctx.SetRole(role);

            Camera3DInfra.SetDriver(ctx, ctx.roleEntity.transform);

            Binding();
            RefreshInfo(ctx.mainCameraID);

            Logic3DBusiness.EnterGame(ctx);
        }

        void Binding() {
            // var cameraID = ctx.mainCameraID;
            // navPanel.action_enableDeadZone = () => {
            //     ctx.core.EnableDeadZone(cameraID, true);
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_disableDeadZone = () => {
            //     ctx.core.EnableDeadZone(cameraID, false);
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_enableSoftZone = () => {
            //     ctx.core.EnableSoftZone(cameraID, true);
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_disableSoftZone = () => {
            //     ctx.core.EnableSoftZone(cameraID, false);
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_followDriver = () => {
            //     Camera3DInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
            //     cameraState = 0;
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_moveToNextTarget = () => {
            //     targetIndex = GetNextTargetIndex(targetIndex);
            //     var target = targets[targetIndex];
            //     Camera3DInfra.SetMoveToTarget(ctx, target.position, 1f, onComplete: () => {
            //         Debug.Log("MoveToTarget Complete");
            //     });
            //     cameraState = 1;
            //     RefreshInfo(cameraID);
            // };
            // navPanel.action_shakeOnce = () => {
            //     ShakeOnce(cameraID);
            // };
        }

        void ShakeOnce(int cameraID) {
            ctx.core.ShakeOnce(cameraID, shakeFrequency, shakeAmplitude, shakeDuration, shakeEasingType, shakeEasingMode);
        }

        void RefreshInfo(int cameraID) {
            // var deadZoneEnable = ctx.core.IsDeadZoneEnable(cameraID);
            // var softZoneEnable = ctx.core.IsSoftZoneEnable(cameraID);

            // if (cameraState == 0) {
            //     if (!deadZoneEnable) {
            //         navPanel.SetInfoTxt("DeadZone 禁用时: 硬跟随 Driver");
            //     }

            //     if (deadZoneEnable && softZoneEnable) {
            //         navPanel.SetInfoTxt("DeadZone 激活, 且 SoftZone 激活时: 在 DeadZone 内不跟随, SoftZone 内阻尼跟随, SoftZone 外硬跟随");
            //     }

            //     if (deadZoneEnable && !softZoneEnable) {
            //         navPanel.SetInfoTxt("DeadZone 激活, 且 SoftZone 禁用时: 在 DeadZone 内不跟随, DeadZone 外硬跟随");
            //     }
            // }

            // if (cameraState == 1) {
            //     navPanel.SetInfoTxt("缓动跟随 Target 时, 不受 DeadZone 和 SoftZone 影响");
            // }
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
            Logic3DBusiness.ProcessInput(ctx);
        }

        void LateUpdate() {
            var dt = Time.deltaTime;
            Camera3DInfra.Tick(ctx, dt);
        }

        private void FixedUpdate() {
            var dt = Time.fixedDeltaTime;
            Logic3DBusiness.BoxCast(ctx);
            Logic3DBusiness.RoleMove(ctx, dt);
            Logic3DBusiness.RoleJump(ctx);
            Logic3DBusiness.RoleFalling(ctx, dt);
            Logic3DBusiness.ResetInput(ctx);
        }

        void OnDestroy() {
            Unbinding();
        }

        void OnDrawGizmos() {
            if (ctx == null || ctx.core == null) {
                return;
            }
            Camera3DInfra.DrawGizmos(ctx);
        }

    }

}