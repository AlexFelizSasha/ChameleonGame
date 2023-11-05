using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUIanimator : MonoBehaviour
{
    private Animator _lifeAnimator;
    private const string LIFE_CHANGE = "LifeChange";

    private void Awake()
    {
        _lifeAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        Block.OnKillPlayer += Block_OnKillPlayer;
    }

    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        _lifeAnimator.SetTrigger(LIFE_CHANGE);
    }
}
