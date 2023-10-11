using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVisual : MonoBehaviour
{
    [SerializeField] private GameObject _parentBlock;
    [SerializeField] private List<MaterialSO> _materialSOList;
    [SerializeField] private GameObject _cloud;

    public event EventHandler OnPlayerIsOnBlock;
    public event EventHandler OnPlayerLeavesBlock;

    private SearchingBlockColor _searchingBlockColor;

    private void Awake()
    {
        _searchingBlockColor = GetComponent<SearchingBlockColor>();
        SetBlockColor(_materialSOList[0]);
        //SetBlockColor(_materialSOList[UnityEngine.Random.Range(0, _materialSOList.Count)]);
        //ChangingSameColors();
    }
    private void Start()
    {
        _parentBlock.gameObject.GetComponent<Block>().OnBlockIdleForVisual += BlockVisual_OnBlockIdleForVisual;
        _parentBlock.gameObject.GetComponent<Block>().OnBlockDestroyedForVisual += BlockVisual_OnBlockDestroyedForVisual;
    }

    private void BlockVisual_OnBlockDestroyedForVisual(object sender, EventArgs e)
    {
        SetBlockColor(_materialSOList[0]);
    }

    private void BlockVisual_OnBlockIdleForVisual(object sender, EventArgs e)
    {
        SetBlockColor(_materialSOList[UnityEngine.Random.Range(1, _materialSOList.Count)]);
        ChangingSameColors();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerRadar>())
        {
            OnPlayerIsOnBlock?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerRadar>())
        {
            OnPlayerLeavesBlock?.Invoke(this, EventArgs.Empty);
        }
    }
    private void SetBlockColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = gameObject.GetComponent<MeshRenderer>();
        var _blockMaterials = _renderer.materials;
        _blockMaterials[0] = _material;
        _renderer.materials = _blockMaterials;

        var _cloudRenderer = _cloud.gameObject.GetComponent<MeshRenderer>();
        var _cloudMaterials = _cloudRenderer.materials;
        _cloudMaterials[0] = _material;
        _cloudRenderer.materials = _cloudMaterials;
    }
    public void ChangingSameColors()
    {
        Block _sameColorBlock = _searchingBlockColor.FindSameColorBlock(transform);
        if (_sameColorBlock != null)
        {
            SetBlockColor(_materialSOList[UnityEngine.Random.Range(1, _materialSOList.Count)]);
        }
    }
    public GameObject GetParentBlock()
    {
        return _parentBlock;
    }
}
