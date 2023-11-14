using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public event EventHandler OnFruitsButton;

    private float _livingTime;
    private float _changeStateTime;
    private List<GameObject> _gardenTreesList;
    private TreeHandler _treeHandler;

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
        gardenState = GardenState.LeavesTrees;
        _livingTime = 0;
        _gardenTreesList = TreeCreator.instance.GetGardenTreeList();
        _changeStateTime = ConstantsKeeper.TREE_CHANGE_STATE_TIME;
    }
    private void Start()
    {

        Debug.Log("Garden");
        StartCoroutine(ChangeTreeVisual(4));
        WateringStopPoint.OnWateringStopPoint += WateringStopPoint_OnWateringStopPoint;
    }
    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (gardenState)
        {
            case GardenState.DeadTrees:
                break;
            case GardenState.BoldTrees:
                //if (_livingTime > _changeStateTime)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(1));
                //    gardenState = GardenState.DeadTrees;
                //}
                //if (_livingTime < 0)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(3));
                //    gardenState = GardenState.BranchesTrees;
                //}
                HandleTreesState(1, GardenState.DeadTrees, 3, GardenState.BranchesTrees);
                break;
            case GardenState.BranchesTrees:
                //if (_livingTime > _changeStateTime)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(2));
                //    gardenState = GardenState.BoldTrees;
                //}
                //if (_livingTime < 0)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(4));
                //    gardenState = GardenState.LeavesTrees;
                //}
                HandleTreesState(2, GardenState.BoldTrees, 4, GardenState.LeavesTrees);
                break;
            case GardenState.LeavesTrees:
                //if (_livingTime > _changeStateTime)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(3));
                //    gardenState = GardenState.BranchesTrees;
                //}
                //if (_livingTime < 0)
                //{
                //    _livingTime = 0;
                //    StartCoroutine(ChangeTreeVisual(5));
                //    OnFruitsButton?.Invoke(this, EventArgs.Empty);
                //    Debug.Log("CollectButton");
                //    gardenState = GardenState.FruitsTrees;
                //}
                HandleTreesState(3, GardenState.BranchesTrees, 5, GardenState.FruitsTrees);
                break;
            case GardenState.FruitsTrees:
                if (_livingTime == Time.deltaTime)
                {
                    OnFruitsButton?.Invoke(this, EventArgs.Empty);
                    Debug.Log("CollectButton");
                }
                if (_livingTime > _changeStateTime)
                {
                    _livingTime = 0;
                    StartCoroutine(ChangeTreeVisual(4));
                    gardenState = GardenState.LeavesTrees;
                }
                break;
        }
    }
    private void WateringStopPoint_OnWateringStopPoint(object sender, System.EventArgs e)
    {
        //StartCoroutine(ChangeTreeState());
        _livingTime -= _changeStateTime;
    }
    //private IEnumerator ChangeTreeState()
    //{
    //    for (int i = 0; i < _gardenTreesList.Count; i++)
    //    {
    //        _gardenTreesList[i].GetComponent<TreeHandler>().livingTime -= _changeStateTime;
    //        if (i % 3 == 0)
    //            yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //}
    private IEnumerator ChangeTreeVisual(int stateNumber)
    {
        for (int i = 0; i < _gardenTreesList.Count; i++)
        {
            _gardenTreesList[i].GetComponent<TreeHandler>().ChangeTreeState(stateNumber);
            if (i % 3 == 0)
                yield return new WaitForSeconds(Time.deltaTime);
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
    
}
