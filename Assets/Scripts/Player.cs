using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler<OnLeavingBlockEventArgs> OnLeavingBlock;
    public class OnLeavingBlockEventArgs : EventArgs
    {
        public Transform leftBlockTransform;
    }

    [SerializeField] private Transform _playerVisual;
    [SerializeField] private Transform _playerRadar;

    private SearchingBlockColor _blockColorSearch;
    private Transform _pointToMove;
    private float _moveSpeed = 10f;
    public bool _isMoving = false;

    private void Awake()
    {
        _blockColorSearch = _playerRadar.gameObject.GetComponent<SearchingBlockColor>();
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
            MoveToNewBlock(_pointToMove);
        }
    }

    private void ColorButtonsManager_OnColorChangedWithButton(object sender, ColorButtonsManagerUI.OnColorChangedWithButtonEventArgs e)
    {
        ChangeSkinColor(e.materialSO);
        ChangeDirectionToChosenColor();
        Block _sameColorBlock = _blockColorSearch.FindSameColorBlock(_playerVisual, _playerRadar);
        if (_sameColorBlock != null)
        {
            _pointToMove = _sameColorBlock.transform;
        }
    }
    private void ChangeSkinColor(MaterialSO materialSO)
    {
        Material _material = materialSO.Material;
        var _renderer = _playerVisual.gameObject.GetComponent<MeshRenderer>();
        var _playerMaterials = _renderer.materials;
        _playerMaterials[0] = _material;
        _renderer.materials = _playerMaterials;
        //Debug.Log("Color Changed to " + materialSO.Color);
    }
    private void ChangeDirectionToChosenColor()
    {
        Block _sameColorBlock = _blockColorSearch.FindSameColorBlock(_playerVisual, _playerRadar);
        if (_sameColorBlock != null)
        {
            Vector3 _blockPosition = _sameColorBlock.transform.position;
            Vector3 _newForwardDirection = _blockPosition - transform.position;
            transform.forward = _newForwardDirection.normalized;
        }
    }
    private void MoveToNewBlock(Transform pointTransform)
    {
        OnLeavingBlock?.Invoke(this, new OnLeavingBlockEventArgs
        {
            leftBlockTransform = transform
        });
        float _moveDistance = _moveSpeed * Time.deltaTime;
        Vector3 _blockPosition = pointTransform.position;

        Vector3 _moveDirection = _blockPosition - transform.position;
        _isMoving = _moveDirection != Vector3.zero;

        transform.position = Vector3.MoveTowards(transform.position, _blockPosition, _moveDistance);
    }
}
