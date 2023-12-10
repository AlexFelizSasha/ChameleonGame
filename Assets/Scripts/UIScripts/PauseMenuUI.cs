using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _mainMenuButton;

    public static event EventHandler OnPausePlayButtonClicked;
    public static event EventHandler OnPauseQuitButtonClicked;
    public static event EventHandler OnPauseMenuButtonClicked;

    private void Start()
    {
        _playButton.onClick.AddListener(() =>
        {
            OnPausePlayButtonClicked?.Invoke(this, EventArgs.Empty);
        });

        _quitButton.onClick.AddListener(() =>
        {
            OnPauseQuitButtonClicked?.Invoke(this, EventArgs.Empty);
        });

        _mainMenuButton.onClick.AddListener(() =>
        {
            OnPauseMenuButtonClicked?.Invoke(this, EventArgs.Empty);
        });
    }
}
