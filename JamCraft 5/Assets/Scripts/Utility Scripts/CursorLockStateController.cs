using UnityEngine;

namespace Utility.Development
{
    public static class CursorLockStateController
    {
        #region LockCursor
        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        #endregion

        #region UnlockCursor
        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }
}
