using UnityEngine;
using Utility.Health;
using Utility.Development;
using UnityEngine.SceneManagement;

namespace JamCraft5.Player
{
    [RequireComponent(typeof(HealthSystem))]
    public class PlayerDeathController : MonoBehaviour
    {
        #region Variables
        private HealthSystem playerHealthSystem;
        #endregion

        #region Awake
        private void Awake()
        {
            playerHealthSystem = GetComponent<HealthSystem>();
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            playerHealthSystem.OnDeath += OnPlayerDeath;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            playerHealthSystem.OnDeath -= OnPlayerDeath;
        }
        #endregion

        #region OnPlayerDeath
        private void OnPlayerDeath()
        {
            FadeSystem.Instance.Fade(1, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
        #endregion
    }
}
