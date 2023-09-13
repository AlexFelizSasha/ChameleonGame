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

    [SerializeField] private Button _brownButton;
    [SerializeField] private Button _darkBlueButton;
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _yellowButton;
    [SerializeField] private Button _grayButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _orangeButton;
    [SerializeField] private Button _violetButton;

    [SerializeField] private MaterialSOList _materialSOList;
    private MaterialSO _materialSO;

    private void Awake()
    {
        _brownButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Brown);
        }
        );
        _darkBlueButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.DarkBlue);
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
        _blueButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Blue);
        }
        );
        _orangeButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Orange);
        }
        );
        _violetButton.onClick.AddListener(() =>
        {
            GiveColor(_materialSOList.Violet);
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
