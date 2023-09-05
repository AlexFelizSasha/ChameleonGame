using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private Transform _lootVisual;

    [SerializeField] private List<MaterialSO> _materialSOList;

    private void SetLootColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = _lootVisual.gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
    }
    
}
