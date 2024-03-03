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
    
    private GameConstantsSO _gameConstantsSO;
    private int _lifeAmount;

    private void Start()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _lifeAmount = _gameConstantsSO.lifeAmount;

        Block.OnKillPlayer += Block_OnKillPlayer;

        _collectButton.OnCollectButtonClicked += _collectButton_OnCollectButtonClicked;

        _lifeText.text = _lifeAmount.ToString();
    }
    private void OnDisable()
    {
        Block.OnKillPlayer -= Block_OnKillPlayer;
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
