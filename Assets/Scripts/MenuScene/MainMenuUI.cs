using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    public static event Action OnStartButtonClicked;
    public static event Action OnQuitButtonClicked;

    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            OnStartButtonClicked?.Invoke();
        });

        _quitButton.onClick.AddListener(() =>
        {
            OnQuitButtonClicked?.Invoke();
        });
    }
}
