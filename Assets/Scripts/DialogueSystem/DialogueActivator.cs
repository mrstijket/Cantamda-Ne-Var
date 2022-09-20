using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public Interactable Interactable { get; set; }

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }
    private void OnMouseOver()
    {
        if (dialogueUI.IsOpen) return;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Interact");
            Interact(this);
        }
        // if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        // {
        //     playerController.Interactable = this;
        // }
    }
    private void EndDialogue()
    {
        Interactable = null;
        // if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        // {
        //     if (playerController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
        //     {
        //         playerController.Interactable = null;
        //     }
        // }
    }

    public void Interact(DialogueActivator dialogueActivator)
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            dialogueActivator.DialogueUI.AddResponseEvents(responseEvents.Events);
        }

        dialogueActivator.DialogueUI.ShowDialogue(dialogueObject);
    }
}
