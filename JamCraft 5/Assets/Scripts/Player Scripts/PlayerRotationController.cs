using UnityEngine;
using Utility.Development;

namespace JamCraft5.Player.Movement
{
    public class PlayerRotationController : MonoBehaviour
    {
        #region Variables
        private Transform transformCache;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
        }
        #endregion

        #region Update
        private void Update()
        {
            Vector3 target = GameUtility.MousePosition;
            Vector3 direction = GameUtility.GetDirection(transformCache.position, target);
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transformCache.rotation = Quaternion.Euler(0, rotation, 0);
        }
        #endregion
    }
}
