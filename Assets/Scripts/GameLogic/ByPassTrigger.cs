using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ByPassTrigger : MonoBehaviour
{
    public bool triggered = false;
    public UnityEvent OnTriggered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(triggered) return;
        if (other.CompareTag("Player2D"))
        {
            Debug.Log("OnTriggerEnter2D Triggered In " + name);
            triggered = true;
            OnTriggered.Invoke();
        }
    }
}
