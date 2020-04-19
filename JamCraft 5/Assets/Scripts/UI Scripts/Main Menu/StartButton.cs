using UnityEngine;
using Utility.Development;
using UnityEngine.SceneManagement;
using JamCraft5.Audio;

namespace JamCraft5.UI.Main_Menu
{
    public class StartButton : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float fadeTime;
        #endregion

        #region StartGame
        public void StartGame(string sceneName)
        {
            AudioManager.Instance.PlayAudio(Audio.AudioType.ButtonSound);
            FadeSystem.Instance.Fade(fadeTime, () =>
            {
                SceneManager.LoadScene(sceneName);
            });
        } 
        #endregion
    }
}
