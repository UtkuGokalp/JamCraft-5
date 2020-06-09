using UnityEngine;
using Utility.Development;

namespace JamCraft5.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private AudioSource buttonSoundSource;
        [SerializeField]
        private AudioSource lightSaberSoundSource;
        [SerializeField]
        private AudioSource laserPistolSoundSource;
        [SerializeField]
        private AudioSource idleTrack;
        [SerializeField]
        private AudioSource combatTrack;
        [SerializeField]
        private AudioSource[] footstepSoundSources;
        public bool PlayingIdleTrack => idleTrack.volume > 0 && idleTrack.isPlaying;
        public bool PlayingCombatTrack => combatTrack.volume > 0 && combatTrack.isPlaying;
        public static AudioManager Instance { get; private set; }
        #endregion
        
        #region Awake
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            idleTrack.Play();
            idleTrack.FadeIn(.5f);
            combatTrack.volume = 0f;
            combatTrack.Play();
        }
        #endregion

        #region PlayAudio
        public void PlayAudio(AudioType audioType)
        {
            switch (audioType)
            {
                case AudioType.ButtonSound:
                    buttonSoundSource.Play();
                    break;
                case AudioType.FootstepSound:
                    footstepSoundSources[Random.Range(0, footstepSoundSources.Length)].Play();
                    break;
                case AudioType.LightSaberSound:
                    lightSaberSoundSource.Play();
                    break;
                case AudioType.LaserPistolSound:
                    laserPistolSoundSource.Play();
                    break;
            }
        }
        #endregion
        
        #region PassToIdleTrack
        public void PassToIdleTrack()
        {
            idleTrack.FadeIn(.5f);
            combatTrack.FadeOut(.5f);
        }
        #endregion
        
        #region PassToCombatTrack
        public void PassToCombatTrack()
        {
            combatTrack.FadeIn(.5f);
            idleTrack.FadeOut(.5f);
        }
        #endregion
    }
}
