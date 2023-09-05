using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootVisual : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Player>())
            Debug.Log("OnCollider!!!!");
    }
}
