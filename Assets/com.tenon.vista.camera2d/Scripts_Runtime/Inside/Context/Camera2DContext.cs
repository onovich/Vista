using System.Collections.Generic;
using System;
using UnityEngine;

namespace TenonKit.Vista.Camera2D {

    internal class Camera2DContext {

        SortedList<int, Camera2DEntity> cameras;
        IDService idService;
        internal IDService IDService => idService;

        Camera2DEntity currentCamera;
        internal Camera2DEntity CurrentCamera => currentCamera;

        Camera mainCamera;
        internal Camera MainCamera => mainCamera;

        Vector2 viewSize;
        internal Vector2 ViewSize => viewSize;

        float orthographicSize;
        internal float OrthographicSize => orthographicSize;

        float aspect;
        internal float Aspect => aspect;

        bool inited;
        internal bool Inited => inited;

        bool confinerIsVaild;
        internal bool ConfinerIsVaild => confinerIsVaild;

        internal Camera2DContext() {
            cameras = new SortedList<int, Camera2DEntity>();
            idService = new IDService();
            confinerIsVaild = false;
        }

        internal void Init(Vector2 screenSize) {
            this.viewSize = screenSize;
            inited = true;
        }

        internal void Inject(Camera mainCamera) {
            this.mainCamera = mainCamera;
            this.orthographicSize = mainCamera.orthographicSize;
            this.aspect = mainCamera.aspect;
        }

        internal void AddCamera(Camera2DEntity camera, int id) {
            bool succ = cameras.TryAdd(id, camera);
            if (!succ) {
                VLog.Error($"Add Camera Error, Camera Not Found: ID = {id}");
            }
        }

        internal void RemoveCamera(int id) {
            bool succ = cameras.Remove(id);
            if (!succ) {
                VLog.Error($"Remove Camera Error,Camera Not Found: ID = {id}");
            }
        }

        internal bool TryGetCamera(int id, out Camera2DEntity camera) {
            return cameras.TryGetValue(id, out camera);
        }

        internal void SetCurrentCamera(int id) {
            var has = cameras.TryGetValue(id, out var camera);
            if (!has) {
                VLog.Error($"Set Current Error, Camera Not Found: ID = {id}");
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