using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _easyStartButton;
    [SerializeField] private Button _mediumStartButton;
    [SerializeField] private Button _hardSButton;
    [SerializeField] private Button _quitButton;

    public static event Action OnEasyStartButtonClicked;
    public static event Action OnMediumStartButtonClicked;
    public static event Action OnHardStartButtonClicked;

    public static event Action OnQuitButtonClicked;

    private void Start()
    {
        _easyStartButton.onClick.AddListener(() =>
        {
            OnEasyStartButtonClicked?.Invoke();
        });

        _mediumStartButton.onClick.AddListener(() =>
        {
            OnMediumStartButtonClicked?.Invoke();
        });

        _hardSButton.onClick.AddListener(() =>
        {
            OnHardStartButtonClicked?.Invoke();
        });

        _quitButton.onClick.AddListener(() =>
        {
            OnQuitButtonClicked?.Invoke();
        });
    }
}
