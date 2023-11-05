using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIanimator : MonoBehaviour
{
    private Animator _scoreAnimator;
    private const string SCORE_ADD = "ScoreAdd";

    private void Awake()
    {
        _scoreAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        ScoreManager.OnScoreChanging += ScoreManager_OnScoreChanging;
    }

    private void ScoreManager_OnScoreChanging(object sender, ScoreManager.OnScoreChangingEventArgs e)
    {
        _scoreAnimator.SetTrigger(SCORE_ADD);
    }
}
