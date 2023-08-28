using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private GameObject _playerRadar;

    private SearchingBlockColor _blockColorSearch;
    private void Awake()
    {
        _blockColorSearch = _playerRadar.GetComponent<SearchingBlockColor>();
    }
    private void Start()
    {
        ColorButtonsManagerUI.OnColorChangedWithButton += ColorButtonsManager_OnColorChangedWithButton;
    }
    private void Update()
    {

    }

    private void ColorButtonsManager_OnColorChangedWithButton(object sender, ColorButtonsManagerUI.OnColorChangedWithButtonEventArgs e)
    {
        ChangeSkinColor(e.materialSO);
        ChangeDirectionToColor();
    }
    private void ChangeSkinColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var renderer = _playerVisual.gameObject.GetComponent<MeshRenderer>();
        var playerMaterials = renderer.materials;
        playerMaterials[0] = _material;
        renderer.materials = playerMaterials;
        Debug.Log("Color Changed to " + materialSO.Color);
    }
    private void ChangeDirectionToColor()
    {
        Vector3 _heightDifference = new Vector3(0, 1.25f, 0);
        Vector3 _rotateDirection = FindBlockToMove().transform.position + _heightDifference;
        transform.forward = _rotateDirection;
        Debug.Log("New direction is " + _rotateDirection);
    }
    private GameObject FindBlockToMove()
    {
        GameObject _blockToMove = gameObject;
        var _playerMaterials = _playerVisual.gameObject.GetComponent<MeshRenderer>().materials;
        List<GameObject> _closeBlocks = _blockColorSearch.GetCloseBlocksList();
        foreach (var block in _closeBlocks)
        {
            if (block != null)
            {
                var _blockMaterials = block.GetComponent<MeshRenderer>().materials;
                Debug.Log($"{block}, {_blockMaterials[0]}, {_playerMaterials[0]}");
                if (_blockMaterials[0].name == _playerMaterials[0].name)
                {
                    _blockToMove = block;
                    Debug.Log(_blockToMove);
                }
            }
        }
        return _blockToMove;
    }
}
