using UnityEngine;
using Utility.Development;
using UnityEngine.SceneManagement;

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
            FadeSystem.Instance.Fade(fadeTime, () =>
            {
                SceneManager.LoadScene(sceneName);
            });
        } 
        #endregion
    }
}
