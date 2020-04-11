using UnityEngine;

namespace Utility.Development
{
    public static class GameUtility
    {
        #region Variables
        private static Camera mainCam;

        public static Camera MainCam
        {
            get
            {
                if (mainCam == null)
                {
                    mainCam = Camera.main;
                }
                return mainCam;
            }
        }
        public static Vector3 MousePosition
        {
            get
            {
                Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                if (plane.Raycast(ray, out float distance))
                {
                    return ray.GetPoint(distance);
                }
                return default;
            }
        }
        #endregion

        #region GetDirection
        /// <summary>
        /// Gets the normalized direction from position to to position.
        /// </summary>
        public static Vector2 GetDirection(Vector2 from, Vector2 to) => (to - from).normalized;
        /// <summary>
        /// Gets the normalized direction from position to to position.
        /// </summary>
        public static Vector3 GetDirection(Vector3 from, Vector3 to) => (to - from).normalized;
        #endregion

        #region GetDirectionToMouse
        public static Vector3 GetDirectionToMouse(Vector3 from) => GetDirection(from, MousePosition);
        #endregion

        #region GetAngleFromVector
        public static float GetAngleFromVector(Vector2 direction) => Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        #endregion
    }
}
