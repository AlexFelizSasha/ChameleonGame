using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerBaggage : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _waterBarrel;
    private GameConstantsSO _gameConstantsSO;

    public static event EventHandler<OnScoreUnloadEventArgs> OnScoreUnload;
    public class OnScoreUnloadEventArgs : EventArgs
    {
        public int unloadScore;
    }

    private Vector3 _underPlayerPosition;

    private int _positionY;
    private int _score;
    private int _unloadScore;
    private float _moveSpeed;
    private float _unloadTime;
    private float _unloadCounter;

    public enum PlayerBaggageState
    {
        Harvest,
        MoveToBarrel,
        Unload
    }
    public PlayerBaggageState playerBaggageState { get; private set; }

    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _positionY = _gameConstantsSO.baggagePositionY;
        _unloadScore = _gameConstantsSO.baggageUnloadScore;
        _moveSpeed = _gameConstantsSO.baggageMoveSpeed;
        _unloadTime = _gameConstantsSO.baggageUnloadTime;
    }

    private void Start()
    {
        _unloadCounter = _unloadTime;
        playerBaggageState = PlayerBaggageState.Harvest;
        Loot.OnLootScoreAdd += Loot_OnLootScoreAdd;
    }
    private void Update()
    {
        switch (playerBaggageState)
        {
            case PlayerBaggageState.Harvest:
                _underPlayerPosition = new Vector3(_playerTransform.position.x, _positionY, _playerTransform.position.z);
                MoveToPosition(_underPlayerPosition);
                if (_score == _unloadScore)
                    playerBaggageState = PlayerBaggageState.MoveToBarrel;
                break;
            case PlayerBaggageState.MoveToBarrel:
                MoveToPosition(_waterBarrel.position);

                if (transform.position == _waterBarrel.position)
                {
                    //Debug.Log("baggage is on barrel!");
                    playerBaggageState = PlayerBaggageState.Unload;
                }
                break;
            case PlayerBaggageState.Unload:
                _unloadCounter -= Time.deltaTime;
                if (_unloadCounter < 0)
                {
                    _unloadCounter = _unloadTime;

                    OnScoreUnload?.Invoke(this, new OnScoreUnloadEventArgs
                    {
                        unloadScore = _score
                    });
                    _score = 0;
                    playerBaggageState = PlayerBaggageState.Harvest;
                }
                break;
        }
    }
    private void OnDisable()
    {
        Loot.OnLootScoreAdd -= Loot_OnLootScoreAdd;
    }
    private void Loot_OnLootScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    {
        _score += e.lootScore;
        if (_score > _unloadScore)
            _score = _unloadScore;
    }
    private void MoveToPosition(Vector3 objectPosition)
    {
        float _speed = _moveSpeed * Time.deltaTime;
        Vector3 _movementPoint = objectPosition;
        Vector3 _moveDirection = _movementPoint - transform.position;
        if (_moveDirection != Vector3.zero)
            transform.forward = _moveDirection.normalized;
        transform.position = Vector3.MoveTowards(transform.position, _movementPoint, _speed);
    }
    public float GetWaterAmount()
    {
        float _waterAmount = (float)_score / _unloadScore;
        return _waterAmount;
    }
}
