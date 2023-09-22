using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockAnimator : MonoBehaviour
{
    private Animator _blockAnimator;
    private const string IS_CREATED = "IsCreated";
    private const string IS_REPLACED = "IsReplaced";
    private const string IS_IDLE = "IsIdle";

    [SerializeField] private Block _block;

    private void Awake()
    {
        _blockAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_blockAnimator != null)
        {
            HandleCreatedAnimation();
            //HandleIdleAnimation();
            HandleReplacedAnimation();
        }

    }
    private void HandleCreatedAnimation()
    {
        if (_block.isCreated)
            _blockAnimator.SetBool(IS_CREATED, true);
        else
            _blockAnimator.SetBool(IS_CREATED, false);
    }
    private void HandleReplacedAnimation()
    {
        if (_block.isReplaced)
            _blockAnimator.SetBool(IS_REPLACED, true);
        else
            _blockAnimator.SetBool(IS_REPLACED, false);
    }
    private void HandleIdleAnimation()
    {
        if (_block.isIdle)
            _blockAnimator.SetBool(IS_IDLE, true);
        else
            _blockAnimator.SetBool(IS_IDLE, false);
    }
}
