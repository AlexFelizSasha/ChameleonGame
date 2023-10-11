using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _geyserParticle;
    [SerializeField] private GeyserAnimator _geyserAnimator;

    private float _speedUpValue = 4f;
    private float _speedDownValue = 0.5f;
    private float _geyserOpenTime = 1f;
    
    public enum GeyserParticleState
    {
        OpenGeyser,
        CloseGeyser
    }

    public GeyserParticleState geyserParticleState;
    private void Start()
    {
        _geyserAnimator.OnOpenGeyser += _geyserAnimator_OnOpenGeyser;
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
        }
        Debug.Log(geyserParticleState + "\n" + _geyserOpenTime);
    }

    private void _geyserAnimator_OnOpenGeyser(object sender, System.EventArgs e)
    {
        _geyserOpenTime = 4f;
        Debug.Log("Particles up!");
        //SpeedupParticles(_speedUpValue);
    }
    private void SpeedupParticles(float speedUpValue)
    {
        //var _particleVelocity = _geyserParticle.velocityOverLifetime;
        //_particleVelocity.yMultiplier += speedUpValue;
        var _particleMain = _geyserParticle.main;
        _particleMain.startLifetime = speedUpValue;
    }
}
