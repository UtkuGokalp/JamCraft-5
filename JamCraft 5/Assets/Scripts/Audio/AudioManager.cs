using UnityEngine;

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
        private AudioSource[] footstepSoundSources;
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
    }
}
