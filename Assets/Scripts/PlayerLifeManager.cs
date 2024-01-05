using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private GameConstantsSO _gameConstantsSO;
    public static event EventHandler OnLifeManagerGameOver;

    private int _lifeAmount;

    private void Awake()
    {
        //_lifeAmount = ConstantsKeeper.LIFE_AMOUNT;
        _lifeAmount = _gameConstantsSO.lifeAmount;
    }
    private void Start()
    {
        Block.OnKillPlayer += Block_OnKillPlayer;
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
