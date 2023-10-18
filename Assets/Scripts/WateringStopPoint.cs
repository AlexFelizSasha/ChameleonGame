using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringStopPoint : MonoBehaviour
{
    //public static WateringStopPoint instance { get; private set; }
    public static event EventHandler OnWateringStopPoint;

    //public void Awake()
    //{
    //    if (instance != null)
    //        Destroy(instance.gameObject);
    //    else instance = this;
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<WateringMachine>())
        {
            OnWateringStopPoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
