using UnityEngine;

namespace TenonKit.Vista.Camera3D {

    internal class InputComponent {

        internal Vector3 manualPanAxis;
        internal Vector2 manualOrbitalAxis;

        internal void SetManualPanAxis(Vector3 axis) {
            manualPanAxis = axis;
        }

        internal void SetManualOrbitalAxis(Vector2 axis) {
            manualOrbitalAxis = axis;
        }

        internal void Reset() {
            manualPanAxis = Vector3.zero;
            manualOrbitalAxis = Vector2.zero;
        }

    }

}