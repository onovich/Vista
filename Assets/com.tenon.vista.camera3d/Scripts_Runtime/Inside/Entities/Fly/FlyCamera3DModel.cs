using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    // 飞行相机
    internal class FlyCamera3DModel : ICamera3D {

        // Mode
        Camera3DMode ICamera3D.Mode => Camera3DMode.FlyCamera;

        // Pos
        internal Vector3 pos;
        Vector3 ICamera3D.Pos => pos;

        // Shake
        internal Camera3DShakeComponent shakeComponent;
        Camera3DShakeComponent ICamera3D.ShakeComponent => shakeComponent;

    }

}