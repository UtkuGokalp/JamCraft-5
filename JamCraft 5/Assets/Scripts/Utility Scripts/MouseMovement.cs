using UnityEngine;

namespace JamCraft5.Utility
{
    public static class MouseMovement
    {
        #region Axis
        public enum Axis
        {
            X,
            Y,
            Z
        }
        #endregion

        #region Variables
        private static Vector3 lastMousePosition;
        #endregion

        #region Constructor
        static MouseMovement()
        {
            lastMousePosition = Input.mousePosition;
            Input.GetAxis("Mouse X"); //Returns the mouse movement on x axis
        }
        #endregion

        #region GetMouseMovement
        public static float GetMouseMovement(Axis axis)
        {
            float mouseMovement = 0;
            switch (axis)
            {
                case Axis.X:
                    mouseMovement = lastMousePosition.x - Input.mousePosition.x;
                    break;
                case Axis.Y:
                    mouseMovement =  /*- */Input.mousePosition.y - lastMousePosition.y;
                    break;
                case Axis.Z:
                    mouseMovement = lastMousePosition.z - Input.mousePosition.z;
                    break;
            }
            lastMousePosition = Input.mousePosition;
            return mouseMovement;
        }
        #endregion
    }
}
