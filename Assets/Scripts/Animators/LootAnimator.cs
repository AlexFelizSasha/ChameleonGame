using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAnimator : MonoBehaviour
{
    [SerializeField] private Loot _loot;
    [SerializeField] private LootVisual _lootVisual;
    [SerializeField] private GameObject _crystal;
    [SerializeField] private GameObject _drop;
    [SerializeField] private GameObject _snow;
    private Animator _lootAnimator;

    private const string LOOT_CREATION = "LootCreation";
    private const string LOOT_MEDIUM = "LootMedium";
    private const string LOOT_MAXIMUM = "LootMaximum";

    private void OnEnable()
    {
        _crystal.SetActive(true);
        _drop.SetActive(false);
        _snow.SetActive(false);
    }

    private void Start()
    {
        _lootAnimator = GetComponent<Animator>();
        _loot.OnLootDropped += Loot_OnLootDropped;
        _loot.OnLootFalls += _loot_OnLootFalls;
        _loot.OnLootCreated += _loot_OnLootCreated;
        _loot.OnLootMediumSize += _loot_OnLootMediumSize;
        _loot.OnLootMaximumSize += _loot_OnLootMaximumSize;
    }


    private void Update()
    {
        //if (_lootAnimator != null)
        //{
        //if (_loot.lootState == Loot.LootState.MinimumSize)
        //    _lootAnimator.SetBool(LOOT_CREATION, true);
        //else _lootAnimator.SetBool(LOOT_CREATION, false);

        //if (_loot.lootState == Loot.LootState.MediumSize)
        //    _lootAnimator.SetBool(LOOT_MEDIUM, true);
        //else _lootAnimator.SetBool(LOOT_MEDIUM, false);

        //if (_loot.lootState == Loot.LootState.MaximumSize)
        //    _lootAnimator.SetBool(LOOT_MAXIMUM, true);
        //else _lootAnimator.SetBool(LOOT_MAXIMUM, false);
        //}
    }
    private void _loot_OnLootCreated(object sender, System.EventArgs e)
    {
        if (_lootAnimator != null)
            _lootAnimator.SetBool(LOOT_CREATION, true);
    }
    private void _loot_OnLootMediumSize(object sender, System.EventArgs e)
    {
        if (_lootAnimator != null)
        {
            _lootAnimator.SetBool(LOOT_CREATION, false);
            _lootAnimator.SetBool(LOOT_MEDIUM, true);
        }
    }
    private void _loot_OnLootMaximumSize(object sender, System.EventArgs e)
    {
        if (_lootAnimator != null)
        {
            _lootAnimator.SetBool(LOOT_MEDIUM, false);
            _lootAnimator.SetBool(LOOT_MAXIMUM, true);
        }
    }
    private void _loot_OnLootFalls(object sender, System.EventArgs e)
    {
        _lootAnimator.SetBool(LOOT_MAXIMUM, false);
        _crystal.SetActive(false);
        _drop.SetActive(false);
        _snow.SetActive(true);
    }
    private void Loot_OnLootDropped(object sender, System.EventArgs e)
    {
        _lootAnimator.SetBool(LOOT_MAXIMUM, false);
        _crystal.SetActive(false);
        _drop.SetActive(true);
        _snow.SetActive(false);
    }
}
