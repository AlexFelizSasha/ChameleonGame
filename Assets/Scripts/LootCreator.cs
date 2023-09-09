using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCreator : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;

    private List<Vector3> _blockPositionList;
    private LootPool _lootPool;
    private int _lootAmount = 8;
    private int _startPositionY = 1;
    private float _spawnTime = 3f;
    private float _liveTime;

    private void Awake()
    {
        _lootPool = GetComponent<LootPool>();
    }
    private void Start()
    {
        _blockPositionList = BlocksCreator.Instance.GetBlocksOnMapPositionList();
        for (int i = 0; i < _lootAmount; i++)
        {
            CreateLootOnMap();
        }
        _blockPositionList.Clear();

        LootVisual.OnLootTouchedOnPosition += LootVisual_OnLootTouchedOnPosition;
        Block.OnBlockIdle += Block_OnBlockIdle;
        Block.OnBlockReplacing += Block_OnBlockReplacing;

        _liveTime = -8;
    }
    private void Update()
    {
        //_liveTime += Time.deltaTime;
        //if (_liveTime > _spawnTime)
        //{
        //    CreateLootOnMap();
        //    _liveTime = 0;
        //}
    }

    private void LootVisual_OnLootTouchedOnPosition(object sender, System.EventArgs e)
    {
        Debug.Log("CREATE LOOT NOW!");
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