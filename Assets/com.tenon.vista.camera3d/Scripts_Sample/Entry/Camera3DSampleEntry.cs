using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D.Sample {

    public class Camera3DSampleEntry : MonoBehaviour {

        Main3DContext ctx;

        [Header("Follow Mode Config")]
        [SerializeField] bool followX;

        [Header("Dead Zone Config")]
        [SerializeField] Vector2 deadZoneNormalizedSize;

        [Header("Soft Zone Config")]
        [SerializeField] Vector2 softZoneNormalizedSize;

        [Header("DampingFactor Config")]
        [SerializeField] Vector3 followDampingFactor;
        [SerializeField] float lookAtDampingFactor;

        [Header("Person Config")]
        [SerializeField] Role3DEntity person;

        [Header("Manual Pan Config")]
        [SerializeField] Vector3 manualPanSpeed;
        [SerializeField] float manualPanCancleDuration;
        [SerializeField] EasingType manualPanEasingType;
        [SerializeField] EasingMode manualPanEasingMode;

        [Header("Manual Orbit Config")]
        [SerializeField] Vector2 manualOrbitalSpeed;
        [SerializeField] float manualOrbitalCancleDuration;
        [SerializeField] EasingType manualOrbitalEasingType;
        [SerializeField] EasingMode manualOrbitalEasingMode;

        [Header("Shake Config")]
        [SerializeField] float shakeFrequency;
        [SerializeField] float shakeAmplitude;
        [SerializeField] float shakeDuration;
        [SerializeField] EasingType shakeEasingType;
        [SerializeField] EasingMode shakeEasingMode;

        [Header("UI")]
        [SerializeField] Panel_3DSampleNavigation navPanel;

        void Start() {
            V3Log.Log = Debug.Log;
            V3Log.Warning = Debug.LogWarning;
            V3Log.Error = Debug.LogError;

            // Camera Agent
            Camera agent = GameObject.Find("MainCamera").GetComponent<Camera>();

            // Context
            var viewSize = new Vector2(Screen.width, Screen.height);
            ctx = new Main3DContext(agent,
                                    viewSize,
                                    manualPanSpeed,
                                    manualPanCancleDuration,
                                    manualPanEasingType,
                                    manualPanEasingMode,
                                    manualOrbitalSpeed,
                                    manualOrbitalCancleDuration,
                                    manualOrbitalEasingType,
                                    manualOrbitalEasingMode);

            // Person
            ctx.SetPerson(person);

            // Camera
            var cameraOriginPos = agent.transform.position;
            var cameraOriginRot = agent.transform.rotation;
            var cameraOriginFov = agent.fieldOfView;
            var cameraID = Camera3DInfra.CreateTPCamera(ctx, cameraOriginPos, cameraOriginPos, cameraOriginRot, cameraOriginFov, person.transform, followX);

            // Dead Zone
            Camera3DInfra.SetTPCameraDeadZone(ctx, deadZoneNormalizedSize);
            Camera3DInfra.SetTPCameraDeadZoneEnable(ctx, true);

            // Soft Zone
            Camera3DInfra.SetTPCameraSoftZone(ctx, softZoneNormalizedSize);
            Camera3DInfra.SetTPCameraSoftZoneEnable(ctx, true);

            // Damping Factor
            Camera3DInfra.SetTPCameraFollowDamppingFactor(ctx, followDampingFactor);
            Camera3DInfra.SetTPCameraLookAtDamppingFactor(ctx, lookAtDampingFactor);

            Binding();
            RefreshInfo(ctx.mainCameraID);

            Logic3DBusiness.EnterGame(ctx);
        }

        void Binding() {
            var cameraID = ctx.mainCameraID;
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
            navPanel.action_shakeOnce = () => {
                ShakeOnce(cameraID);
            };
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

        // int GetNextTargetIndex(int current) {
        //     return (current + 1) % targets.Length;
        // }

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
            Logic3DBusiness.ProcessInput(ctx);

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

        }

        void FixTick(float dt) {
            Logic3DBusiness.BoxCast(ctx);
            Logic3DBusiness.RoleMove(ctx, dt);
            Logic3DBusiness.RoleJump(ctx);
            Logic3DBusiness.RoleFalling(ctx, dt);

            Physics.Simulate(dt);
        }

        void LateUpdate() {
            var dt = Time.deltaTime;
            Camera3DInfra.Tick(ctx, dt);

            Logic3DBusiness.CameraPan_ApplySet(ctx);
            Logic3DBusiness.CameraPan_Apply(ctx);
            Logic3DBusiness.CameraPan_ApplyCancle(ctx);

            Logic3DBusiness.CameraOrbital_ApplySet(ctx);
            Logic3DBusiness.CameraOrbital_Apply(ctx);
            Logic3DBusiness.CameraOrbital_ApplyCancle(ctx);
            Logic3DBusiness.ResetInput(ctx);
        }

        void OnDestroy() {
            Unbinding();
        }

        void OnGUI() {
            if (ctx == null || ctx.core == null) {
                return;
            }
            Camera3DInfra.OnDrawGUI(ctx);
        }

    }

}