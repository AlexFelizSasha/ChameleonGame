using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event EventHandler OnBarrelLoaded;
    public static event EventHandler<OnScoreChangingEventArgs> OnScoreChanging;
    public class OnScoreChangingEventArgs: EventArgs
    {
        public int score;
    }

    private int _unloadedScore;
    private int _fullBarrelScore = 100;

    private int _score;

    private void Start()
    {
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
