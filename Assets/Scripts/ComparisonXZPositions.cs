using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparisonXZPositions 
{
    public static bool EqualXZPositions(Vector3 objectPosition, Vector3 blockPosition)
    {
        Vector2 _objectXZposition = new Vector2(objectPosition.x, objectPosition.z);
        Vector2 _blockXZposition = new Vector2(blockPosition.x, blockPosition.z);

        return _blockXZposition == _objectXZposition;
    }
}
