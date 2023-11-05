using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserOnBlockCreator : MonoBehaviour
{
    [SerializeField] private GameObject _geyserPrefab;
    private float _geyserHeightDifference = 0.5f;

    private void Awake()
    {
        GameObject _geyser = Instantiate(_geyserPrefab, GeyserPosition(), Quaternion.identity);        
    }
    private Vector3 GeyserPosition()
    {
        var _position = transform.position;
        Vector3 _geyserPosition = new Vector3(_position.x, _position.y - _geyserHeightDifference, _position.z);
        return _geyserPosition;
    }
}
