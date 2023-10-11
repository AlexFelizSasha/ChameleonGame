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
    public event EventHandler OnBlockIdleForVisual;
    public event EventHandler OnBlockDestroyedForVisual;
    public static event EventHandler<OnBlockIdleEventArgs> OnBlockIdle;
    public class OnBlockIdleEventArgs : EventArgs
    {
        public Vector3 blockPosition;
    }

    public static event EventHandler<OnBlockDestroyedEventArgs> OnBlockDestroyed;
    public class OnBlockDestroyedEventArgs : EventArgs
    {
        public Vector3 blockPosition;
    }
    [SerializeField] private GameObject _blockVisualObj;

    public enum BlockState
    {
        Creation,
        FirstFlight,
        Idle,
        WithPlayer,
        Replacing,
        Destroying
    }

    public BlockState blockState { get; private set; }
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
        blockState = BlockState.Creation;
        _livingTime = 0;
        _blockVisualObj.GetComponent<BlockVisual>().OnPlayerIsOnBlock += Block_OnPlayerIsOnBlock;
        _blockVisualObj.GetComponent<BlockVisual>().OnPlayerLeavesBlock += Block_OnPlayerLeavesBlock;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (blockState)
        {
            case BlockState.Creation:
                HandleCreationState();
                break;
            case BlockState.FirstFlight:
                HandleFirstFlightState();
                break;
            case BlockState.Idle:
                HandleIdleState();
                break;
            case BlockState.WithPlayer:
                HandleWithPlayerState();
                break;
            case BlockState.Replacing:
                HandleReplacingState();
                break;
            case BlockState.Destroying:
                DestroyBlock();
                break;
        }
    }
    private void HandleCreationState()
    {
        if (_livingTime > _timeForFirstFlight)
        {
            blockState = BlockState.FirstFlight;
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
            OnBlockIdleForVisual?.Invoke(this, EventArgs.Empty);
            isIdle = true;
            blockState = BlockState.Idle;
        }
        isCreated = false;
    }
    private void HandleIdleState()
    {          
        if (_playerIsOnBlock)
        {
            blockState = BlockState.WithPlayer;
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
            blockState = BlockState.Replacing;
        }
        isReplaced = true;
    }
    private void HandleReplacingState()
    {

        MoveBlock(_destroyingPositionY);
        if (transform.position.y == _destroyingPositionY)
        {

            OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs
            {
                blockPosition = transform.position
            });

            OnBlockDestroyedForVisual?.Invoke(this, EventArgs.Empty);
            blockState = BlockState.Destroying;
        }
        isReplaced = false;
    }
    private void DestroyBlock()
    {
        _livingTime = 0;
        isCreated = true;
        _playerLeftBlock = false;

        int _startY = BlocksSpawnPoints.startPositionY;
        Vector3 _startPosition = new Vector3(transform.position.x, _startY, transform.position.z);
        transform.position = _startPosition;
        blockState = BlockState.Creation;
    }

    private void Block_OnPlayerLeavesBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = false;
        _playerLeftBlock = true;
    }

    private void Block_OnPlayerIsOnBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = true;
        //Debug.Log("Block sees player");
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
        return _blockVisualObj;
    }
}

