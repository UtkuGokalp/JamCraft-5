using UnityEngine;
using UnityEngine.Audio;

namespace JamCraft5.UI.Game
{
    public class MusicSliderController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private AudioMixer musicMixer;
        #endregion

        #region ChangeMusicLevel
        public void ChangeMusicLevel(float volume)
        {
            musicMixer.SetFloat("Music Volume", volume);
        }
        #endregion
    }
}
