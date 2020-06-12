using UnityEngine;
using Utility.Development;
using UnityEngine.SceneManagement;

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
        private AudioSource mainMenuTrack;
        [SerializeField]
        private AudioSource idleTrack;
        [SerializeField]
        private AudioSource combatTrack;
        [SerializeField]
        private AudioSource[] footstepSoundSources;
        public bool PlayingIdleTrack => idleTrack.volume > 0 && idleTrack.isPlaying && (combatTrack.volume <= 0 || !combatTrack.isPlaying);
        public bool PlayingCombatTrack => combatTrack.volume > 0 && combatTrack.isPlaying;
        private bool MainMenuTrackPlaying => mainMenuTrack.volume > 0 && mainMenuTrack.isPlaying;
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
            //This part assumes that AudioManager is first created in the MainMenu scene.
            mainMenuTrack.Play();
            mainMenuTrack.FadeIn(.5f);
            combatTrack.volume = 0f;
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += ActiveSceneChanged;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= ActiveSceneChanged;
        }
        #endregion

        #region PlaySFX
        public void PlaySFX(SFXType sfxType)
        {
            switch (sfxType)
            {
                case SFXType.ButtonSound:
                    buttonSoundSource.Play();
                    break;
                case SFXType.FootstepSound:
                    footstepSoundSources[Random.Range(0, footstepSoundSources.Length)].Play();
                    break;
                case SFXType.LightSaberSound:
                    lightSaberSoundSource.Play();
                    break;
                case SFXType.LaserPistolSound:
                    laserPistolSoundSource.Play();
                    break;
            }
        }
        #endregion

        #region PassToIdleTrack
        public void PassToIdleTrack(float fadeTime = 0.5f)
        {
            combatTrack.FadeOut(fadeTime);
        }
        #endregion

        #region PassToCombatTrack
        public void PassToCombatTrack(float fadeTime = 0.5f)
        {
            combatTrack.FadeIn(fadeTime);
        }
        #endregion

        #region ActiveSceneChanged
        private void ActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            //If this scene has the player in it
            //(the scene that will actually have the gameplay)
            if (GameObject.FindGameObjectWithTag(GameUtility.PLAYER_TAG) != null)
            {
                mainMenuTrack.FadeOut(.5f);
                idleTrack.Play();
                combatTrack.Play();
                idleTrack.FadeIn(0.5f, 0.5f); //0.5f as the final volume because otherwise it is too loud
            }
            else if (!MainMenuTrackPlaying)
            {
                idleTrack.FadeOut(.5f);
                combatTrack.FadeOut(.5f);
                mainMenuTrack.Play();
                mainMenuTrack.FadeIn(0.5f);
            }
        }
        #endregion
    }
}
