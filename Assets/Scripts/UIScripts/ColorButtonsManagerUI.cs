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

    private ButtonCoolDown _brownButtonCooldown;
    private ButtonCoolDown _darkBlueButtonCooldown;
    private ButtonCoolDown _redButtonCooldown;
    private ButtonCoolDown _greenButtonCooldown;
    private ButtonCoolDown _yellowButtonCooldown;
    private ButtonCoolDown _grayButtonCooldown;
    private ButtonCoolDown _blueButtonCooldown;
    private ButtonCoolDown _orangeButtonCooldown;
    private ButtonCoolDown _violetButtonCooldown;

    private void Awake()
    {
        _brownButtonCooldown = _brownButton.GetComponent<ButtonCoolDown>();
        _darkBlueButtonCooldown = _darkBlueButton.GetComponent<ButtonCoolDown>();
        _redButtonCooldown = _redButton.GetComponent<ButtonCoolDown>();
        _greenButtonCooldown = _greenButton.GetComponent<ButtonCoolDown>();
        _yellowButtonCooldown = _yellowButton.GetComponent<ButtonCoolDown>();
        _grayButtonCooldown = _grayButton.GetComponent<ButtonCoolDown>();
        _blueButtonCooldown = _blueButton.GetComponent<ButtonCoolDown>();
        _orangeButtonCooldown = _orangeButton.GetComponent<ButtonCoolDown>();
        _violetButtonCooldown = _violetButton.GetComponent<ButtonCoolDown>();

        _brownButton.onClick.AddListener(() =>
        {
            if (_brownButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Brown);
                _brownButtonCooldown.ColldownButton();
            }
        }
        );
        _darkBlueButton.onClick.AddListener(() =>
        {
            if (_darkBlueButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.DarkBlue);
                _darkBlueButtonCooldown.ColldownButton();
            }
        }
        );
        _redButton.onClick.AddListener(() =>
        {
            if (_redButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Red);
                _redButtonCooldown.ColldownButton();
            }
        }
        );
        _greenButton.onClick.AddListener(() =>
        {
            if (_greenButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Green);
                _greenButtonCooldown.ColldownButton();
            }
        }
        );
        _yellowButton.onClick.AddListener(() =>
        {
            if (_yellowButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Yellow);
                _yellowButtonCooldown.ColldownButton();
            }
        }
        );
        _grayButton.onClick.AddListener(() =>
        {
            if (_grayButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Gray);
                _grayButtonCooldown.ColldownButton();
            }
        }
        );
        _blueButton.onClick.AddListener(() =>
        {
            if (_blueButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Blue);
                _blueButtonCooldown.ColldownButton();
            }
        }
        );
        _orangeButton.onClick.AddListener(() =>
        {
            if (_orangeButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Orange);
                _orangeButtonCooldown.ColldownButton();
            }
        }
        );
        _violetButton.onClick.AddListener(() =>
        {
            if (_violetButtonCooldown.isActive)
            {
                GiveColor(_materialSOList.Violet);
                _violetButtonCooldown.ColldownButton();
            }
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
