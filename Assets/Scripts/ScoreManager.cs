using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameConstantsSO _gameConstantsSO;
    public static event EventHandler OnBarrelLoaded;
    public static event EventHandler<OnScoreChangingEventArgs> OnScoreChanging;
    public class OnScoreChangingEventArgs: EventArgs
    {
        public int score;
    }

    private int _unloadedScore;
    private int _fullBarrelScore;

    private int _score;
    

    private void Start()
    {
        //_fullBarrelScore = ConstantsKeeper.FULL_WATER_BARREL_SCORE;
        _fullBarrelScore = _gameConstantsSO.fullWaterBarrelScore;
        _score = 0;
        _unloadedScore = 0;
        Loot.OnLootScoreAdd += Loot_OnScoreAdd;
        PlayerBaggage.OnScoreUnload += PlayerBaggage_OnScoreUnload;
    }
    private void Update()
    {
        if (_unloadedScore >= _fullBarrelScore)
            OnBarrelLoaded?.Invoke(this, EventArgs.Empty);
    }
    private void OnDisable()
    {
        Loot.OnLootScoreAdd -= Loot_OnScoreAdd;
        PlayerBaggage.OnScoreUnload -= PlayerBaggage_OnScoreUnload;
    }
    private void PlayerBaggage_OnScoreUnload(object sender, PlayerBaggage.OnScoreUnloadEventArgs e)
    {
        _unloadedScore += e.unloadScore;
    }

    private void Loot_OnScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    {
        _score += e.lootScore;
        OnScoreChanging?.Invoke(this, new OnScoreChangingEventArgs
        {
            score = _score
        });
    }
}
