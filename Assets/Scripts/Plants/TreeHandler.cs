using System;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    [SerializeField] private GameObject _treeBold;
    [SerializeField] private GameObject _treeBranches;
    [SerializeField] private GameObject _treeBranchesLeaves;
    [SerializeField] private GameObject _treeBranchesLeavesFruits;

    private void Awake()
    {
        ActivateBoldTree();
    }

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
