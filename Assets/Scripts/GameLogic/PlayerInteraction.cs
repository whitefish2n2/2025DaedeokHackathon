using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject rayOrigin;

    public Interactable currentInteractable;
    public GameObject currentInteractableGameObject;

    public PlayerInteraction(TextMeshProUGUI interactText)
    {
    }


    private void Update()
    {
        int interactionSize = 0;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentInteractable)
            {
                currentInteractable.Interact();
            }
        }
        int interactableCount = 0;
        var hit = Physics2D.RaycastAll(
            rayOrigin.transform.position, 
            Vector2.right, 
            100f, 
            LayerMask.GetMask("Interactable"));
        
        foreach (RaycastHit2D h in hit)
        {
            if (h.transform.gameObject == currentInteractableGameObject) return;
            if(h.transform.gameObject.TryGetComponent<Interactable>(out var i))
            {
                interactionSize++;
                InteractionAnnotationPosManager.Instance.AppearAnnotation();
                InteractionAnnotationPosManager.Instance.SetText(i.annotation);
            }
        }
        if (interactionSize == 0)
        {
            currentInteractable = null; 
            currentInteractableGameObject = null;
            InteractionAnnotationPosManager.Instance.DisappearAnnotation();
        }
        

        
    }
}
