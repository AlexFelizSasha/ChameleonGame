using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _blockVisual;
    [SerializeField] private List<MaterialSO> _materialSOList;

    private SearchingBlockColor _searchingBlockColor;
    

    private void Awake()
    {
        _searchingBlockColor = _blockVisual.GetComponent<SearchingBlockColor>();
        SetBlockColor(_materialSOList[Random.Range(0, _materialSOList.Count)]);
    }
    private void Update()
    {
        ChangingSameColors();
    }
    public GameObject GetBlockVisual()
    {
        return _blockVisual;    
    }
    private void SetBlockColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = _blockVisual.gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
    }
    private void ChangingSameColors()
    {
        Block _sameColorBlock = _searchingBlockColor.FindSameColorBlock(_blockVisual.transform);
        if (_sameColorBlock != null)
        {
            SetBlockColor(_materialSOList[Random.Range(0, _materialSOList.Count)]);
        }
    }
}

