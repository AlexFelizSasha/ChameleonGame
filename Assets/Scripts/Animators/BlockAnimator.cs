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
    //private void Start()
    //{
    //    _block.OnBlockIdleForVisual += _block_OnBlockIdleForVisual;
    //}

    //private void _block_OnBlockIdleForVisual(object sender, System.EventArgs e)
    //{
    //    _blockAnimator.SetTrigger(IDLE_BLOCK);
    //}

    private void Update()
    {
        if (_blockAnimator != null)
        {
            HandleCreatedAnimation();
            HandleReplacedAnimation();
            HandleIdleAnimation();
        }
        //if (_block.blockState == Block.BlockState.FirstFlight)
        //{
        //    _blockAnimator.SetTrigger(FIRST_FLIGHT);
        //}
        //else _blockAnimator.ResetTrigger(FIRST_FLIGHT);


        //if (_block.blockState == Block.BlockState.Idle)
        //{
        //    _blockAnimator.SetTrigger(IDLE_BLOCK);
        //}
        //else _blockAnimator.ResetTrigger(IDLE_BLOCK);



    }

    private void HandleCreatedAnimation()
    {
        if (_block.isCreated)
            _blockAnimator.SetBool(IS_CREATED, true);
        else
            _blockAnimator.SetBool(IS_CREATED, false);
    }
    private void HandleIdleAnimation()
    {
        if (_block.isIdle)
            _blockAnimator.SetBool(IS_IDLE, true);
        else
            _blockAnimator.SetBool(IS_IDLE, false);
    }
    private void HandleReplacedAnimation()
    {
        if (_block.isReplaced)
            _blockAnimator.SetBool(IS_REPLACED, true);
        else
            _blockAnimator.SetBool(IS_REPLACED, false);
    }
}
