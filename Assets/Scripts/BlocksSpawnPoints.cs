using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class BlocksSpawnPoints
{
    public static int startPositionX = 0;
    public static int startPositionY = -10;
    public static int startPositionZ = 0;
    private static int[,] SpawnPointsArraySquare(int squareSide, int blockSideSize)
    {
        int _pointsAmount = squareSide * squareSide;
        int[,] _pointsArray = new int[3, _pointsAmount];
        int _positionX = startPositionX, _positionY = startPositionY, _positionZ = startPositionZ;
        for (int i = 0; i < _pointsAmount; i++)
        {
            if (i % squareSide == 0 && i != 0)
            {
                _positionX += blockSideSize;
                _positionZ -= blockSideSize * (squareSide - 1);
            }
            else
            {
                if (i != 0)
                {
                    _positionZ += blockSideSize;
                }
            }
            _pointsArray[0, i] = _positionX;
            _pointsArray[1, i] = _positionY;
            _pointsArray[2, i] = _positionZ;
        }

        return _pointsArray;
    }
    public static int[,] GetPointsArraySquare(int squareSide, int blockSideSize)
    {
        return SpawnPointsArraySquare(squareSide, blockSideSize);
    }

}
