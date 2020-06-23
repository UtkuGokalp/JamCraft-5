using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUnlocking : MonoBehaviour
{
    #region Variables
    public bool Dash = false;
    public bool Dash2 = false;
    public static bool playerPause = false;
    #endregion

    #region OnEnable
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    #endregion

    #region OnDisable
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    #endregion

    #region OnSceneChanged
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        playerPause = false;
    }
    #endregion
}
