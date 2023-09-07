using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootVisual : MonoBehaviour
{
    public event EventHandler OnLootTouched;
    public event EventHandler OnLootPicked;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.GetComponent<PlayerVisual>())
        {
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
