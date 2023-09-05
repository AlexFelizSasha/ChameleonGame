using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCreator : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    private int _lootAmount = 8;
    private void Start()
    {
        CreateLootOnMap();
    }

    private void CreateLootOnMap()
    {
        List<Vector3> _lootPositionList = BlocksCreator.Instance.GetBlocksOnMapPositionList();
        for (int i = 0; i < _lootAmount; i++)
        {
            Vector3 _blockPosition = _lootPositionList[Random.Range(0, _lootPositionList.Count)];
            Vector3 _lootPosition = new Vector3(_blockPosition.x, _blockPosition.y, _blockPosition.z);
            GameObject _loot = Instantiate(_lootPrefab, _lootPosition, Quaternion.identity);
            _lootPositionList.Remove(_blockPosition);
        }
    }


}
