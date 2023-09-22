using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event EventHandler<OnScoreChangingEventArgs> OnScoreChanging;
    public class OnScoreChangingEventArgs: EventArgs
    {
        public int score;
    }

    private int _score;

    private void Start()
    {
        _score = 0;
        Loot.OnLootScoreAdd += Loot_OnScoreAdd;
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
