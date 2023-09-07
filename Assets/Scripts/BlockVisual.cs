using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVisual : MonoBehaviour
{
    public event EventHandler OnPlayerIsOnBlock;
    public event EventHandler OnPlayerLeavesBlock;
    [SerializeField] private GameObject _parentBlock;
    [SerializeField] private List<MaterialSO> _materialSOList;

    private SearchingBlockColor _searchingBlockColor;

    private void Awake()
    {
        _searchingBlockColor = GetComponent<SearchingBlockColor>();
        SetBlockColor(_materialSOList[UnityEngine.Random.Range(0, _materialSOList.Count)]);
        ChangingSameColors();
    }
    private void Start()
    {
        _parentBlock.GetComponent<Block>().OnBlockDestroyed += BlockVisual_OnBlockDestroyed;
    }

    private void BlockVisual_OnBlockDestroyed(object sender, EventArgs e)
    {
        SetBlockColor(_materialSOList[UnityEngine.Random.Range(0, _materialSOList.Count)]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerRadar>())
        {
            OnPlayerIsOnBlock?.Invoke(this, EventArgs.Empty);
            Debug.Log("BlockVisual sees player");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerRadar>())
        {
            OnPlayerLeavesBlock?.Invoke(this, EventArgs.Empty);
            Debug.Log("BlockVisual sees player leaves");
        }
    }
    private void SetBlockColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
    }
    private void ChangingSameColors()
    {
        Block _sameColorBlock = _searchingBlockColor.FindSameColorBlock(transform);
        if (_sameColorBlock != null)
        {
            SetBlockColor(_materialSOList[UnityEngine.Random.Range(0, _materialSOList.Count)]);
        }
    }
    public GameObject GetParentBlock()
    {
        return _parentBlock;
    }
}
