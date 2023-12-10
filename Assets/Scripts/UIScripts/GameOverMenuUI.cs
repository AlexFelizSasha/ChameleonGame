using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _mainMenuButton;

    public static event EventHandler OnGameOverQuitButtonClicked;
    public static event EventHandler OnGameOverRestartButtonClicked;
    public static event EventHandler OnGameOverMenuButtonClicked;

    private void Start()
    {
        _quitButton.onClick.AddListener(() =>
        {
            OnGameOverQuitButtonClicked?.Invoke(this, EventArgs.Empty);
        });

        _mainMenuButton.onClick.AddListener(() =>
        {
            OnGameOverMenuButtonClicked?.Invoke(this, EventArgs.Empty);
        });

        _restartButton.onClick.AddListener(() =>
        {
            OnGameOverRestartButtonClicked?.Invoke(this, EventArgs.Empty);
        });
    }
}
