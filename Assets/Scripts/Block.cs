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
    private enum State
    {
        Creation,
        FirstFlight,
        Idle,
        WithPlayer,
        Replacing,
        Destroying
    }

    private State _state;
    private float _blockFlySpeedMin = 1f;
    private float _blockFlySpeedMax = 5f;

    private float _gamePlayPositionY = 1f;
    private float _destroyingPositionY = 20f;
    private bool _playerLeftBlock = false;
    private bool _playerIsOnBlock = false;
    private float _livingTime;
    private float _timeForFirstFlight = 1f;
    public bool _isCreated = false;
    public bool _isReplaced = false;

    private void Start()
    {
        _state = State.Creation;
        _livingTime = 0;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerIsOnBlock += Block_OnPlayerIsOnBlock;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerLeavesBlock += Block_OnPlayerLeavesBlock;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (_state)
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
            _state = State.FirstFlight;
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
            _state = State.Idle;
        }
        _isCreated = false;
    }
    private void HandleIdleState()
    {
        if (_playerIsOnBlock)
        {
            _state = State.WithPlayer;
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
            _state = State.Replacing;
        }
        _isReplaced = true;
    }
    private void HandleReplacingState()
    {

        MoveBlock(_destroyingPositionY);
        if (transform.position.y == _destroyingPositionY)
        {
            _state = State.Destroying;
        }
        _isReplaced = false;
    }

    private void Block_OnPlayerLeavesBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = false;
        Debug.Log("Block sees player leaves");
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
    private void DestroyBlock()
    {
        _livingTime = 0;
        _isCreated = true;
        _playerLeftBlock = false;
        OnBlockDestroyed?.Invoke(this, EventArgs.Empty);

        int _startY = BlocksSpawnPoints.startPositionY;
        Vector3 _startPosition = new Vector3(transform.position.x, _startY, transform.position.z);
        transform.position = _startPosition;
        _state = State.Creation;
    }
    public GameObject GetBlockVisual()
    {
        return _blockVisual;
    }
}

