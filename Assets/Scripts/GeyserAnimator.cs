using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserAnimator : MonoBehaviour
{
    public event EventHandler OnOpenGeyser;
    private Animator _geyserAnimator;
    private const string OPEN_GEYSER = "OpenGeyser";

    private float _animatorTimer;

    public enum GeyserAnimatorState
    {
        Open,
        Idle
    }
    public GeyserAnimatorState geyserAnimatorState;
    private void Start()
    {
        _geyserAnimator = GetComponent<Animator>();
        Block.OnBlockDestroyed += Block_OnBlockDestroyed;
        
        _animatorTimer = 0;
        geyserAnimatorState = GeyserAnimatorState.Open;
        _geyserAnimator.SetBool(OPEN_GEYSER, true);
    }
    private void Update()
    {
        switch (geyserAnimatorState)
        {
            case GeyserAnimatorState.Open:
                _animatorTimer += Time.deltaTime;
                if (_animatorTimer > Time.deltaTime)
                {
                    
                    _geyserAnimator.SetBool(OPEN_GEYSER, false);
                    _animatorTimer = 0;
                    geyserAnimatorState = GeyserAnimatorState.Idle;
                }
                break;
            case GeyserAnimatorState.Idle:
                break;
        }
    }
    private void Block_OnBlockDestroyed(object sender, Block.OnBlockDestroyedEventArgs e)
    {
        if (EqualXZPositions(transform.position, e.blockPosition))
        {
            _geyserAnimator.SetBool(OPEN_GEYSER, true);
            OnOpenGeyser?.Invoke(this, EventArgs.Empty);
            geyserAnimatorState = GeyserAnimatorState.Open;
            
            Debug.Log("geyser!!!!");
        }
    }

    private bool EqualXZPositions(Vector3 objectPosition, Vector3 blockPosition)
    {
        Vector2 _objectXZposition = new Vector2(objectPosition.x, objectPosition.z);
        Vector2 _blockXZposition = new Vector2(blockPosition.x, blockPosition.z);

        return _blockXZposition == _objectXZposition;
    }
}
