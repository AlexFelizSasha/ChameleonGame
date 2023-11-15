using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserCollider : MonoBehaviour
{
    public event EventHandler OnDropOnGeyser;
    public static event EventHandler<OnDropForBlockEventArgs> OnDropForBlock;
    public class OnDropForBlockEventArgs : EventArgs
    {
        public Vector3 position;
    }
    [SerializeField] Material _dropMaterial;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Drop>())
        {
            OnDropOnGeyser?.Invoke(this, EventArgs.Empty);
            OnDropForBlock?.Invoke(this, new OnDropForBlockEventArgs()
            {
                position = transform.position
            });
            
            Debug.Log("Drop on geyser" + transform.position);
        }
    }
}
