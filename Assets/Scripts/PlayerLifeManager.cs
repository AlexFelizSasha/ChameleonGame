using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public static event EventHandler OnGameOver;

    private int _lifeAmount;

    private void Awake()
    {
        _lifeAmount = ConstantsKeeper.LIFE_AMOUNT;
    }
    private void Start()
    {
        Block.OnKillPlayer += Block_OnKillPlayer;
    }

    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        _lifeAmount --;
        if (_lifeAmount == 0 )
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
