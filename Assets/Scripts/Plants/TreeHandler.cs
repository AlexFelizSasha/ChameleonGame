using System;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    [SerializeField] private GameObject _treeBold;
    [SerializeField] private GameObject _treeBranches;
    [SerializeField] private GameObject _treeBranchesLeaves;
    [SerializeField] private GameObject _treeBranchesLeavesFruits;

    //public float livingTime;

    //public enum TreeState
    //{
    //    Dead,
    //    Bold,
    //    Branches,
    //    BranchesLeaves,
    //    BranchesLeavesFruits
    //}
    //public TreeState treeState;

    private void Awake()
    {
        //treeState = TreeState.BranchesLeavesFruits;
        ActivateBoldTree();
    }

    //private void Update()
    //{
        //livingTime += Time.deltaTime;
        //switch (treeState)
        //{
        //    case TreeState.Dead:
        //        ActivateBoldTree();
        //        break;
        //    case TreeState.Bold:
        //        ActivateBoldTree();
        //        ChangeState(TreeState.Dead, TreeState.Branches);
        //        break;
        //    case TreeState.Branches:
        //        ActivateBranchTree();
        //        ChangeState(TreeState.Bold, TreeState.BranchesLeaves);
        //        break;
        //    case TreeState.BranchesLeaves:
        //        ActivateBranchesLeavesTree();
        //        ChangeState(TreeState.Branches, TreeState.BranchesLeavesFruits);
        //        break;
        //    case TreeState.BranchesLeavesFruits:
        //        ActivateBranchesLeavesFruitsTree();
        //        ChangeState(TreeState.BranchesLeaves, TreeState.BranchesLeavesFruits);
        //        break;
        //}
    //}
    //private void ChangeState(TreeState previousState, TreeState nextState)
    //{
    //    if (livingTime > _changeStateTime)
    //    {
    //        treeState = previousState;
    //        livingTime = 0;
    //    }
    //    if(livingTime < 0)
    //    {
    //        treeState = nextState;
    //        livingTime = 0;
    //    }
    //}
    public void ChangeTreeState(int stateNumber)
    {
        switch (stateNumber)
        {
            case 1:
                ActivateBoldTree();
                break;
            case 2:
                ActivateBoldTree();
                break;
            case 3:
                ActivateBranchTree();
                break;
            case 4:
                ActivateBranchesLeavesTree();
                break;
            case 5:
                ActivateBranchesLeavesFruitsTree();
                break;
        }
    }
    private void ActivateBoldTree()
    {
        _treeBold.SetActive(true);
        _treeBranches.SetActive(false);
        _treeBranchesLeaves.SetActive(false);
        _treeBranchesLeavesFruits.SetActive(false);
    }
    private void ActivateBranchTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(true);
        _treeBranchesLeaves.SetActive(false);
        _treeBranchesLeavesFruits.SetActive(false);
    }
    private void ActivateBranchesLeavesTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(false);
        _treeBranchesLeaves.SetActive(true);
        _treeBranchesLeavesFruits.SetActive(false);
    }
    private void ActivateBranchesLeavesFruitsTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(false);
        _treeBranchesLeaves.SetActive(false);
        _treeBranchesLeavesFruits.SetActive(true);
    }
}
