using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public static event EventHandler<OnLootScoreAddEventArgs> OnLootScoreAdd;
    public class OnLootScoreAddEventArgs : EventArgs
    {
        public int lootScore;
    }

    [SerializeField] private GameObject _lootVisual;

    [SerializeField] private List<MaterialSO> _materialSOList;
    
    private enum State
    {
        Creation,
        MinimumSize,
        MediumSize,
        MaximumSize,
        OverSize
    }

    private State _state;
    private float _fallingSpeed = 6.0f;
    private bool _isTouched = false;
    private bool _isPicked = false;
    private int _downPointY = -25;   //how low loot falls down
    private int _lootScore;
    private float _livingTime;
    private float _minimumSizeTime = 5f;
    private float _mediumSizeTime = 15f;
    private float _maximumSizeTime = 25f;
    private float _overSizeTime = 30f;

    private void Start()
    {
        _state = State.Creation;
        _lootScore = 0;
        _livingTime = 0;
        _lootVisual.GetComponent<LootVisual>().OnLootTouched += Loot_OnLootTouched;
        _lootVisual.GetComponent<LootVisual>().OnLootPicked += Loot_OnLootPicked;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        if (_isTouched)
        {
            DropDownLoot();
        }
        if (_isPicked)
        {
            DestroyLoot();
        }

        switch (_state)
        {
            case State.Creation:
                _lootScore = 1;
                if (_livingTime > _minimumSizeTime)
                    _state = State.MinimumSize;
                break;
            case State.MinimumSize:
                _lootScore = 2;
                if (_livingTime > _mediumSizeTime)
                    _state = State.MediumSize;
                break;
            case State.MediumSize:
                _lootScore = 3;
                if (_livingTime > _maximumSizeTime)
                    _state = State.MaximumSize;
                break;
            case State.MaximumSize:
                _lootScore = 5;
                if (_livingTime > _overSizeTime)
                    _state = State.OverSize;
                break;
            case State.OverSize:
                _lootScore = 15;
                _isTouched = true;
                break;
        }
    }
    private void Loot_OnLootPicked(object sender, System.EventArgs e)
    {
        _isPicked = true;
        OnLootScoreAdd?.Invoke(this, new OnLootScoreAddEventArgs
        {
            lootScore = _lootScore
        });
    }
    private void Loot_OnLootTouched(object sender, System.EventArgs e)
    {
        _isTouched = true;

        Debug.Log("Loot Touched!");
    }
    private void DropDownLoot()
    {
        float _moveDistance = _fallingSpeed * Time.deltaTime;

        Vector3 _fallingDirection = new Vector3(transform.position.x, _downPointY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _fallingDirection, _moveDistance);
    }
    private void DestroyLoot()
    {
        _isTouched = false;
        _isPicked = false;        
        gameObject.SetActive(false);
    }
}
