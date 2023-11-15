using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringStopPoint : MonoBehaviour
{
    public static event EventHandler OnWateringStopPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<WateringMachine>())
        {
            OnWateringStopPoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
