using System.Collections.Generic;
using System;

namespace MortiseFrame.Vista {

    public class Camera2DContext {

        SortedList<int, Camera2DEntity> cameras;
        IDService idService;

        public Camera2DContext() {
            cameras = new SortedList<int, Camera2DEntity>();
            idService = new IDService();
        }

    }

}