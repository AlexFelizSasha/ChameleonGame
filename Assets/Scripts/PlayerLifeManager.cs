using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private CollectButtonUI _collectButton;

    public static event EventHandler OnLifeManagerGameOver;

    private GameConstantsSO _gameConstantsSO;
    private int _lifeAmount;

    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _lifeAmount = _gameConstantsSO.lifeAmount;
    }
    private void Start()
    {
        Block.OnKillPlayer += Block_OnKillPlayer;
        _collectButton.OnCollectButtonClicked += CollectButton_OnCollectButtonClicked;
    }

    private void CollectButton_OnCollectButtonClicked(object sender, EventArgs e)
    {
        _lifeAmount++;
    }

    private void OnDisable()
    {
        Block.OnKillPlayer -= Block_OnKillPlayer;
    }
    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        _lifeAmount --;
        if (_lifeAmount == 0 )
        {
            OnLifeManagerGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
