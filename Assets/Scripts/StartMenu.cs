using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator Menu;
    public bool menu = false;
    public void StartGame()
    {
        StartCoroutine(Animate());
    }
    public IEnumerator Animate()
    {
        Menu.SetBool("GameStart",true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GamePlayScene");
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
