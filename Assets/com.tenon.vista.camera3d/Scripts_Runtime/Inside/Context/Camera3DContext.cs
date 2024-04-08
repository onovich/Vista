using System.Collections.Generic;
using System;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DContext {

        SortedList<int, Camera3DEntity> cameras;
        IDService3D idService;
        internal IDService3D IDService => idService;

        Camera3DEntity currentCamera;
        internal Camera3DEntity CurrentCamera => currentCamera;

        Camera mainCamera;
        internal Camera MainCamera => mainCamera;

        Vector3 viewSize;
        internal Vector3 ViewSize => viewSize;

        float aspect;
        internal float Aspect => aspect;

        float fov;
        internal float FOV => fov;

        bool inited;
        internal bool Inited => inited;

        bool confinerIsVaild;
        internal bool ConfinerIsVaild => confinerIsVaild;

        internal Camera3DContext() {
            cameras = new SortedList<int, Camera3DEntity>();
            idService = new IDService3D();
            confinerIsVaild = false;
        }

        internal void Init(Vector3 viewSize) {
            this.viewSize = viewSize;
            inited = true;
        }

        internal void Inject(Camera mainCamera) {
            this.mainCamera = mainCamera;
            fov = mainCamera.fieldOfView;
            aspect = mainCamera.aspect;
        }

        internal void AddCamera(Camera3DEntity camera, int id) {
            bool succ = cameras.TryAdd(id, camera);
            if (!succ) {
                V3Log.Error($"Add Camera Error, Camera Not Found: ID = {id}");
            }
        }

        internal void RemoveCamera(int id) {
            bool succ = cameras.Remove(id);
            if (!succ) {
                V3Log.Error($"Remove Camera Error,Camera Not Found: ID = {id}");
            }
        }

        internal bool TryGetCamera(int id, out Camera3DEntity camera) {
            return cameras.TryGetValue(id, out camera);
        }

        internal void SetCurrentCamera(int id) {
            var has = cameras.TryGetValue(id, out var camera);
            if (!has) {
                V3Log.Error($"Set Current Error, Camera Not Found: ID = {id}");
            }
            currentCamera = camera;
        }

        internal void SetConfinerValid(bool valid) {
            confinerIsVaild = valid;
        }

        internal void Clear() {
            cameras.Clear();
            currentCamera = null;
        }

    }

}