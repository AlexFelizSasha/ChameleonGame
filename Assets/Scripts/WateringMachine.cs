using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMachine : MonoBehaviour
{
    [SerializeField] private List<Transform> _machineRoutePoints;
    private float _moveSpeed = 3f;

    private enum MachineState
    {
        Idle,
        SimpleMove,
        MoveAndWater,
        MoveBackAndWater,
        MoveToWaterBarrel
    }
    private MachineState _machineState;
    private void Start()
    {
        _machineState = MachineState.Idle;
    }

    private void Update()
    {
        switch (_machineState)
        {
            case MachineState.Idle:
                transform.position = _machineRoutePoints[0].position;
                if (transform.position == _machineRoutePoints[0].position)
                    SetMachineDirection(_machineRoutePoints[1].position);
                if (WaterBarrel.isFull)
                    _machineState = MachineState.SimpleMove;
                break;
            case MachineState.SimpleMove:
                MoveToPosition(_machineRoutePoints[1].position);
                if (transform.position == _machineRoutePoints[1].position)
                    _machineState = MachineState.MoveAndWater;
                break;
            case MachineState.MoveAndWater:
                MoveToPosition(_machineRoutePoints[2].position);
                if (transform.position == _machineRoutePoints[2].position)
                    _machineState = MachineState.MoveBackAndWater;
                break;
            case MachineState.MoveBackAndWater:
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
