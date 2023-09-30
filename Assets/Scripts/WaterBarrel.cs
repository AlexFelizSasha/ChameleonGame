using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarrel : MonoBehaviour
{
    private int _barrelCapacity = 20;
    private int _score;
    public static bool isFull;

    private void Start()
    {
        PlayerBaggage.OnScoreUnload += PlayerBaggage_OnScoreUnload;
    }
    private void Update()
    {
        isFull = _score >= _barrelCapacity;
        if (isFull)
        {
            Debug.Log($"Barrel is Full with {_score}");
            _score = 0;
        }
    }

    private void PlayerBaggage_OnScoreUnload(object sender, PlayerBaggage.OnScoreUnloadEventArgs e)
    {
        _score += e.unloadScore;
    }
}
