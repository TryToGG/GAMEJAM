using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator Menu;
    public bool menu = false;
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionPressed()
    {
        Menu.SetBool("Options",menu);
        menu = !menu;
    }
}
