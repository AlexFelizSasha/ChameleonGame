using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static event EventHandler<OnBlockReplacingEventArgs> OnBlockReplacing;
    public class OnBlockReplacingEventArgs : EventArgs
    {
        public Vector3 blockPosition;
    }
    public static event EventHandler<OnBlockIdleEventArgs> OnBlockIdle;
    public class OnBlockIdleEventArgs : EventArgs
    {
        public Vector3 blockPosition;
    }

    public event EventHandler OnBlockDestroyed;
    [SerializeField] private GameObject _blockVisual;
    public enum State
    {
        Creation,
        FirstFlight,
        Idle,
        WithPlayer,
        Replacing,
        Destroying
    }

    public State state { get; private set; }
    private float _blockFlySpeedMin = 1f;
    private float _blockFlySpeedMax = 5f;

    private int _gamePlayPositionY = 1;
    private float _destroyingPositionY = 20f;
    private float _livingTime;
    private float _timeForFirstFlight = 1f;
    private bool _playerLeftBlock = false;
    private bool _playerIsOnBlock = false;

    public bool isCreated = false;
    public bool isReplaced = false;
    public bool isIdle = false;

    private void Start()
    {
        state = State.Creation;
        _livingTime = 0;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerIsOnBlock += Block_OnPlayerIsOnBlock;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerLeavesBlock += Block_OnPlayerLeavesBlock;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (state)
        {
            case State.Creation:
                HandleCreationState();
                break;
            case State.FirstFlight:
                HandleFirstFlightState();
                break;
            case State.Idle:
                HandleIdleState();
                break;
            case State.WithPlayer:
                HandleWithPlayerState();
                break;
            case State.Replacing:
                HandleReplacingState();
                break;
            case State.Destroying:
                DestroyBlock();
                break;
        }
    }
    private void HandleCreationState()
    {
        if (_livingTime > _timeForFirstFlight)
        {
            state = State.FirstFlight;
        }
    }
    private void HandleFirstFlightState()
    {
        MoveBlock(_gamePlayPositionY);
        if (transform.position.y == _gamePlayPositionY)
        {
            OnBlockIdle?.Invoke(this, new OnBlockIdleEventArgs
            {
                blockPosition = transform.position
            });
            isIdle = true;
            state = State.Idle;
        }
        isCreated = false;
    }
    private void HandleIdleState()
    {          
        if (_playerIsOnBlock)
        {
            state = State.WithPlayer;
            isIdle = false;
        }
    }
    private void HandleWithPlayerState()
    {
        if (_playerLeftBlock)
        {
            OnBlockReplacing?.Invoke(this, new OnBlockReplacingEventArgs
            {
                blockPosition = transform.position
            });
            state = State.Replacing;
        }
        isReplaced = true;
    }
    private void HandleReplacingState()
    {

        MoveBlock(_destroyingPositionY);
        if (transform.position.y == _destroyingPositionY)
        {
            state = State.Destroying;
        }
        isReplaced = false;
    }
    private void DestroyBlock()
    {
        _livingTime = 0;
        isCreated = true;
        _playerLeftBlock = false;
        OnBlockDestroyed?.Invoke(this, EventArgs.Empty);

        int _startY = BlocksSpawnPoints.startPositionY;
        Vector3 _startPosition = new Vector3(transform.position.x, _startY, transform.position.z);
        transform.position = _startPosition;
        state = State.Creation;
    }

    private void Block_OnPlayerLeavesBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = false;
        _playerLeftBlock = true;
    }

    private void Block_OnPlayerIsOnBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = true;
        Debug.Log("Block sees player");
    }
    private void MoveBlock(float positionY)
    {
        float _blockFlySpeed = UnityEngine.Random.Range(_blockFlySpeedMin, _blockFlySpeedMax);
        float _moveDistance = _blockFlySpeed * Time.deltaTime;
        Vector3 _pointToMove = new Vector3(transform.position.x, positionY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _pointToMove, _moveDistance);
    }
    public GameObject GetBlockVisual()
    {
        return _blockVisual;
    }
}

