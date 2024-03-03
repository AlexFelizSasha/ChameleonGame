using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        MainMenuUI.OnQuitButtonClicked += MainMenuUIOnQuitButtonClicked;

        MainMenuUI.OnEasyStartButtonClicked += MainMenuUIOnEasyStartButtonClicked;
        MainMenuUI.OnMediumStartButtonClicked += MainMenuUIOnMediumStartButtonClicked;
        MainMenuUI.OnHardStartButtonClicked += MainMenuUIOnHardStartButtonClicked;
    }

    private void OnDisable()
    {
        MainMenuUI.OnQuitButtonClicked -= MainMenuUIOnQuitButtonClicked;

        MainMenuUI.OnEasyStartButtonClicked -= MainMenuUIOnEasyStartButtonClicked;
        MainMenuUI.OnMediumStartButtonClicked -= MainMenuUIOnMediumStartButtonClicked;
        MainMenuUI.OnHardStartButtonClicked -= MainMenuUIOnHardStartButtonClicked;
    }

    private void MainMenuUIOnEasyStartButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    private void MainMenuUIOnMediumStartButtonClicked()
    {
        SceneManager.LoadScene(2);
    }
    private void MainMenuUIOnHardStartButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    private void MainMenuUIOnQuitButtonClicked()
    {
        Application.Quit();
    }
}
