using System.Collections.Generic;
using System;

namespace MortiseFrame.Vista {

    public class Camera2DContext {

        SortedList<int, CameraEntity> cameras;
        IDService idService;

        public CameraContext() {
            cameras = new SortedList<int, CameraEntity>();
            idService = new IDService();
        }

    }

}