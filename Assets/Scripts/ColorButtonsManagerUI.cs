using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonsManagerUI : MonoBehaviour
{
    public static event EventHandler<OnColorChangedWithButtonEventArgs> OnColorChangedWithButton;
    public class OnColorChangedWithButtonEventArgs : EventArgs
    {
        public MaterialSO materialSO;
    }

    [SerializeField] private Button _blackButton;
    [SerializeField] private Button _whiteButton;
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _yellowButton;
    [SerializeField] private Button _grayButton;

    [SerializeField] private MaterialSOList _materialSOList;
    private MaterialSO _materialSO;

    private void Awake()
    {
        _blackButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Black);
        }
        );
        _whiteButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.White);
        }
        );
        _redButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Red);
        }
        );
        _greenButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Green);
        }
        );
        _yellowButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Yellow);
        }
        );
        _grayButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Gray);
        }
        );
    }

    private void GiveColor(MaterialSO materialSO)
    {
        _materialSO = materialSO;


        OnColorChangedWithButton?.Invoke(this, new OnColorChangedWithButtonEventArgs
        {
            materialSO = _materialSO
        });
    }
}
