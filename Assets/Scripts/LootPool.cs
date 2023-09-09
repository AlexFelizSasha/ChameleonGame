using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawningObject;

    private int _poolSize;
    private List<GameObject> _poolObjectList;
    private void Awake()
    {
        _poolSize = 10;
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
            GameObject poolObject = Instantiate(ChooseObject(_spawningObject), Vector3.zero, Quaternion.identity);
            poolObject.SetActive(false);
            _poolObjectList.Add(poolObject);
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
