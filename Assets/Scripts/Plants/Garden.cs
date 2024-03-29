using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] private CollectButtonUI _collectButtonUI;

    public event Action OnFruitsButton;
    public static event Action OnTreesDead;

    private GameConstantsSO _gameConstantsSO;
    private float _livingTime;
    private float _changeStateTime;
    private List<GameObject> _gardenTreesList;

    public enum GardenState
    {
        DeadTrees,
        BoldTrees,
        BranchesTrees,
        LeavesTrees,
        FruitsTrees
    }
    public GardenState gardenState;
    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        gardenState = GardenState.LeavesTrees;
        _livingTime = 0;
        _gardenTreesList = TreeCreator.instance.GetGardenTreeList();
        _changeStateTime = _gameConstantsSO.treeChangeStateTime;
    }
    private void Start()
    {
        StartCoroutine(ChangeTreeVisual(4));
        WateringStopPoint.OnWateringStopPoint += WateringStopPoint_OnWateringStopPoint;
        _collectButtonUI.OnCollectButtonClicked += CollectButtonUI_OnCollectButtonClicked;
    }
    private void OnDisable()
    {
        WateringStopPoint.OnWateringStopPoint -= WateringStopPoint_OnWateringStopPoint;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (gardenState)
        {
            case GardenState.DeadTrees:
                break;
            case GardenState.BoldTrees:
                HandleTreesState(1, GardenState.DeadTrees, 3, GardenState.BranchesTrees);
                break;
            case GardenState.BranchesTrees:
                HandleTreesState(2, GardenState.BoldTrees, 4, GardenState.LeavesTrees);
                break;
            case GardenState.LeavesTrees:
                HandleTreesState(3, GardenState.BranchesTrees, 5, GardenState.FruitsTrees);
                break;
            case GardenState.FruitsTrees:
                HandleFruitsState();
                break;
        }
    }
    private void WateringStopPoint_OnWateringStopPoint()
    {
        _livingTime -= _changeStateTime;
    }
    private void CollectButtonUI_OnCollectButtonClicked(object sender, EventArgs e)
    {
        _livingTime += _changeStateTime;
    }
    private IEnumerator ChangeTreeVisual(int stateNumber)
    {
        for (int i = 0; i < _gardenTreesList.Count; i++)
        {
            _gardenTreesList[i].GetComponent<TreeHandler>().ChangeTreeState(stateNumber);
            if (i % 3 == 0)
                yield return new WaitForSeconds(Time.deltaTime);
            if (i ==  _gardenTreesList.Count -2 && gardenState == GardenState.FruitsTrees)
            {
                OnFruitsButton?.Invoke();
            }
            if (i == _gardenTreesList.Count - 1 && gardenState == GardenState.DeadTrees)
            {
                OnTreesDead?.Invoke();
            }
        }
    }
    private void HandleTreesState(int previousTreeStateNumber, GardenState previousGardenState,
                                    int nextStateNumber, GardenState nextGardenState)
    {
        if (_livingTime > _changeStateTime)
        {
            _livingTime = 0;
            StartCoroutine(ChangeTreeVisual(previousTreeStateNumber));
            gardenState = previousGardenState;
        }
        if (_livingTime < 0)
        {
            _livingTime = 0;
            StartCoroutine(ChangeTreeVisual(nextStateNumber));
            gardenState = nextGardenState;
        }
    }
    private void HandleFruitsState()
    {
        if (_livingTime > _changeStateTime)
        {
            _livingTime = 0;
            StartCoroutine(ChangeTreeVisual(4));
            gardenState = GardenState.LeavesTrees;
        }
    }
}
