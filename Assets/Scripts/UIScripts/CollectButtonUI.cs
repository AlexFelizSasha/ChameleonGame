using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectButtonUI : MonoBehaviour
{
    public event EventHandler OnCollectButtonClicked;

    [SerializeField] private Garden _garden;
    [SerializeField] private Button _collectButton;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        _garden.OnFruitsButton += Garden_OnFruitsButton;
        _collectButton.onClick.AddListener(() =>
            {
                OnCollectButtonClicked?.Invoke(this, EventArgs.Empty);
                Hide();
            });
    }

    private void Garden_OnFruitsButton(object sender, System.EventArgs e)
    {
        Show();
        Debug.Log("SHOW COLLECT BUTTON");
    }

    private void Show()
    {
        _collectButton.gameObject.SetActive(true);
    }
    private void Hide()
    {
        _collectButton.gameObject.SetActive(false);
    }
}
