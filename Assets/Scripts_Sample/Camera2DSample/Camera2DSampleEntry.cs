using MortiseFrame.Abacus.Extension;
using UnityEngine;

namespace MortiseFrame.Vista.Sample {

    public class Camera2DSampleEntry : MonoBehaviour {

        CameraInfraContext ctx;
        [SerializeField] Vector2 cameraOriginPos;
        [SerializeField] Vector2 confinerSize;
        [SerializeField] Vector2 confinerPos;
        [SerializeField] Vector2 deadZoneSize;
        [SerializeField] Vector2 deadZonePos;
        [SerializeField] Vector2 viewSize;

        void Start() {
            ctx = new CameraInfraContext();
            ctx.CreateCamera2D(cameraOriginPos.ToFVector2(), confinerSize.ToFVector2(), confinerPos.ToFVector2(), deadZoneSize.ToFVector2(), deadZonePos.ToFVector2(), viewSize.ToFVector2());
        }

        void Update() {
            var dt = Time.deltaTime;
            CameraInfra.Tick(ctx, dt);
        }

    }

}