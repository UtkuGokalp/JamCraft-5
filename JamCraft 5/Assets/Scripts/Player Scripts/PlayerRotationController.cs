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
            if (Player.Attack.PlayerAttack.Attacking) { return; }

            Vector3 target = GameUtility.MousePosition;
            Vector3 direction = GameUtility.GetDirection(transformCache.position, target);
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transformCache.rotation = Quaternion.Euler(transformCache.rotation.eulerAngles.x, rotation, transformCache.rotation.eulerAngles.z);
        }
        #endregion
    }
}
