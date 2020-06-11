using UnityEngine;
using UnityEngine.Audio;

namespace JamCraft5.UI.Game
{
    public class SFXSliderController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private AudioMixer sfxMixer;
        #endregion

        #region ChangeSFXLevel
        public void ChangeSFXLevel(float volume)
        {
            sfxMixer.SetFloat("SFX Volume", volume);
        }
        #endregion
    }
}
