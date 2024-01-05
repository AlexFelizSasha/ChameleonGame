using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextOnLootUI : ScoreTextPopUpUI
{
    [SerializeField] private Transform _player;

    private void Start()
    {
        Loot.OnLootScoreAdd += Loot_OnLootScoreAdd;
    }
    private void OnDisable()
    {
        Loot.OnLootScoreAdd -= Loot_OnLootScoreAdd;
    }
    private void Loot_OnLootScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    {
        SetScoreText(e.lootScore);
        Show();
        SetScoreTextPosition(_player);
    }
}
