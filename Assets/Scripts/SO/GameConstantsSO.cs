using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConstantsSO : ScriptableObject
{
    public int lifeAmount = 5;
    public float killPlayerTime = 10f;
    public float startPositionX = -5f;
    public float startPositionY = 1f;
    public float startPositionZ = 0f;

    public int cloudsPositionY = 1;

    public float dropDelayTime = 20f;

    public int treeChangeStateTime = 60;
    public int deadTreesAmount = 200;
    public int gardenTreesAmount = 120;
    public float treePositionY = -10.5f;

    public int fullWaterBarrelScore = 100;

    public float waterMachineMoveSpeed = 3f;

    public int baggagePositionY = -10;
    public int baggageUnloadScore = 10;
    public float baggageMoveSpeed = 6f;
    public float baggageUnloadTime = 5f;

    public float playerMoveSpeed = 10f;

    public int lootPoolSize = 10;
    public int lootAmount = 8;
    public int lootScoreMinimal = 10;
}
