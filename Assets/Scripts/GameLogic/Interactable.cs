using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string annotation;
    public UnityEvent onInteract;

    public virtual void Interact()
    {
        onInteract?.Invoke();
    }
}
