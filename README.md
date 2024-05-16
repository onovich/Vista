# Vista
Vista, a lightweight 2D camera library, named after "观".<br/>
**Vista，轻量级的 2d 相机库，取名自「观」。**

![](https://github.com/onovich/Vista/blob/main/Assets/com.tenon.vista/Resources_Sample/codecover_visita.png)

Vista provides managed 2D Camera functionality for Unity.<br/>
**Vista 为 Unity 里的 2D Camera 提供托管。**

Vista's underlying system does not hijack any Camera or Transform from the upper layer, ensuring no side effects and allowing safe usage. The upper layer records the virtual camera created by the lower layer in the form of IDs, which can be queried and accessed through the interface.<br/>
**Vista 的底层不会劫持上层的任何 Camera 或 Transform，不会产生副作用，可以放心使用。上层以 id 的形式记录下层创建的虚拟相机，通过接口查询和访问。**

Note that this library is only for 2D cameras. For 3D cameras, please visit [Scape](https://github.com/onovich/Scape)<br/>
**注意，当前库只适用于 2d 相机，3d 相机请移步到 [Scape](https://github.com/onovich/Scape)**

# Readiness
Stable and available.<br/>
**稳定可用。**

# Terminology
### Driver
An upper-level object that the camera can follow. The lower level does not care about who the Driver is, only its coordinates.<br/>
**一切相机可跟随的上层对象，下层不关心 Driver 是谁，只记录它的坐标。**

### Follow Driver
A camera movement mode. When the camera follows the Driver, the logic involves DeadZone, SoftZone, and Damping.<br/>
**相机的一种行动模式。相机跟随 Driver 时，逻辑与 DeadZone、SoftZone、Damping 有关。**

### Move To Target
Another camera movement mode. Here, the Target refers to an isolated coordinate, often used for cinematics, simulating a track camera in a 2D scene. When executing Move To Target, the logic is not related to DeadZone, SoftZone, or Damping.<br/>
**相机的另一种行动模式。这里的 Target 指一个孤立的坐标，这个方法常用于演出，可以模拟 2D 场景中的的轨道相机。执行 Move To Target 时，逻辑与  DeadZone、SoftZone、Damping 无关。**

### DeadZone
A rectangle on the screen where the camera does not follow if the Driver is within it.<br/>
**屏幕上的一个矩形，Driver 在矩形内时，相机不执行跟随。**

### SoftZone
An extension of DeadZone. When the Driver is between the SoftZone and DeadZone, the camera follows with damping delay. Beyond the SoftZone, the camera performs hard follow.<br/>
**DeadZone 的延伸，Driver 在 SoftZone 和 DeadZone 之间时，相机的跟随会带阻尼延时。超出 SoftZone 后，相机会执行硬跟随。**

### Damping
A coefficient between 0 and 1. When set to 0, the camera follows hard; when set to 1, the camera does not follow. Between 0 and 1, the camera follows smoothly, not immediately reaching the target.<br/>
**0-1的一个系数，为0时相机的跟随表现为硬跟随，为1时相机的表现为不跟随，在0-1之间，相机会缓动跟随，不立刻抵达目标。**

### Constraint
The boundaries of the camera, usually the boundaries of the 2D scene. The camera cannot move beyond the boundaries.<br/>
**相机的边界，一般就是 2D 场景的边界。相机的移动不能超出边界。**

# Camera Follow Logic
* When DeadZone is disabled, follow the Driver hard.<br/>
  **DeadZone 禁用时，硬跟随 Driver；**
* When the Driver is within the DeadZone, do not follow.<br/>
  **Driver 在 DeadZone 内时，不跟随；**
* When the Driver is within the SoftZone and the SoftZone is disabled, follow hard based on the excess of the DeadZone.<br/>
  **Driver 在 SoftZone 内时，若 SoftZone 禁用，则根据 DeadZone 的超出量执行硬跟随；**
* When the Driver is within the SoftZone and the SoftZone is enabled, follow with damping based on the excess of the DeadZone.<br/>
  **Driver 在 SoftZone 内时，若 SoftZone 激活，则根据 DeadZone 的超出量执行阻尼跟随；**
* When the Driver is outside the SoftZone, follow hard based on the excess of the SoftZone.<br/>
  **Driver 在 SoftZone 外时，根据 SoftZone 的超出量执行硬跟随。**

# Features
## Implemented
* Follow Driver
* Move To Target
* Check Constraint And Resume
* Shake

## To Be Implemented
* DeadZone Offset
* Zoom
* Pause / UnPause
* Manual Controller
* Transition Between Multiple Cameras

## Not In Plan
* Look At Group
* Composite Shake

# Sample
```
// Create And Init Camera

void Start() {
    var screenSize = new Vector2(Screen.width, Screen.height);
    cameraCore = new Camera2DCore(screenSize);
    mainCameraID = Camera2DInfra.CreateMainCamera(mainCamera.transform.position,
                                                  mainCamera.transform.rotation.eulerAngles.z,
                                                  mainCamera.orthographicSize,
                                                  mainCamera.aspect,
                                                  confinerWorldMax,
                                                  confinerWorldMin,
                                                  role.transform.position);
    cameraCore.SetCurrentCamera(mainCameraID);
    cameraCore.SetDeadZone(ctx.mainCameraID, deadZoneSize, Vector2.zero);
    cameraCore.SetSoftZone(ctx.mainCameraID,
                           softZoneSize,
                           Vector2.zero,
                           softZoneDampingFactor,
                           recenterEasingType,
                           recenterEasingMode,
                           recenterEasingDuration);
    ctx.core.EnableDeadZone(ctx.mainCameraID, true);
    ctx.core.EnableSoftZone(ctx.mainCameraID, true);
    ctx.SetRole(role);
}
```

```
// Shake
void ShakeOnce(int cameraID) {
    ctx.core.ShakeOnce(cameraID, shakeFrequency, shakeAmplitude, shakeDuration, shakeEasingType, shakeEasingMode);
}
```

```
// Tick
void LateTick(float dt) {
    Camera2DInfra.RecordDriverPos(ctx, role.transform.position);
    var pos = Camera2DInfra.TickPos(ctx, dt);
    mainCamera.transform.position = pos;
}
```

# Project Sample
[Leap](https://github.com/onovich/Leap) A Platform Game In Development.

# Dependency
Easing Function Library
[Swing](https://github.com/onovich/Swing)

# UPM URL
ssh://git@github.com/onovich/Vista.git?path=/Assets/com.tenon.vista#main
