using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal interface ICamera3D {

        TRS3DModel TRS { get; }
        Camera3DShakeComponent ShakeCom { get; }
        Camera3DMode Mode { get; }
        Matrix4x4 GetViewMatrix();
        Matrix4x4 GetProjectionMatrix();

    }

}