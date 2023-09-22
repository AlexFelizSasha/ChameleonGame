using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCreator : MonoBehaviour
{
    public static BlocksCreator Instance { get; private set; }
    public static int BlockSideSize {  get; private set; }
    public static int BlocksSquareSideSize { get; private set; }

    [SerializeField] private GameObject _blockPrefab;
    private List<int> _blocksSpawnPositionXList;
    private List<int> _blocksSpawnPositionYList;
    private List<int> _blocksSpawnPositionZList;
    private List<GameObject> _blocksOnMapList;
    private List<Vector3> _blocksPositionList;
    private void Awake()
    {
        Instance = this;
        _blocksSpawnPositionXList = new List<int>();
        _blocksSpawnPositionYList = new List<int>();
        _blocksSpawnPositionZList = new List<int>();
        _blocksOnMapList = new List<GameObject>();
        _blocksPositionList = new List<Vector3>();
        BlockSideSize = 5;
        BlocksSquareSideSize = 5;
    }
    private void Start()
    {
        SetSpawnCoordinates();
        PutBlocksSquareOnMap();
    }
    private List<int> SetPositionList(int coordinateNumber)
    {
        List<int> _positionValueList = new List<int>();
        int[,] _spawnPointsArray = BlocksSpawnPoints.GetPointsArraySquare(BlocksSquareSideSize, BlockSideSize);
        for (int i = 0; i < BlocksSquareSideSize * BlocksSquareSideSize; i++)
        {
            _positionValueList.Add(_spawnPointsArray[coordinateNumber, i]);
        }
        return _positionValueList;
    }
    private void SetSpawnCoordinates()
    {
        _blocksSpawnPositionXList = SetPositionList(0); //x
        _blocksSpawnPositionYList = SetPositionList(1); //y
        _blocksSpawnPositionZList = SetPositionList(2); //z
    }
    private void PutBlocksSquareOnMap()
    {
        int _blocksAmount = BlocksSquareSideSize * BlocksSquareSideSize;
        for (int i = 0; i < _blocksAmount; i++)
        {
            Vector3 _blockPosition = new Vector3(_blocksSpawnPositionXList[i],
                                                 _blocksSpawnPositionYList[i],
                                                 _blocksSpawnPositionZList[i]);
            _blocksPositionList.Add(_blockPosition);
            GameObject _block = Instantiate(_blockPrefab, _blockPosition, Quaternion.identity);
            _blocksOnMapList.Add(_block);
        }
    }
    public List<GameObject> GetBlocksOnMapList()
    {
        return _blocksOnMapList;
    }
    public List<Vector3> GetBlocksOnMapPositionList()
    {
        return _blocksPositionList;
    }
}
