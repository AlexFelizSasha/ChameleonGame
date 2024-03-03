using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameConstantsSO _gameConstantsSO;
    public static event EventHandler<OnBlockReplacingEventArgs> OnBlockReplacing;
    public class OnBlockReplacingEventArgs : EventArgs
    {
        public Vector3 blockPosition;
    }
    public event EventHandler OnBlockIdleForVisual;
    public event EventHandler OnBlockDestroyedForVisual;
    public static event EventHandler OnKillPlayer;
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
        WithPlayerAndDrop,
        Replacing,
        WithDrop,
        Destroying
    }

    public BlockState blockState { get; private set; }
    private float _blockFlySpeedMin = 1f;
    private float _blockFlySpeedMax = 5f;

    private int _gamePlayPositionY = 1;
    private float _destroyingPositionY = 20f;
    private float _livingTime;
    private float _timeForFirstFlight = 1f;
    private float _timeWithPlayer;
    private float _killPlayerTime;
    private float _dropDelayTime;
    private bool _playerLeftBlock = false;
    private bool _playerIsOnBlock = false;

    private GeyserCollider _geyserCollider;

    public bool isCreated = false;
    public bool isReplaced = false;
    public bool isIdle = false;

    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _geyserCollider = GetComponent<GeyserCollider>();
        _killPlayerTime = _gameConstantsSO.killPlayerTime;
        _dropDelayTime = _gameConstantsSO.dropDelayTime;
        _timeWithPlayer = 0;
        _livingTime = 0;
    }
    private void Start()
    {
        blockState = BlockState.Creation;

        _blockVisualObj.GetComponent<BlockVisual>().OnPlayerIsOnBlock += Block_OnPlayerIsOnBlock;
        _blockVisualObj.GetComponent<BlockVisual>().OnPlayerLeavesBlock += Block_OnPlayerLeavesBlock;
        GeyserCollider.OnDropForBlock += GeyserCollider_OnDropForBlock;
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
            case BlockState.WithPlayerAndDrop:
                HandleWithPlayerAndDropState();
                break;
            case BlockState.WithDrop:
                HandleWithDropState();
                break;
        }
    }
    private void OnDisable()
    {
        GeyserCollider.OnDropForBlock -= GeyserCollider_OnDropForBlock;
    }
    private void HandleCreationState()
    {

        if (_livingTime > _timeForFirstFlight)
        {
            isCreated = true;
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
        isIdle = false;
        if (_playerIsOnBlock)
        {
            _timeWithPlayer = 0;

            blockState = BlockState.WithPlayer;
        }
    }
    private void HandleWithPlayerState()
    {
        KillPlayer();
        if (_playerLeftBlock)
        {
            ReplaceBlock();
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
    private void HandleWithPlayerAndDropState()
    {
        KillPlayer();
        if (_playerLeftBlock)
        {
            blockState = BlockState.WithDrop;
        }
    }
    private void HandleWithDropState()
    {
        MoveBlock(_destroyingPositionY);
        if (_livingTime > 0)
            DestroyBlock();
    }
    private void ReplaceBlock()
    {
        OnBlockReplacing?.Invoke(this, new OnBlockReplacingEventArgs
        {
            blockPosition = transform.position
        });
        _playerIsOnBlock = false;
        blockState = BlockState.Replacing;
    }
    private void DestroyBlock()
    {
        _livingTime = 0;
        _playerLeftBlock = false;
        isReplaced = false;

        int _startY = BlocksSpawnPoints.startPositionY - 1;
        Vector3 _startPosition = new Vector3(transform.position.x, _startY, transform.position.z);
        transform.position = _startPosition;
        blockState = BlockState.Creation;
    }
    private void KillPlayer()
    {
        _timeWithPlayer += Time.deltaTime;
        if (_timeWithPlayer > _killPlayerTime)
        {
            ReplaceBlock();
            OnKillPlayer?.Invoke(this, EventArgs.Empty);
        }
    }
    private void Block_OnPlayerLeavesBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = false;
        _playerLeftBlock = true;
    }

    private void Block_OnPlayerIsOnBlock(object sender, System.EventArgs e)
    {
        _playerIsOnBlock = true;
    }
    private void GeyserCollider_OnDropForBlock(object sender, GeyserCollider.OnDropForBlockEventArgs e)
    {
        if (ComparisonXZPositions.EqualXZPositions(transform.position, e.position))
        {
            _livingTime = -_dropDelayTime;
            if (_playerIsOnBlock)
            {
                blockState = BlockState.WithPlayerAndDrop;
            }
            else
            {
                blockState = BlockState.WithDrop;
            }
        }
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

