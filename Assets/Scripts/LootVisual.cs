using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootVisual : MonoBehaviour
{
    public static event EventHandler OnLootTouchedOnPosition;
    

    public event EventHandler OnLootPicked;
    public event EventHandler OnLootTouched;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.GetComponent<PlayerVisual>())
        {
            OnLootTouchedOnPosition?.Invoke(this, EventArgs.Empty);
            OnLootTouched?.Invoke(this, EventArgs.Empty);

            Debug.Log("OnCollider!!!!");
        }        
        if (other.gameObject.GetComponent<BaggageVisual>())
        {
            OnLootPicked?.Invoke(this, EventArgs.Empty);            

            Debug.Log("InBaggage!!!!");
        }
    }
}
