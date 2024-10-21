using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControl : MonoBehaviour
{

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
