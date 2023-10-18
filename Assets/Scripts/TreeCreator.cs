using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreator : MonoBehaviour
{
    [SerializeField] private GameObject _boldTreePrefab;
    [SerializeField] private GameObject _gardenTreePrefab;
    private int _boldTreesAmount = 150;
    private int _gardenTreesAmount = 80;
    private float _groundYposition = -10.5f;
    //private int _minimalXposition = -16;
    //private int _minimalZposition = -15;
    //private int _maxXposition = 35;
    //private int _maxZposition = 35;

    [SerializeField] private Transform _worldBottomLeftPoint;
    [SerializeField] private Transform _worldTopRightPoint;
    [SerializeField] private Transform _toxicBottomLeftPoint;
    [SerializeField] private Transform _toxicTopRightPoint;
    [SerializeField] private Transform _gardenBottomLeftPoint;
    [SerializeField] private Transform _gardenTopRightPoint;
    [SerializeField] private Transform _barrelBottomLeftPoint;
    [SerializeField] private Transform _barrelTopRightPoint;

    private List<Vector3> _boldTreePositionList = new List<Vector3>();
    private List<Vector3> _gardenTreePositionList = new List<Vector3>();
    private void Start()
    {
        SetBoldTreesPositions();
        SetGardenTreePosition();
        CreatePlants(_boldTreePrefab, _boldTreePositionList);
        CreatePlants(_gardenTreePrefab, _gardenTreePositionList);
    }

    private void CreatePlants(GameObject prefab, List<Vector3> positionsList)
    {
        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector3 _rotation = new Vector3(0, Random.Range(0, 180), 0);
            GameObject _tree = Instantiate(prefab, positionsList[i], Quaternion.Euler(_rotation));
        }
    }
    private void SetGardenTreePosition()
    {
        while (_gardenTreePositionList.Count <= _gardenTreesAmount)
        {
            float _xPosition = Random.Range(_gardenBottomLeftPoint.position.x, _gardenTopRightPoint.position.x);
            float _zPosition = Random.Range(_gardenBottomLeftPoint.position.z, _gardenTopRightPoint.position.z);
            _gardenTreePositionList.Add(new Vector3(_xPosition, _groundYposition, _zPosition));
        }
    }
    private void SetBoldTreesPositions()
    {
        float _minimalXposition = _worldBottomLeftPoint.position.x;
        float _maxXposition = _worldTopRightPoint.position.x;
        float _minimalZposition = _worldBottomLeftPoint.position.z;
        float _maxZposition = _worldTopRightPoint.position.z;

        while (_boldTreePositionList.Count <= _boldTreesAmount)
        {
            float _xPosition = Random.Range(_minimalXposition, _maxXposition);
            float _zPosition = Random.Range(_minimalZposition, _maxZposition);
            if (_xPosition > _toxicBottomLeftPoint.position.x && _xPosition < _toxicTopRightPoint.position.x &&
                _zPosition > _toxicBottomLeftPoint.position.z && _zPosition < _toxicTopRightPoint.position.z)
                continue;
            if (_xPosition > _gardenBottomLeftPoint.position.x && _xPosition < _gardenTopRightPoint.position.x &&
                _zPosition > _gardenBottomLeftPoint.position.z && _zPosition < _gardenTopRightPoint.position.z)
                continue;
            if (_xPosition > _barrelBottomLeftPoint.position.x && _xPosition < _barrelTopRightPoint.position.x &&
                _zPosition > _barrelBottomLeftPoint.position.z && _zPosition < _barrelTopRightPoint.position.z)
                continue;
            _boldTreePositionList.Add(new Vector3(_xPosition, _groundYposition, _zPosition));
        }
    }
}
