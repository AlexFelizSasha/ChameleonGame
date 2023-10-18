using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    [SerializeField] private GameObject _treeBold;
    [SerializeField] private GameObject _treeBranches;
    [SerializeField] private GameObject _treeFull;
    [SerializeField] private Transform _wateringStopPointTransform;

    private float _livingTime;
    private float _changeStateTime = 60;
    private float _wateringAddTime = 60;

    public enum TreeState
    {
        Dead,
        Bold,
        Branches,
        Full
    }
    public TreeState treeState;

    private void Awake()
    {
        treeState = TreeState.Full;
    }
    private void Start()
    {
        WateringStopPoint.OnWateringStopPoint += WateringStopPoint_OnWateringStopPoint;
    }

    private void Update()
    {
        _livingTime += Time.deltaTime;
        switch (treeState)
        {
            case TreeState.Dead:
                break;
            case TreeState.Bold:
                ActivateBoldTree();
                ChangeState(TreeState.Dead, TreeState.Branches);
                break;
            case TreeState.Branches:
                ActivateBranchTree();
                ChangeState(TreeState.Bold, TreeState.Full);
                break;
            case TreeState.Full:
                ActivateFullTree();
                ChangeState(TreeState.Branches, TreeState.Full);
                break;
        }
        
    }
    private void WateringStopPoint_OnWateringStopPoint(object sender, System.EventArgs e)
    {
        _livingTime -= _wateringAddTime;
    }
    private void ChangeState(TreeState previousState, TreeState nextState)
    {
        if (_livingTime > _changeStateTime)
        {
            treeState = previousState;
            _livingTime = 0;
        }
        if(_livingTime < 0)
        {
            treeState = nextState;
        }
    }
    private void ActivateBoldTree()
    {
        _treeBold.SetActive(true);
        _treeBranches.SetActive(false);
        _treeFull.SetActive(false);
    }
    private void ActivateBranchTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(true);
        _treeFull.SetActive(false);
    }
    private void ActivateFullTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(false);
        _treeFull.SetActive(true);
    }
}
