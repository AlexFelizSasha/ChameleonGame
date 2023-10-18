using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeyserCreator : MonoBehaviour
{
    [SerializeField] private GameObject _geyserPrefab;
    private List<Vector3> _geyserPositionList;
    private float _geyserHeightDifference = 0.8f;

    private void Start()
    {
        _geyserPositionList = BlocksCreator.Instance.GetBlocksOnMapPositionList().ToList();
        CreatGeysersOnMap();
    }

    private void CreatGeysersOnMap()
    {
        for (int i = 0; i < _geyserPositionList.Count; i++)
        {
            Vector3 _geyserPosition = _geyserPositionList[i];
            float _geyserLowerYposition = _geyserPosition.y - _geyserHeightDifference;
            Vector3 _geyserLowerPosition = new Vector3(_geyserPosition.x, _geyserLowerYposition, _geyserPosition.z);
            GameObject _geyser = Instantiate(_geyserPrefab, _geyserLowerPosition, Quaternion.identity);
        }
    }
    private void PrintGeyserPositionList()
    {
        for (int i = 0; i < _geyserPositionList.Count; i++)
            Debug.Log(_geyserPositionList[i]);
    }
}
