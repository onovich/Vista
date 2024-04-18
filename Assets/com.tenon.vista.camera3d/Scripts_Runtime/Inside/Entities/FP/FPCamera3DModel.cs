using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    // 第一人称相机
    internal class FPCamera3DModel : ICamera3D {

        // Mode
        Camera3DMode ICamera3D.Mode => Camera3DMode.FPCamera;

        // Pos
        internal Vector3 pos;
        Vector3 ICamera3D.Pos => pos;

        // Shake
        internal Camera3DShakeComponent shakeComponent;
        Camera3DShakeComponent ICamera3D.ShakeComponent => shakeComponent;

    }

}