using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawningObject;
    private GameConstantsSO _gameConstantsSO;

    private int _poolSize;
    private List<GameObject> _poolObjectList;
    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _poolSize = _gameConstantsSO.lootPoolSize;
    }
    private void Start()
    {
        InitializePool();
    }
    private void InitializePool()
    {
        _poolObjectList = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject _poolObject = Instantiate(ChooseObject(_spawningObject), Vector3.zero, Quaternion.identity);
            _poolObject.SetActive(false);
            _poolObject.transform.parent = transform;
            _poolObjectList.Add(_poolObject);
        }
    }
    private GameObject ChooseObject(List<GameObject> objectsList)
    {
        return objectsList[Random.Range(0, objectsList.Count)];
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject spawnedObject in _poolObjectList)
        {
            if (!spawnedObject.activeInHierarchy)
            {
                return spawnedObject;
            }
        }
        return null;
    }
}
