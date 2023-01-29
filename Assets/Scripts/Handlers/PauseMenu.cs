using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Handlers
{
    public class PauseMenu : MonoBehaviour
    {
        public void Resume()
        {
            GameManager.Instance.ActivateControls = true;
            GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<Canvas>().enabled = false;
        }
        public void Options()
        {

        }
        public void MainMenu()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }
}