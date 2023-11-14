using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static GeyserParticle;

public class GeyserParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _geyserParticle;
    [SerializeField] private GeyserAnimator _geyserAnimator;
    private GeyserCollider _geyserCollider;

    private float _speedUpValue = 4f;
    private float _speedDownValue = 0.5f;
    private float _speedBlockValue = 0f;
    private float _geyserOpenTime = 1f;
    private float _geyserBlockTime;
    private float _unBlockTime = 1f;
    private float _geyserAnimationTime = 4f;

    public enum GeyserParticleState
    {
        OpenGeyser,
        BlockGeyser,
        CloseGeyser
    }

    public GeyserParticleState geyserParticleState;
    private void Awake()
    {
        _geyserCollider = GetComponent<GeyserCollider>();
        _geyserBlockTime = ConstantsKeeper.DROP_DELAY_TIME;
    }
    private void Start()
    {
        _geyserOpenTime = _geyserAnimationTime;
        geyserParticleState = GeyserParticleState.CloseGeyser;
        
        _geyserAnimator.OnOpenGeyser += _geyserAnimator_OnOpenGeyser;
        _geyserCollider.OnDropOnGeyser += _geyserCollider_OnDropOnGeyser;
    }


    private void Update()
    {
        _geyserOpenTime -= Time.deltaTime;
        switch (geyserParticleState)
        {
            case GeyserParticleState.OpenGeyser:
                if (_geyserOpenTime < 0)
                {
                    SpeedupParticles(_speedDownValue);
                    geyserParticleState = GeyserParticleState.CloseGeyser;
                }
                break;
            case GeyserParticleState.CloseGeyser:
                if (_geyserOpenTime > 0)
                {
                    SpeedupParticles(_speedUpValue);
                    geyserParticleState = GeyserParticleState.OpenGeyser;
                }
                break;
            case GeyserParticleState.BlockGeyser:

                if (_geyserOpenTime < _unBlockTime)
                {
                    _geyserOpenTime = _geyserAnimationTime;
                    SpeedupParticles(_speedUpValue);
                    geyserParticleState = GeyserParticleState.OpenGeyser;
                }
                break;
        }
    }

    private void _geyserAnimator_OnOpenGeyser(object sender, System.EventArgs e)
    {
        _geyserOpenTime = _geyserAnimationTime;
    }
    private void _geyserCollider_OnDropOnGeyser(object sender, System.EventArgs e)
    {
        _geyserOpenTime = _geyserBlockTime;
        SpeedupParticles(_speedBlockValue);
        Debug.Log("Geyser blocked" + transform.position);
        geyserParticleState = GeyserParticleState.BlockGeyser;
    }
    private void SpeedupParticles(float speedUpValue)
    {
        var _particleMain = _geyserParticle.main;
        _particleMain.startLifetime = speedUpValue;
    }
}
