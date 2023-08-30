using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private GameObject _playerRadar;

    private SearchingBlockColor _blockColorSearch;
    private Transform _pointToMove;
    private float _moveSpeed = 10f;

    private void Awake()
    {
        _blockColorSearch = _playerRadar.GetComponent<SearchingBlockColor>();
        _pointToMove = null;
    }
    private void Start()
    {
        ColorButtonsManagerUI.OnColorChangedWithButton += ColorButtonsManager_OnColorChangedWithButton;
    }
    private void Update()
    {
        if (_pointToMove != null)
        {
            MoveToNewPoint(_pointToMove);
        }
    }

    private void ColorButtonsManager_OnColorChangedWithButton(object sender, ColorButtonsManagerUI.OnColorChangedWithButtonEventArgs e)
    {
        ChangeSkinColor(e.materialSO);
        ChangeDirectionToChosenColor();
        if (FindBlockToMove() != null)
        {
            _pointToMove = FindBlockToMove().transform;
        }
    }
    private void ChangeSkinColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = _playerVisual.gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
        Debug.Log("Color Changed to " + materialSO.Color);
    }
    private void ChangeDirectionToChosenColor()
    {
        if (FindBlockToMove() != null)
        {
            Vector3 _blockPosition = FindBlockToMove().transform.position;
            Vector3 _newForwardDirection = _blockPosition - transform.position;
            transform.forward = _newForwardDirection.normalized;
        }
    }
    private Block FindBlockToMove()
    {
        Block _blockToMove = null;
        var _playerMaterials = _playerVisual.gameObject.GetComponent<MeshRenderer>().materials;
        List<GameObject> _closeBlocks = _blockColorSearch.GetCloseBlocksList();
        foreach (var block in _closeBlocks)
        {
            if (block != null)
            {
                var _blockVisual = block.GetComponent<Block>().GetBlockVisual();
                var _blockMaterials = _blockVisual.GetComponent<MeshRenderer>().materials;
                if (_blockMaterials[0].name == _playerMaterials[0].name)
                {
                    _blockToMove = block.GetComponent<Block>();
                }
            }
        }
        return _blockToMove;
    }
    private void MoveToNewPoint(Transform pointTransform)
    {
        float _moveDistance = _moveSpeed * Time.deltaTime;
        Vector3 _blockPosition = pointTransform.position;

        transform.position = Vector3.MoveTowards(transform.position, _blockPosition, _moveDistance);
    }
}
