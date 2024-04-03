using System.Collections.Generic;
using System;
using UnityEngine;

namespace MortiseFrame.Vista {

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

        internal Camera2DContext() {
            cameras = new SortedList<int, Camera2DEntity>();
            idService = new IDService();
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
                throw new Exception("Camera2DContext.AddCamera: failed to add camera");
            }
        }

        internal void RemoveCamera(Camera2DEntity camera) {
            int id = camera.ID;
            bool succ = cameras.Remove(id);
            if (!succ) {
                throw new Exception("Camera2DContext.RemoveCamera: failed to remove camera");
            }
        }

        internal bool TryGetCamera(int id, out Camera2DEntity camera) {
            return cameras.TryGetValue(id, out camera);
        }

        internal void SetCurrentCamera(Camera2DEntity camera) {
            currentCamera = camera;
        }

        internal void Clear() {
            cameras.Clear();
            currentCamera = null;
        }

    }

}