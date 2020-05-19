using UnityEngine;
using Utility.Health;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(EnemyChasePlayerComponent))]
    public class EnemyRotationComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float rotationSpeed;
        private Transform transformCache;
        private EnemyChasePlayerComponent chasePlayerComponent;
        private HealthSystem healthSystem;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            healthSystem = GetComponent<HealthSystem>();
            chasePlayerComponent = GetComponent<EnemyChasePlayerComponent>();
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            healthSystem.OnDeath += OnDeath;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            healthSystem.OnDeath -= OnDeath;
        }
        #endregion

        #region OnDeath
        private void OnDeath()
        {
            enabled = false;
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
