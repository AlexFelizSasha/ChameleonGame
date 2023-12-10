using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChameleonGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _colorButtons;
    [SerializeField] private GameObject _pauseButton;

    private void Start()
    {
        PauseButton.OnPauseButtonClicked += PauseButton_OnPauseButtonClicked;

        PauseMenuUI.OnPausePlayButtonClicked += PauseMenuUI_OnPausePlayButtonClicked;
        PauseMenuUI.OnPauseQuitButtonClicked += PauseMenuUI_OnPauseQuitButtonClicked;
        PauseMenuUI.OnPauseMenuButtonClicked += PauseMenuUI_OnPauseMenuButtonClicked;

        PlayerLifeManager.OnLifeManagerGameOver += PlayerLifeManager_OnGameOver;

        Garden.OnTreesDead += Garden_OnTreesDead;

        GameOverMenuUI.OnGameOverQuitButtonClicked += GameOverMenuUI_OnGameOverQuitButtonClicked;
        GameOverMenuUI.OnGameOverRestartButtonClicked += GameOverMenuUI_OnGameOverRestartButtonClicked;
        GameOverMenuUI.OnGameOverMenuButtonClicked += GameOverMenuUI_OnGameOverMenuButtonClicked;
    }
    private void Garden_OnTreesDead()
    {
        HandleGameOver();
    }
    private void GameOverMenuUI_OnGameOverMenuButtonClicked(object sender, System.EventArgs e)
    {
        GoToMainMenu();
    }
    private void GameOverMenuUI_OnGameOverRestartButtonClicked(object sender, System.EventArgs e)
    {
        RestartGame();
    }
    private void PauseMenuUI_OnPauseMenuButtonClicked(object sender, System.EventArgs e)
    {
        GoToMainMenu();
    }
    private void GameOverMenuUI_OnGameOverQuitButtonClicked(object sender, System.EventArgs e)
    {
        Application.Quit();
    }
    private void PlayerLifeManager_OnGameOver(object sender, System.EventArgs e)
    {
        HandleGameOver();
    }
    private void PauseMenuUI_OnPauseQuitButtonClicked(object sender, System.EventArgs e)
    {
        Application.Quit();        
    }
    private void PauseMenuUI_OnPausePlayButtonClicked(object sender, System.EventArgs e)
    {
        HandlePauseUnclicked();
    }
    private void PauseButton_OnPauseButtonClicked(object sender, System.EventArgs e)
    {
        HandlePauseClicked();
    }
    private void HandlePauseClicked()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _colorButtons.SetActive(false);
        _pauseButton.SetActive(false);
    }
    private void HandlePauseUnclicked()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _colorButtons.SetActive(true);
        _pauseButton.SetActive(true);
    }
    private void HandleGameOver()
    {
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
        _colorButtons.SetActive(false);
        _pauseButton.SetActive(false);
    }
    private void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
