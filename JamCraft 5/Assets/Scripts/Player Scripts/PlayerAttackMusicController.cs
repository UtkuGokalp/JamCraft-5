using UnityEngine;
using System.Collections;
using JamCraft5.Player.Attack;

namespace JamCraft5.Audio
{
    public class PlayerAttackMusicController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float transitionTime;
        private WaitForSeconds transitionWaitTime;
        private bool transitioning;
        #endregion

        #region Awake
        private void Awake()
        {
            transitionWaitTime = new WaitForSeconds(transitionTime);
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            PlayerAttackComboController.OnPlayerAttacked += OnPlayerAttacked;
            PlayerAttackComboController.OnPlayerStoppedAttacking += OnPlayerStoppedAttacking;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            PlayerAttackComboController.OnPlayerAttacked -= OnPlayerAttacked;
            PlayerAttackComboController.OnPlayerStoppedAttacking -= OnPlayerStoppedAttacking;
        }
        #endregion

        #region OnPlayerAttacked
        private void OnPlayerAttacked(object sender, System.EventArgs e)
        {
            if (AudioManager.Instance.PlayingIdleTrack)
            {
                AudioManager.Instance.PassToCombatTrack();
            }
        }
        #endregion

        #region OnPlayerStoppedAttacking
        private void OnPlayerStoppedAttacking(object sender, System.EventArgs e)
        {
            if (!transitioning)
            {
                transitioning = true;
                StartCoroutine(PassToIdleTrackCoroutine());
            }
        }
        #endregion

        #region PassToIdleTrackCoroutine
        private IEnumerator PassToIdleTrackCoroutine()
        {
            yield return transitionWaitTime;
            if (!PlayerAttackComboController.Attacking)
            {
                AudioManager.Instance.PassToIdleTrack();
                transitioning = false;
            }
        }
        #endregion
    }
}
