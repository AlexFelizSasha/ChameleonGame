using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsKeeper : MonoBehaviour
{
    public static ConstantsKeeper instance {  get; private set; }

    private void Awake()
    {
        instance = this; 
    }
}
