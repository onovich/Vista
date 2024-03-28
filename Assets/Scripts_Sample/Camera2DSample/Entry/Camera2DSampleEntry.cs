using MortiseFrame.Abacus.Extension;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        MainContext ctx;
        [SerializeField] Vector2 cameraOriginPos;
        [SerializeField] Vector2 confinerSize;
        [SerializeField] Vector2 confinerPos;
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 deadZonePos;
        [SerializeField] Vector2 viewSize;

        [SerializeField] RoleEntity role;

        void Start() {
            VLog.Log = Debug.Log;
            VLog.Warning = Debug.LogWarning;
            VLog.Error = Debug.LogError;

            ctx = new MainContext();
            ctx.CreateMainCamera(cameraOriginPos.ToFVector2(), confinerSize.ToFVector2(), confinerPos.ToFVector2(), deadZoneSize.ToFVector2(), deadZonePos.ToFVector2(), viewSize.ToFVector2());
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

        void OnDrawGizmos() {
            if (ctx == null || ctx.mainCamera == null) return;
            var camera = ctx.mainCamera;
            var confiner = camera.Confiner;
            var deadZone = camera.DeadZone;
            var viewSize = camera.ViewSize;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(confiner.Center.ToVector3(), confiner.Size.ToVector3());
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(deadZone.Center.ToVector3(), deadZone.Size.ToVector3());
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(viewSize.Center.ToVector3(), viewSize.Size.ToVector3());
        }

    }

}