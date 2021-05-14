using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// ------------------------------------------------
    /// CLASS: SManager.cs
    /// DESC: A simple Manager to handle the game Scenes
    /// -------------------------------------------------
    /// </summary>
    public class SManager : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }

        public void LoadMainScene(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void LoadMainMenu(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}