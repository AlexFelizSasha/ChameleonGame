using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event EventHandler OnBlockDestroyed;
    [SerializeField] private GameObject _blockVisual;
    private enum State
    {
        Created,
        FirstFlight,
        WaitingForPLayer,
        WithPlayer,
        Replaced,
        Destroyed
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

    private void Start()
    {
        _state = State.Created;
        _livingTime = 0;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerIsOnBlock += Block_OnPlayerIsOnBlock;
        _blockVisual.GetComponent<BlockVisual>().OnPlayerLeavesBlock += Block_OnPlayerLeavesBlock;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (_state)
        {
            case State.Created:
                if (_livingTime > _timeForFirstFlight)
                {
                    _state = State.FirstFlight;
                }
                break;
            case State.FirstFlight:
                MoveBlock(_gamePlayPositionY);
                if (transform.position.y == _gamePlayPositionY)
                {
                    _state = State.WaitingForPLayer;
                }
                break;
            case State.WaitingForPLayer:
                if (_playerIsOnBlock)
                {
                    _state = State.WithPlayer;
                }
                break;
            case State.WithPlayer:
                if (_playerLeftBlock)
                {
                    _state = State.Replaced;
                }
                break;
            case State.Replaced:
                MoveBlock(_destroyingPositionY);
                if (transform.position.y == _destroyingPositionY)
                {
                    _state = State.Destroyed;
                }
                break;
            case State.Destroyed:
                DestroyBlock();
                break;
        }
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
        OnBlockDestroyed?.Invoke(this, EventArgs.Empty);

        _livingTime = 0;
        _playerLeftBlock = false;
        int _startY = BlocksSpawnPoints.startPositionY;
        Vector3 _startPosition = new Vector3(transform.position.x, _startY, transform.position.z);
        transform.position = _startPosition;
        _state = State.Created;
    }
    public GameObject GetBlockVisual()
    {
        return _blockVisual;
    }

}

