using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootCreator : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;

    private List<Vector3> _blockPositionList;
    private LootPool _lootPool;
    private int _lootAmount;
    private int _startPositionY;

    private void Awake()
    {
        _lootAmount = ConstantsKeeper.LOOT_AMOUNT;
        _startPositionY = ConstantsKeeper.CLOUDS_Y_POSITION;
        _lootPool = GetComponent<LootPool>();
    }
    private void Start()
    {
        _blockPositionList = BlocksCreator.Instance.GetBlocksOnMapPositionList().ToList();
        //Debug.Log(_blockPositionList.Count + " Loot amount");
        for (int i = 0; i < _lootAmount; i++)
        {
            CreateLootOnMap();
        }
        _blockPositionList.Clear();

        Block.OnBlockIdle += Block_OnBlockIdle;
        Block.OnBlockReplacing += Block_OnBlockReplacing;
        Loot.OnLootDestroyed += Loot_OnLootDestroyed;
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