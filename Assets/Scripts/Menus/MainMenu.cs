using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour

{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject credits;

    public void playGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void playLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void playLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void playLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void playLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void playLevel6()
    {
        SceneManager.LoadScene("Level6");
    }

    public void playLevel7()
    {
        SceneManager.LoadScene("Level7");
    }

    public void playLevel8()
    {
        SceneManager.LoadScene("Level8");
    }

    public void playLevel9()
    {
        SceneManager.LoadScene("Level9");
    }

    public void playLevel10()
    {
        SceneManager.LoadScene("Level10");
    }

    public void playLevel11()
    {
        SceneManager.LoadScene("Level11");
    }

    public void playLevel12()
    {
        SceneManager.LoadScene("Level12");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void openLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        credits.SetActive(false);
    }

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        credits.SetActive(false);
    }
    public void openCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}
