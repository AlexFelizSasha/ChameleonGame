using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        MainMenuUI.OnQuitButtonClicked += MainMenuUI_OnQuitButtonClicked;
        MainMenuUI.OnStartButtonClicked += MainMenuUI_OnStartButtonClicked;
    }
    private void OnDisable()
    {
        MainMenuUI.OnQuitButtonClicked -= MainMenuUI_OnQuitButtonClicked;
        MainMenuUI.OnStartButtonClicked -= MainMenuUI_OnStartButtonClicked;
    }

    private void MainMenuUI_OnStartButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    private void MainMenuUI_OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
