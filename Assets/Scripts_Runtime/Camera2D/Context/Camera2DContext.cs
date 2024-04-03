using System.Collections.Generic;
using System;
using UnityEngine;

namespace MortiseFrame.Vista {

    public class Camera2DContext {

        SortedList<int, Camera2DEntity> cameras;
        IDService idService;
        public IDService IDService => idService;

        Camera2DEntity currentCamera;
        public Camera2DEntity CurrentCamera => currentCamera;

        Camera mainCamera;
        public Camera MainCamera => mainCamera;

        Vector2 viewSize;
        public Vector2 ViewSize => viewSize;

        public Camera2DContext() {
            cameras = new SortedList<int, Camera2DEntity>();
            idService = new IDService();
        }

        public void Init(Vector2 screenSize) {
            this.viewSize = screenSize;
        }

        public void Inject(Camera mainCamera) {
            this.mainCamera = mainCamera;
        }

        public void AddCamera(Camera2DEntity camera, int id) {
            bool succ = cameras.TryAdd(id, camera);
            if (!succ) {
                throw new Exception("Camera2DContext.AddCamera: failed to add camera");
            }
        }

        public void RemoveCamera(Camera2DEntity camera) {
            int id = camera.ID;
            bool succ = cameras.Remove(id);
            if (!succ) {
                throw new Exception("Camera2DContext.RemoveCamera: failed to remove camera");
            }
        }

        public bool TryGetCamera(int id, out Camera2DEntity camera) {
            return cameras.TryGetValue(id, out camera);
        }

        public void SetCurrentCamera(Camera2DEntity camera) {
            currentCamera = camera;
        }

        public void Clear() {
            cameras.Clear();
            currentCamera = null;
        }

    }

}