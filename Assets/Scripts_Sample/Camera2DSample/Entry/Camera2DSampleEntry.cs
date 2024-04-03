using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        MainContext ctx;
        [SerializeField] Vector2 cameraOriginPos;
        [SerializeField] Vector2 confinerWorldSize;
        [SerializeField] Vector2 confinerWorldPos;
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 softZoneSize;
        [SerializeField] Vector2 viewSize;

        [SerializeField] RoleEntity role;

        void Start() {
            VLog.Log = Debug.Log;
            VLog.Warning = Debug.LogWarning;
            VLog.Error = Debug.LogError;

            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

            var screenSize = new Vector2(Screen.width, Screen.height);
            ctx = new MainContext(mainCamera, screenSize);
            ctx.CreateMainCamera(cameraOriginPos, confinerWorldSize, confinerWorldPos, deadZoneSize, softZoneSize, viewSize);
            ctx.SetCurrentCamera(ctx.mainCamera);
            ctx.SetRole(role);

            LogicBusiness.EnterGame(ctx);
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

        void OnGUI() {

        }

        void OnDrawGizmos() {
            if (ctx == null || ctx.mainCamera == null) return;
            var camera = ctx.mainCamera;
            var confiner = camera.Confiner;
            var screenSize = new Vector2(Screen.width, Screen.height);
            var deadZoneScreenSize = camera.DeadZone_GetSize();
            var deadZoneWorldSize = PositionUtil.ScreenToWorldSize(Camera.main, deadZoneScreenSize);

            // Confiner 是世界坐标,不会跟随相机动
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube((Vector2)confiner.center, confiner.size);

            // DeadZone, SoftZone, ViewSize 是相对坐标，会随着相机移动
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)camera.Pos, deadZoneWorldSize);
        }

    }

}