using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //or make it equal 1
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
