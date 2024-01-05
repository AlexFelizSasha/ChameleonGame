using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreAmountText;

    private void Start()
    {
        ScoreManager.OnScoreChanging += ScoreManager_OnScoreChanging;
        _scoreAmountText.text = "0";
    }
    private void OnDisable()
    {
        ScoreManager.OnScoreChanging -= ScoreManager_OnScoreChanging;
    }
    private void ScoreManager_OnScoreChanging(object sender, ScoreManager.OnScoreChangingEventArgs e)
    {
        _scoreAmountText.text = e.score.ToString();
    }
}
