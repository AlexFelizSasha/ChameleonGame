using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaggage : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private int _positionY = -10;

    private void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 _baggagePosition = new Vector3(_playerTransform.position.x, _positionY, _playerTransform.position.z);
        transform.position = _baggagePosition;
        transform.rotation = _playerTransform.rotation;
    }
}
