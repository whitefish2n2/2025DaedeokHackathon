using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject rayOrigin;
    RaycastHit2D[] hit;

    public GameObject interactTextGameObject;
    public TextMeshProUGUI interactText;
    public Interactable currentInteractable;
    public GameObject currentInteractableGameObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentInteractable)
            {
                currentInteractable.Interact();
            }
        }
        int interactableCount = 0;
        var size = Physics2D.RaycastNonAlloc(
            rayOrigin.transform.position, 
            Vector2.right, 
            hit, 
            100f, 
            LayerMask.GetMask("Interactable"));
        if(hit.Length == 0){ currentInteractable = null; currentInteractableGameObject = null; }
        foreach (RaycastHit2D h in hit)
        {
            if (h.transform.gameObject == currentInteractableGameObject) return;
            if(h.transform.gameObject.TryGetComponent<Interactable>(out var i))
            {
                interactText.text = i.annotation;
            }
        }

        
    }
}
