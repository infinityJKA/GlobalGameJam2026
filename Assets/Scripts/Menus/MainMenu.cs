using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour

{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public void playGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void openLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }
}
