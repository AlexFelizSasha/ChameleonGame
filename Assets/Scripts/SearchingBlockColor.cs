using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingBlockColor : MonoBehaviour
{
    public List<GameObject> GetCloseBlocksList(Vector3 _currentPosition)
    {
        return FindBlocksAround(_currentPosition);
    }
    public Block FindSameColorBlock(Transform visualTransform)
    {
        return FindSameColorBlock(visualTransform, visualTransform);
    }
    public Block FindSameColorDiagonalBlock(Transform visualTransform)
    {
        var _playerMaterials = visualTransform.gameObject.GetComponent<MeshRenderer>().materials;
        List<GameObject> _closeBlocks = FindBlocksAroundDiagonal(visualTransform.position);
        return FindBlockInList(_closeBlocks, visualTransform);
        
    }
    public Block FindSameColorBlock(Transform visualTransform, Transform radarTransform)
    {
        
        var _playerMaterials = visualTransform.gameObject.GetComponent<MeshRenderer>().materials;
        List<GameObject> _closeBlocks = FindBlocksAround(radarTransform.position);
        return FindBlockInList(_closeBlocks, visualTransform);
    }
    private Block FindBlockInList(List<GameObject> blockList, Transform visualTransform)
    {
        Block _sameColorBlock = null;
        var _playerMaterials = visualTransform.gameObject.GetComponent<MeshRenderer>().materials;
        foreach (var block in blockList)
        {
            if (block != null)
            {
                var _blockVisual = block.GetComponent<Block>().GetBlockVisual();
                var _blockMaterials = _blockVisual.GetComponent<MeshRenderer>().materials;
                if (_blockMaterials[0].name == _playerMaterials[0].name)
                {
                    _sameColorBlock = block.GetComponent<Block>();
                }
            }
        }
        return _sameColorBlock;
    }
    private List<GameObject> FindBlocksAroundDiagonal(Vector3 _currentPosition)
    {
        List<GameObject> _blocksAround = new List<GameObject>();
        //float _distanceToBlock = BlocksCreator.BlockSideSize / Mathf.Cos(45); 
        Vector3 _rightTop = new Vector3(transform.position.x + 1,
                                    transform.position.y,
                                    transform.position.z + 1);
        Vector3 _rightBottom = new Vector3(transform.position.x - 1,
                                    transform.position.y,
                                    transform.position.z + 1);
        Vector3 _leftBottom = new Vector3(transform.position.x - 1,
                                    transform.position.y,
                                    transform.position.z - 1);
        Vector3 _leftTop = new Vector3(transform.position.x + 1,
                                    transform.position.y,
                                    transform.position.z - 1);
        Vector3 _blockToBlockVector = _rightTop - _currentPosition;
        Vector3 _blockToBlockVectorNormalized = _blockToBlockVector.normalized;
        float _distanceToBlock = _blockToBlockVectorNormalized.magnitude;
        _blocksAround.Add(SearchForNeighborBlock(_rightTop, _currentPosition, _distanceToBlock));
        Debug.Log(SearchForNeighborBlock(_rightTop, _currentPosition, _distanceToBlock));

        _blocksAround.Add(SearchForNeighborBlock(_rightBottom, _currentPosition, _distanceToBlock));
        Debug.Log(SearchForNeighborBlock(_rightBottom, _currentPosition, _distanceToBlock));

        _blocksAround.Add(SearchForNeighborBlock(_leftBottom, _currentPosition, _distanceToBlock));
        Debug.Log(SearchForNeighborBlock(_rightBottom, _currentPosition, _distanceToBlock));

        _blocksAround.Add(SearchForNeighborBlock(_leftTop, _currentPosition, _distanceToBlock));
        Debug.Log(SearchForNeighborBlock(_leftTop, _currentPosition, _distanceToBlock));

        return _blocksAround;

    }    
    private List<GameObject> FindBlocksAround(Vector3 _currentPosition)
    {
        List<GameObject> _blocksAround = new List<GameObject>();
        float _distanceToBlock = BlocksCreator.BlockSideSize * 1.5f;
        _blocksAround.Add(SearchForNeighborBlock(Vector3.forward, _currentPosition, _distanceToBlock));
        _blocksAround.Add(SearchForNeighborBlock(Vector3.left, _currentPosition, _distanceToBlock));
        _blocksAround.Add(SearchForNeighborBlock(Vector3.right, _currentPosition, _distanceToBlock));
        _blocksAround.Add(SearchForNeighborBlock(Vector3.back, _currentPosition, _distanceToBlock));

        return _blocksAround;
    }
    private GameObject SearchForNeighborBlock(Vector3 searchDirection, Vector3 _currentPosition, float distanceToBlock)
    {
        GameObject _block = null;
        
        RaycastHit _hit;
        Ray _forwardDirectionRay = new Ray(_currentPosition, searchDirection);
        if (Physics.Raycast(_forwardDirectionRay, out _hit, distanceToBlock))
        {
            if (_hit.collider.GetComponent<BlockVisual>())
            {
                _block = _hit.collider.gameObject.GetComponent<BlockVisual>().GetParentBlock();
            }
        }

        return _block;
    }
}
