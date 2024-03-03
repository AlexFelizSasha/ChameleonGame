using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMachine : MonoBehaviour
{
    [SerializeField] private List<Transform> _machineRoutePoints;
    public static WateringMachine instance { get; private set; }

    public event EventHandler OnStartWatering;
    public event EventHandler OnStopWatering;

    private GameConstantsSO _gameConstantsSO;
    private float _moveSpeed;

    private bool _isStopWatering;
    private bool _isStartWatering;

    private enum MachineState
    {
        Idle,
        StartMove,
        MoveAndWater,
        MoveBackAfterWater,
        MoveToWaterBarrel
    }
    private MachineState _machineState;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _moveSpeed = _gameConstantsSO.waterMachineMoveSpeed;

    }
    private void Start()
    {
        _machineState = MachineState.Idle;
    }

    private void Update()
    {
        switch (_machineState)
        {
            case MachineState.Idle:
                _isStopWatering = false;
                _isStartWatering = false;
                transform.position = _machineRoutePoints[0].position;
                if (transform.position == _machineRoutePoints[0].position)
                    SetMachineDirection(_machineRoutePoints[1].position);
                if (WaterBarrel.isFull)
                    _machineState = MachineState.StartMove;
                break;
            case MachineState.StartMove:
                MoveToPosition(_machineRoutePoints[1].position);
                if (transform.position == _machineRoutePoints[1].position)
                {
                    if (!_isStartWatering)
                    {
                        OnStartWatering?.Invoke(this, EventArgs.Empty);
                        _isStartWatering = true;
                    }
                    _machineState = MachineState.MoveAndWater;
                }
                break;
            case MachineState.MoveAndWater:
                MoveToPosition(_machineRoutePoints[2].position);
                if (transform.position == _machineRoutePoints[2].position)
                {
                    if (!_isStopWatering)
                    {
                        OnStopWatering?.Invoke(this, EventArgs.Empty);
                        _isStopWatering = true;
                    }
                    _machineState = MachineState.MoveBackAfterWater;
                }
                break;
            case MachineState.MoveBackAfterWater:
                MoveToPosition(_machineRoutePoints[1].position);
                if (transform.position == _machineRoutePoints[1].position)
                    _machineState = MachineState.MoveToWaterBarrel;
                break;
            case MachineState.MoveToWaterBarrel:
                MoveToPosition(_machineRoutePoints[0].position);
                if (transform.position == _machineRoutePoints[0].position)
                    _machineState = MachineState.Idle;
                break;
        }
    }
    private void MoveToPosition(Vector3 objectPosition)
    {
        float _speed = _moveSpeed * Time.deltaTime;
        Vector3 _movementPoint = objectPosition;
        Vector3 _moveDirection = _movementPoint - transform.position;
        transform.forward = _moveDirection.normalized;
        transform.position = Vector3.MoveTowards(transform.position, _movementPoint, _speed);
    }
    private void SetMachineDirection(Vector3 directionPosition)
    {
        Vector3 _direction = directionPosition - transform.position;
        transform.forward = _direction.normalized;
    }
}
