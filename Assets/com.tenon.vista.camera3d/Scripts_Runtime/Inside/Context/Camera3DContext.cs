using System.Collections.Generic;
using System;
using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class Camera3DContext {

        // Service
        internal IDService3D idService;

        // Global Info
        internal Vector2 viewSize;
        internal float aspect;

        // Repo
        internal SortedList<int, TPCamera3DModel> tpCameras;

        // IoC
        internal Camera cameraAgent;

        // State
        internal bool confinerIsVaild;

        internal Camera3DContext() {
            tpCameras = new SortedList<int, TPCamera3DModel>();
            idService = new IDService3D();
            confinerIsVaild = true;
        }

        internal void Init(Vector2 viewSize, float aspect) {
            this.viewSize = viewSize;
            this.aspect = aspect;
        }

        internal void Inject(Camera cameraAgent) {
            this.cameraAgent = cameraAgent;
        }

        internal void AddTPCamera(TPCamera3DModel camera, int id) {
            bool succ = tpCameras.TryAdd(id, camera);
            if (!succ) {
                V3Log.Error($"Add Camera Error, Camera Not Found: ID = {id}");
            }
        }

        internal void RemoveTPCamera(int id) {
            bool succ = tpCameras.Remove(id);
            if (!succ) {
                V3Log.Error($"Remove Camera Error,Camera Not Found: ID = {id}");
            }
        }

        internal bool TryGetTPCamera(int id, out TPCamera3DModel camera) {
            return tpCameras.TryGetValue(id, out camera);
        }

        internal void SetConfinerValid(bool valid) {
            confinerIsVaild = valid;
        }

        internal void TPCamera_ForEach(Action<TPCamera3DModel> action) {
            foreach (var camera in tpCameras.Values) {
                action(camera);
            }
        }

        internal void Clear() {
            tpCameras.Clear();
        }

    }

}