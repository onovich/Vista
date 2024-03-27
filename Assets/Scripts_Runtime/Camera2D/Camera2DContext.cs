using System.Collections.Generic;
using System;

namespace MortiseFrame.Vista {

    public class Camera2DContext {

        SortedList<int, Camera2DEntity> cameras;
        IDService idService;
        public IDService IDService => idService;

        public Camera2DContext() {
            cameras = new SortedList<int, Camera2DEntity>();
            idService = new IDService();
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

    }

}