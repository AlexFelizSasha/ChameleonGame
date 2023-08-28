using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingBlockColor : MonoBehaviour
{
    [SerializeField] private Transform _blocksRadar;

    private Vector3 _radarPosition;
    private Vector3 _playerNextPosition;
    //private List<GameObject> _blocksAround = new List<GameObject>(2);

    private void Start()
    {
        _radarPosition = _blocksRadar.position;
    }
    //private void Update()
    //{
    //    ShowBlocksAround();
    //}
    public Vector3 GetPositionToMove()
    {
        return _playerNextPosition;
    }
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
        float _distanceToBlock = 2.5f;
        RaycastHit _hit;
        Ray _forwardDirectionRay = new Ray(_radarPosition, searchDirection);
        if (Physics.Raycast(_forwardDirectionRay, out _hit, _distanceToBlock))
        {
            if (_hit.collider.gameObject.GetComponent<MeshRenderer>())
            {
                _block = _hit.collider.gameObject;
            }
        }

        return _block;
    }
    public List<GameObject> GetCloseBlocksList()
    {
        return FindBlocksAround();
    }
    //private void ShowBlocksAround()
    //{
    //    foreach (GameObject block in FindBlocksAround())
    //        if (block != null)
    //            Debug.Log(block.gameObject.GetComponent<MeshRenderer>().materials[0]);
    //}
}
