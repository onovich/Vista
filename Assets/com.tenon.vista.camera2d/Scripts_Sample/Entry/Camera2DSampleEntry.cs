using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera2D.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        Main2DContext ctx;

        [Header("Camera2D Config")]
        [SerializeField] Camera mainCamera;

        [Header("Confiner Config")]
        [SerializeField] Vector2 confinerWorldMax;
        [SerializeField] Vector2 confinerWorldMin;

        [Header("DeadZone Config")]
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 softZoneSize;
        [SerializeField] Vector2 softZoneDampingFactor;

        [Header("Driver Config")]
        [SerializeField] Role2DEntity role;

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
            V2Log.Log = Debug.Log;
            V2Log.Warning = Debug.LogWarning;
            V2Log.Error = Debug.LogError;

            var screenSize = new Vector2(Screen.width, Screen.height);
            ctx = new Main2DContext(screenSize);
            var cameraID = Camera2DInfra.CreateMainCamera(ctx,
                                                          mainCamera.transform.position,
                                                          mainCamera.transform.rotation.eulerAngles.z,
                                                          mainCamera.orthographicSize,
                                                          mainCamera.aspect,
                                                          confinerWorldMax,
                                                          confinerWorldMin);
            Camera2DInfra.SetCurrentCamera(ctx, ctx.mainCameraID);
            ctx.core.SetDeadZone(ctx.mainCameraID, deadZoneSize, Vector2.zero);
            ctx.core.SetSoftZone(ctx.mainCameraID, softZoneSize, Vector2.zero, softZoneDampingFactor);
            ctx.core.EnableDeadZone(ctx.mainCameraID, true);
            ctx.core.EnableSoftZone(ctx.mainCameraID, true);
            ctx.SetRole(role);

            Camera2DInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);

            Binding();
            RefreshInfo(ctx.mainCameraID);

            Logic2DBusiness.EnterGame(ctx);
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
                Camera2DInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
                cameraState = 0;
                RefreshInfo(cameraID);
            };
            navPanel.action_moveToNextTarget = () => {
                targetIndex = GetNextTargetIndex(targetIndex);
                var target = targets[targetIndex];
                Camera2DInfra.SetMoveToTarget(ctx, target.position, 1f, onComplete: () => {
                    Debug.Log("MoveToTarget Complete");
                });
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

        float restDT = 0;
        void Update() {
            var dt = Time.deltaTime;
            Logic2DBusiness.ProcessInput(ctx);
            float fixInterval = Time.fixedDeltaTime;
            restDT += dt;
            if (restDT <= fixInterval) {
                FixTick(restDT);
                restDT = 0;
            } else {
                while (restDT > fixInterval) {
                    FixTick(fixInterval);
                    restDT -= fixInterval;
                }
            }
            LateTick(dt);
        }

        void FixTick(float dt) {
            Logic2DBusiness.RoleMove(ctx, dt);
            Logic2DBusiness.ResetInput(ctx);
            Physics2D.Simulate(dt);
        }

        void LateTick(float dt) {
            var pos = Camera2DInfra.TickPos(ctx, dt);
            mainCamera.transform.position = pos;
        }

        void OnDestroy() {
            Unbinding();
        }

        void OnDrawGizmos() {
            if (ctx == null || ctx.core == null) {
                return;
            }
            Camera2DInfra.DrawGizmos(ctx);
        }

    }

}