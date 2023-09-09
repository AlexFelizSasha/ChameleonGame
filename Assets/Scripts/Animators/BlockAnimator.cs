using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimator : MonoBehaviour
{
    private Animator _blockAnimator;
    private const string IS_CREATED = "IsCreated";
    private const string IS_REPLACED = "IsReplaced";

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
            HandleReplacedAnimation();
        }

    }
    private void HandleCreatedAnimation()
    {
        if (_block._isCreated)
            _blockAnimator.SetBool(IS_CREATED, true);
        else
            _blockAnimator.SetBool(IS_CREATED, false);
    }
    private void HandleReplacedAnimation()
    {
        if (_block._isReplaced)
            _blockAnimator.SetBool(IS_REPLACED, true);
        else
            _blockAnimator.SetBool(IS_REPLACED, false);
    }
}
