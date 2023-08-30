using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingBlockColor : MonoBehaviour
{
    private List<GameObject> FindBlocksAround()
    {
        List<GameObject> _blocksAround = new List<GameObject>();
        _blocksAround.Add(SearchBlock(Vector3.forward));
        _blocksAround.Add(SearchBlock(Vector3.left));
        _blocksAround.Add(SearchBlock(Vector3.right));
        _blocksAround.Add(SearchBlock(Vector3.back));

        return _blocksAround;
    }
    private GameObject SearchBlock(Vector3 searchDirection)
    {
        GameObject _block = null;
        float _distanceToBlock = 5f;
        RaycastHit _hit;
        Ray _forwardDirectionRay = new Ray(transform.position, searchDirection);
        if (Physics.Raycast(_forwardDirectionRay, out _hit, _distanceToBlock))
        {
            if (_hit.collider.GetComponent<BlockVisual>())
            {
                _block = _hit.collider.gameObject.GetComponent<BlockVisual>().GetParentBlock();
            }
        }

        return _block;
    }
    public List<GameObject> GetCloseBlocksList()
    {
        return FindBlocksAround();
    }
}
