using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextUnloadUI : ScoreTextPopUpUI
{
    [SerializeField] private Transform _playerBaggage;
    
    private void Start()
    {
        PlayerBaggage.OnScoreUnload += PlayerBaggage_OnScoreUnload;
    }
    private void OnDisable()
    {
        PlayerBaggage.OnScoreUnload -= PlayerBaggage_OnScoreUnload;
    }
    private void PlayerBaggage_OnScoreUnload(object sender, PlayerBaggage.OnScoreUnloadEventArgs e)
    {
        SetScoreText(e.unloadScore);
        Show();
        SetScoreTextPosition(_playerBaggage);
    }
}
