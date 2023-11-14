using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUIanimator : MonoBehaviour
{
    private Animator _lifeAnimator;
    private LifeManagerUI _lifeManager;
    private const string LIFE_CHANGE = "LifeChange";

    private void Awake()
    {
        _lifeAnimator = GetComponent<Animator>();
        _lifeManager = GetComponent<LifeManagerUI>();
    }
    private void Start()
    {
        _lifeManager.OnLifeChanged += _lifeManager_OnLifeChanged;
    }

    private void _lifeManager_OnLifeChanged(object sender, System.EventArgs e)
    {
        _lifeAnimator.SetTrigger(LIFE_CHANGE);
    }
}
