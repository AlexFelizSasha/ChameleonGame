using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    public static event Action OnPauseButtonClicked;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            OnPauseButtonClicked?.Invoke();
        }
        );
    }
}
