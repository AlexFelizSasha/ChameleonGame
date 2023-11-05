using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    public static event EventHandler OnPauseButtonClicked;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            OnPauseButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        );
    }
}
