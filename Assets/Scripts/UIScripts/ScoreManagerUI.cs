using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        ScoreManager.OnScoreChanging += ScoreManager_OnScoreChanging;
        _scoreText.text = "Score: 0";
    }

    private void ScoreManager_OnScoreChanging(object sender, ScoreManager.OnScoreChangingEventArgs e)
    {
        _scoreText.text = "Score: " + e.score.ToString();
    }
}
