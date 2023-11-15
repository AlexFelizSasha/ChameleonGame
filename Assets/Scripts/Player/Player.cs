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
    [SerializeField] private GameConstantsSO _gameConstantsSO;

    private SearchingBlockColor _blockColorSearch;
    private Transform _pointToMove;
    private Vector3 _startGamePosition;
    private float _moveSpeed;
    public bool _isMoving = false;

    private void Awake()
    {
        //_moveSpeed = ConstantsKeeper.PLAYER_MOVE_SPEED;
        _moveSpeed = _gameConstantsSO.playerMoveSpeed;
        _blockColorSearch = _playerRadar.gameObject.GetComponent<SearchingBlockColor>();
        _pointToMove = null;
    }
    private void Start()
    {
        _startGamePosition = new Vector3(ConstantsKeeper.START_POSITION_X, ConstantsKeeper.START_POSITION_Y, ConstantsKeeper.START_POSITION_Z);
        ColorButtonsManagerUI.OnColorChangedWithButton += ColorButtonsManager_OnColorChangedWithButton;
        Block.OnKillPlayer += Block_OnKillPlayer;
    }


    private void Update()
    {
        if (_pointToMove != null)
        {
            MoveToNewBlock(_pointToMove);
        }
    }
    private void Block_OnKillPlayer(object sender, EventArgs e)
    {
        Debug.Log("Player killed!");
        transform.position = _startGamePosition;
        _pointToMove = transform;
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
