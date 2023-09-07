using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaggage : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 _baggagePosition = new Vector3(_playerTransform.position.x, -10, _playerTransform.position.z);
        transform.position = _baggagePosition;
    }
}
