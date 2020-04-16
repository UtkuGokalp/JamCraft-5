using UnityEngine;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(EnemyChasePlayerComponent))]
    public class EnemyRotationComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float rotationSpeed;
        private Transform transformCache;
        private EnemyChasePlayerComponent chasePlayerComponent;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            chasePlayerComponent = GetComponent<EnemyChasePlayerComponent>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (chasePlayerComponent.PlayerIsInDetectionRange)
            {
                Vector3 direction = GameUtility.GetDirection(transformCache.position, GameUtility.PlayerPosition);
                float yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion desiredRotation = Quaternion.Euler(0, yRotation, 0);
                transformCache.rotation = Quaternion.Slerp(transformCache.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }
        }
        #endregion
    }
}
