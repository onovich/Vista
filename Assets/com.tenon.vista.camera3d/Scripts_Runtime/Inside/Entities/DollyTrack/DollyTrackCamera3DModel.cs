using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    // 轨道相机
    internal class DollyTrackCamera3DModel : ICamera3D {

        // Mode
        Camera3DMode ICamera3D.Mode => Camera3DMode.DollyTrackCamera;

        // Pos
        internal Vector3 pos;
        Vector3 ICamera3D.Pos => pos;

        // Shake
        internal Camera3DShakeComponent shakeComponent;
        Camera3DShakeComponent ICamera3D.ShakeComponent => shakeComponent;

    }

}