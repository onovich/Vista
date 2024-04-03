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

            LogicBusiness.EnterGame(ctx);
        }

        void Binding() {
            var camera = ctx.mainCamera;
            navPanel.action_enableDeadZone = () => {
                camera.EnableDeadZone(true);
            };
            navPanel.action_disableDeadZone = () => {
                camera.EnableDeadZone(false);
            };
            navPanel.action_enableSoftZone = () => {
                camera.EnableSoftZone(true);
            };
            navPanel.action_disableSoftZone = () => {
                camera.EnableSoftZone(false);
            };
            navPanel.action_followDriver = () => {
                CameraInfra.SetMoveByDriver(ctx, ctx.roleEntity.transform);
            };
            navPanel.action_moveToNextTarget = () => {
                targetIndex = GetNextTargetIndex(targetIndex);
                var target = targets[targetIndex];
                CameraInfra.SetMoveToTarget(ctx, target.position, 1f);
            };
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