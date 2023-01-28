using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("In Game");
    }
    public void Options()
    {
        Debug.Log("Options Chosen");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
