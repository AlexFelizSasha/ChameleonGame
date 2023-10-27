using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserAnimator : MonoBehaviour
{
    public event EventHandler OnOpenGeyser;
    private Animator _geyserAnimator;
    private GeyserCollider _geyserCollider;
    private const string OPEN_GEYSER = "OpenGeyser";

    [SerializeField] private List<MaterialSO> _geyserMaterialSOlist;
    [SerializeField] private GameObject _geyserHole;

    private float _animatorTimer;
    private float _dropDelayTime;

    public enum GeyserAnimatorState
    {
        Open,
        Blocked,
        Idle
    }
    public GeyserAnimatorState geyserAnimatorState;

    public void Awake()
    {
        _geyserAnimator = GetComponent<Animator>();
        _geyserCollider = GetComponent<GeyserCollider>();
    }
    private void Start()
    {
        Block.OnBlockDestroyed += Block_OnBlockDestroyed;
        _geyserCollider.OnDropOnGeyser += _geyserCollider_OnDropOnGeyser;

        _animatorTimer = 0;
        _dropDelayTime = ConstantsKeeper.DROP_DELAY_TIME;
        geyserAnimatorState = GeyserAnimatorState.Open;
        _geyserAnimator.SetBool(OPEN_GEYSER, true);
    }


    private void Update()
    {
        switch (geyserAnimatorState)
        {
            case GeyserAnimatorState.Open:
                _animatorTimer += Time.deltaTime;
                if (_animatorTimer > Time.deltaTime)
                {
                    _geyserAnimator.SetBool(OPEN_GEYSER, false);
                    _animatorTimer = 0;
                    geyserAnimatorState = GeyserAnimatorState.Idle;
                }
                break;
            case GeyserAnimatorState.Blocked:
                _animatorTimer += Time.deltaTime;
                if (_animatorTimer > 0)
                {
                    ChangeGeyserColor(_geyserMaterialSOlist[0], _geyserHole);
                    OpenGeyser();
                }
                break;
            case GeyserAnimatorState.Idle:
                break;
        }
    }
    private void _geyserCollider_OnDropOnGeyser(object sender, EventArgs e)
    {
        _animatorTimer = -_dropDelayTime;
        geyserAnimatorState = GeyserAnimatorState.Blocked;
        ChangeGeyserColor(_geyserMaterialSOlist[1], _geyserHole);
        Debug.Log("Geyser blocked");
    }
    private void Block_OnBlockDestroyed(object sender, Block.OnBlockDestroyedEventArgs e)
    {
        if (geyserAnimatorState == GeyserAnimatorState.Blocked)
            return;
        if (ComparisonXZPositions.EqualXZPositions(transform.position, e.blockPosition))
        {
            OpenGeyser();
        }
    }
    private void OpenGeyser()
    {
        _geyserAnimator.SetBool(OPEN_GEYSER, true);
        OnOpenGeyser?.Invoke(this, EventArgs.Empty);
        geyserAnimatorState = GeyserAnimatorState.Open;
    }
    private void ChangeGeyserColor(MaterialSO materialSO, GameObject objectVisual)
    {
        Material _material = materialSO.Material;
        var _renderer = objectVisual.gameObject.GetComponent<MeshRenderer>();
        var _blockMaterials = _renderer.materials;
        _blockMaterials[0] = _material;
        _renderer.materials = _blockMaterials;
    }
}
