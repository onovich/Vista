using System.Collections.Generic;
using System;

namespace MortiseFrame.Vista {

    public class CameraContext {

        SortedList<int, CameraEntity> cameras;
        IDService idService;

        public CameraContext() {
            cameras = new SortedList<int, CameraEntity>();
            idService = new IDService();
        }







    }

}