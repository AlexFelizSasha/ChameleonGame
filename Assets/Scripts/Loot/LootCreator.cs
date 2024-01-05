using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootCreator : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private GameConstantsSO _gameConstantsSO;

    private List<Vector3> _blockPositionList;
    private LootPool _lootPool;
    private int _lootAmount;
    private float _startPositionY;

    private void Awake()
    {
        _lootAmount = _gameConstantsSO.lootAmount;
        _startPositionY = _gameConstantsSO.startPositionY;
        _lootPool = GetComponent<LootPool>();
    }
    private void Start()
    {
        _blockPositionList = BlocksCreator.Instance.GetBlocksOnMapPositionList().ToList();
        for (int i = 0; i < _lootAmount; i++)
        {
            CreateLootOnMap();
        }
        _blockPositionList.Clear();

        Block.OnBlockIdle += Block_OnBlockIdle;
        Block.OnBlockReplacing += Block_OnBlockReplacing;
        Loot.OnLootDestroyed += Loot_OnLootDestroyed;
    }
    private void OnDisable()
    {
        Block.OnBlockIdle -= Block_OnBlockIdle;
        Block.OnBlockReplacing -= Block_OnBlockReplacing;
        Loot.OnLootDestroyed -= Loot_OnLootDestroyed;
    }
    private void Loot_OnLootDestroyed(object sender, Loot.OnLootDroppedEventArgs e)
    {
        CreateLootOnMap();
    }
    private void Block_OnBlockReplacing(object sender, Block.OnBlockReplacingEventArgs e)
    {
        _blockPositionList.Remove(e.blockPosition);
    }

    private void Block_OnBlockIdle(object sender, Block.OnBlockIdleEventArgs e)
    {
        _blockPositionList.Add(e.blockPosition);
    }

    private void CreateLootOnMap()
    {
        Vector3 _blockPosition = _blockPositionList[Random.Range(0, _blockPositionList.Count)];
        Vector3 _lootPosition = new Vector3(_blockPosition.x, _startPositionY, _blockPosition.z);

        GameObject spawnedObject = _lootPool.GetPooledObject();

        if (spawnedObject != null)
        {
            spawnedObject.transform.position = _lootPosition;
            spawnedObject.SetActive(true);
        }
    }
}