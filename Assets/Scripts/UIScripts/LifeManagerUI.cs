using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeManagerUI : MonoBehaviour
{
    public event EventHandler OnLifeChanged;

    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private CollectButtonUI _collectButton;
    private int _lifeAmount;

    private void Start()
    {
        _lifeAmount = ConstantsKeeper.LIFE_AMOUNT;
        Block.OnKillPlayer += Block_OnKillPlayer;
        _collectButton.OnCollectButtonClicked += _collectButton_OnCollectButtonClicked;

        _lifeText.text = _lifeAmount.ToString();
    }

    private void _collectButton_OnCollectButtonClicked(object sender, System.EventArgs e)
    {
        ChangeLifeAmount(1);
    }

    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        ChangeLifeAmount(-1);
    }
    private void ChangeLifeAmount(int changeValue)
    {
        _lifeAmount += changeValue;
        _lifeText.text = _lifeAmount.ToString();
        OnLifeChanged?.Invoke(this, EventArgs.Empty);
    }
}
