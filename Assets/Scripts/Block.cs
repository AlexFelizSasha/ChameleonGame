using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _blockVisual;
    [SerializeField] private MaterialSO _materialSO;
    

    private void Awake()
    {
        SetBlockolor(_materialSO);
    }
    public Vector3 GetBlockVisulPosition()
    {
        return _blockVisual.transform.position;
    }
    public GameObject GetBlockVisual()
    {
        return _blockVisual;    
    }
    private void SetBlockolor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = _blockVisual.gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
    }
}

