using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVisual : MonoBehaviour
{
    [SerializeField] private GameObject _parentBlock;

    public GameObject GetParentBlock()
    {
        return _parentBlock;
    }
}
