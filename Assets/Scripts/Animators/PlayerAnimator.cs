using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _playerAnimator;
    private const string IS_MOVING = "IsMoving";

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_playerAnimator != null)
        {
            if (_player._isMoving)
                _playerAnimator.SetBool(IS_MOVING, true);
            else _playerAnimator.SetBool(IS_MOVING, false);
        }
    }
}
