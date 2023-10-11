using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreator : MonoBehaviour
{
    [SerializeField] private GameObject _treePrefab;
    private int _treesAmount = 80;
    private float _groundYposition = -10.5f;
    private int _minimalXposition = -16;
    private int _minimalZposition = -15;
    private int _maxXposition = 35;
    private int _maxZposition = 35;

    private void Start()
    {
        CreateTrees(_treesAmount);
    }

    private void CreateTrees(int treesAmount)
    {
        for (int i = 0; i < treesAmount; i++)
        {
            GameObject _tree = Instantiate(_treePrefab, SetTreePosition(), Quaternion.identity);
        }
    }
    private Vector3 SetTreePosition()
    {
        int _xPosition = Random.Range(_minimalXposition, _maxXposition);
        int _zPosition = Random.Range(_minimalZposition, _maxZposition);
        return new Vector3(_xPosition, _groundYposition, _zPosition);
    }
}
