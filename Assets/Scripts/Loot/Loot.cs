using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{  
    [SerializeField] private LootVisual _lootVisual;
    [SerializeField] private List<MaterialSO> _materialSOList;
    [SerializeField] private GameConstantsSO _gameConstantsSO;

    public static event EventHandler<OnLootScoreAddEventArgs> OnLootScoreAdd;
    public class OnLootScoreAddEventArgs : EventArgs
    {
        public int lootScore;
    }
    public static event EventHandler<OnLootDroppedEventArgs> OnLootDestroyed;
    public class OnLootDroppedEventArgs : EventArgs
    {
        public Vector2 lootPosition;
    }

    public event EventHandler OnLootDropped;
    public event EventHandler OnLootFalls;
    public event EventHandler OnLootCreated;
    public event EventHandler OnLootMediumSize;
    public event EventHandler OnLootMaximumSize;


    public enum LootState
    {
        Creation,
        MinimumSize,
        MediumSize,
        MaximumSize,
        Dropping
    }

    public LootState lootState { get; private set; }
    private float _fallingSpeed;
    private float _snowFallingSpeed = 5f;
    private int _downPointY = -25;   //how low loot falls down
    private int _lootScore;
    private int _lootScoreMinimal;

    private float _livingTime;
    private float _minimumSizeTimeMin = 5f;
    private float _minimumSizeTimeMax = 7f;
    private float _minimumSizeTime;
    private float _mediumSizeTimeMin = 8f;
    private float _mediumSizeTimeMax = 16f;
    private float _mediumSizeTime;
    private float _maximumSizeTimeMin = 20f;
    private float _maximumSizeTimeMax = 36f;
    private float _maximumSizeTime;
    private float _overSizeTimeMin = 40f;
    private float _overSizeTimeMax = 56f;
    private float _overSizeTime;

    private void OnEnable()
    {
        lootState = LootState.Creation;
        _lootScoreMinimal = _gameConstantsSO.lootScoreMinimal;
    }

    private void Start()
    {
        CountStateTime();

        _lootScore = 0;
        _livingTime = 0;
        
        _lootVisual.OnLootTouched += Loot_OnLootTouched;
        _lootVisual.OnLootPicked += Loot_OnLootPicked;
        _lootVisual.OnLootOnTheGround += Loot_OnLootOnTheGround;
    }


    private void Update()
    {
        _livingTime += Time.deltaTime;

        switch (lootState)
        {
            case LootState.Creation:
                HandleCreationState();
                break;
            case LootState.MinimumSize:
                HandleMinimumSizeState();
                break;
            case LootState.MediumSize:
                _lootScore = 3;
                HandleMediumSizeState();
                break;
            case LootState.MaximumSize:
                HandleMaximumSizeState();
                break;
            case LootState.Dropping:
                HandleDroppingState();
                break;
        }
    }
    public int GetLootScore()
    {
        return _lootScore;
    }

    private void Loot_OnLootPicked(object sender, System.EventArgs e)
    {
        DestroyLoot();
        OnLootScoreAdd?.Invoke(this, new OnLootScoreAddEventArgs
        {
            lootScore = _lootScore,
        });
    }
    private void Loot_OnLootTouched(object sender, System.EventArgs e)
    {
        lootState = LootState.Dropping;
        OnLootDropped?.Invoke(this, EventArgs.Empty);
    }
    private void Loot_OnLootOnTheGround(object sender, EventArgs e)
    {
        DestroyLoot();
    }
    private void CountStateTime()
    {
        _minimumSizeTime = UnityEngine.Random.Range(_minimumSizeTimeMin, _minimumSizeTimeMax);
        _mediumSizeTime = UnityEngine.Random.Range(_mediumSizeTimeMin, _mediumSizeTimeMax);
        _maximumSizeTime = UnityEngine.Random.Range(_maximumSizeTimeMin, _maximumSizeTimeMax);
        _overSizeTime = UnityEngine.Random.Range(_overSizeTimeMin, _overSizeTimeMax);
    }
    private void HandleCreationState()
    {
        _lootScore = _lootScoreMinimal;
        if (_livingTime > _minimumSizeTime)
        {
            OnLootCreated?.Invoke(this, EventArgs.Empty);
            lootState = LootState.MinimumSize;
        }
    }
    private void HandleMinimumSizeState()
    {
        _lootScore = 2 * _lootScoreMinimal;
        if (_livingTime > _mediumSizeTime)
        {
            OnLootMediumSize?.Invoke(this, EventArgs.Empty);
            lootState = LootState.MediumSize;
        }
    }
    private void HandleMediumSizeState()
    {
        _lootScore = 3 * _lootScoreMinimal;
        if (_livingTime > _maximumSizeTime)
        {
            OnLootMaximumSize?.Invoke(this, EventArgs.Empty);
            lootState = LootState.MaximumSize;
        }
    }
    private void HandleMaximumSizeState()
    {
        _lootScore = 5 * _lootScoreMinimal;
        if (_livingTime > _overSizeTime)
        {
            _lootScore = 0;
            OnLootFalls?.Invoke(this, EventArgs.Empty);
            lootState = LootState.Dropping;
        }
    }
    private void HandleDroppingState()
    {
        _fallingSpeed = _snowFallingSpeed;
        DropDownLoot();
    }
    private void DropDownLoot()
    {
        float _moveDistance = _fallingSpeed * Time.deltaTime;

        Vector3 _fallingDirection = new Vector3(transform.position.x, _downPointY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _fallingDirection, _moveDistance);
    }
    private void DestroyLoot()
    {
        OnLootDestroyed?.Invoke(this, new OnLootDroppedEventArgs
        {
            lootPosition = transform.position
        });
        _fallingSpeed = 0;
        _livingTime = 0;
        
        gameObject.SetActive(false);
    }
}
