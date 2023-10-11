using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject _treeBold;
    [SerializeField] private GameObject _treeBranches;
    [SerializeField] private GameObject _treeFull;

    public enum TreeState
    {
        Bold,
        Branches,
        Full
    }
    public TreeState treeState;

    private void Awake()
    {
        treeState = TreeState.Full;
    }
    private void Update()
    {
        switch (treeState)
        {
            case TreeState.Bold:
                ActiveBoldTree();
                break;
            case TreeState.Branches:
                ActiveBranchTree();
                break;
            case TreeState.Full:
                ActiveFullTree();
                break;
        }
    }
    private void ActiveBoldTree()
    {
        _treeBold.SetActive(true);
        _treeBranches.SetActive(false);
        _treeFull.SetActive(false);
    }
    private void ActiveBranchTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(true);
        _treeFull.SetActive(false);
    }
    private void ActiveFullTree()
    {
        _treeBold.SetActive(false);
        _treeBranches.SetActive(false);
        _treeFull.SetActive(true);
    }
}
