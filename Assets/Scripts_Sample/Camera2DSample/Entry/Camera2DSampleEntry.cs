using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        MainContext ctx;
        [SerializeField] Vector2 cameraOriginPos;
        [SerializeField] Vector2 confinerWorldMax;
        [SerializeField] Vector2 confinerWorldMin;
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
            CameraInfra.CreateMainCamera(ctx, cameraOriginPos, confinerWorldMax, confinerWorldMin);
            CameraInfra.SetCurrentCamera(ctx, ctx.mainCamera);
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
            if (ctx == null || ctx.core == null || ctx.mainCamera == null) {
                return;
            }
            CameraInfra.DrawGizmos(ctx);
        }

    }

}