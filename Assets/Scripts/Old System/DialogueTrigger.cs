using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Button previousDialogue;
    public Button nextDialogue;
    public DialogueManager dialogueManager;
    public List<Button> options;
    public Dialogue dialogue;
    private Button thisDialogue;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        thisDialogue = GetComponent<Button>();
    }
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
        this.GetComponentInChildren<Button>().gameObject.SetActive(false);
    }
    public void TriggerSelection()
    {
        dialogueManager.StartDialogue(dialogue);
        this.gameObject.SetActive(false);
    }
    public void KeepGoing()
    {
        if (previousDialogue)
        {
            if (!dialogueManager.animator.GetBool("isOpen") && !previousDialogue.gameObject.activeInHierarchy && thisDialogue.onClick.GetPersistentMethodName(0) == "KeepGoing")
            {
                StartCoroutine(WaitForEnoughTime());
            }
        }
    }
    public void LoadOptions()
    {
        if (this.options.Count > 0 && dialogueManager.sentences.Count == 0 && dialogueManager.animator.GetBool("isOpen"))
        {
            for (int i = 0; i < this.options.Count; i++)
            {
                this.options[i].gameObject.GetComponentInChildren<Button>().gameObject.SetActive(false);
                //this.options[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(false);
                this.options[i].gameObject.SetActive(true);
                Debug.Log(this.options[i].name);
            }
        }
    }
    public void UnloadOptions()
    {
        if (this.options.Count > 0 && dialogueManager.animator.GetBool("isOpen"))
        {
            for (int i = 0; i < this.options.Count; i++)
            {
                this.options[i].gameObject.SetActive(false);
                Debug.Log(this.options[i].name);
            }
        }
    }
    public void ChooseOption()
    {
        dialogueManager.animator.SetBool("isOpen", false);
        for (int i = 0; i < previousDialogue.gameObject.GetComponentInParent<DialogueTrigger>().options.Count; i++)
        {
            previousDialogue.gameObject.GetComponentInParent<DialogueTrigger>().options[i].gameObject.SetActive(false);
        }
        TriggerSelection();
    }
    IEnumerator WaitForEnoughTime()
    {
        yield return new WaitForSeconds(0.6f);
        TriggerSelection();
    }
    private void Update()
    {
        KeepGoing();
        LoadOptions();
    }
}
