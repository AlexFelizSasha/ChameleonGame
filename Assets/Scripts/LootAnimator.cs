using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAnimator : MonoBehaviour
{
    [SerializeField] private Loot _loot;
    [SerializeField] private LootVisual _lootVisual;
    [SerializeField] private GameObject _crystal;
    [SerializeField] private GameObject _drop;
    private Animator _lootAnimator;

    private const string LOOT_CREATION = "LootCreation";
    private const string LOOT_MEDIUM = "LootMedium";
    private const string LOOT_MAXIMUM = "LootMaximum";
    private const string LOOT_FALLS_DOWN = "LootFallsDown";

    private void OnEnable()
    {
        _crystal.SetActive(true);
        _drop.SetActive(false);
    }

    private void Start()
    {
        _lootAnimator = GetComponent<Animator>();
        _loot.OnLootDropped += Loot_OnLootDropped;
    }

    private void Loot_OnLootDropped(object sender, System.EventArgs e)
    {
        _crystal.SetActive(false);
        _drop.SetActive(true);
    }

    private void Update()
    {
        if (_lootAnimator != null)
        {
            if(_loot.lootState == Loot.State.MinimumSize)
                _lootAnimator.SetBool(LOOT_CREATION, true);
            else _lootAnimator.SetBool(LOOT_CREATION, false);

            if(_loot.lootState == Loot.State.MediumSize)
                _lootAnimator.SetBool(LOOT_MEDIUM, true);
            else _lootAnimator.SetBool(LOOT_MEDIUM, false);

            if(_loot.lootState == Loot.State.MaximumSize)
                _lootAnimator.SetBool(LOOT_MAXIMUM, true);
            else _lootAnimator.SetBool(LOOT_MAXIMUM, false);

            if(_loot.lootState == Loot.State.Dropping)
                _lootAnimator.SetBool(LOOT_FALLS_DOWN, true);
            else _lootAnimator.SetBool(LOOT_FALLS_DOWN, false);
        }
    }
}
