using UI;
using UnityEngine;

public class InteractableDialogNPC : Interactable
{
    [SerializeField] private string[] lines;
    [SerializeField] private string npcName;
    [SerializeField] private bool interactOnce;
    private bool interacted;
    public override void Interact()
    {
        if (interactOnce && interacted) return;
        DialogueSystem.Instance.SetDialogue(lines, npcName);
        DialogueSystem.Instance.StartDialogue();
        
        base.Interact();
    }
}

