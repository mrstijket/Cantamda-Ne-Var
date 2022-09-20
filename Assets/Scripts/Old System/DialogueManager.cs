using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float dialogueOpenTime = 1f;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public DialogueTrigger[] dialogueTriggers;
    public Queue<string> sentences;
    private Queue<string> names;
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        StartCoroutine(StartProcess(dialogue));
    }
    IEnumerator StartProcess(Dialogue dialogue)
    {
        yield return new WaitForSeconds(dialogueOpenTime);
        animator.SetBool("isOpen", true);

        names.Clear();
        sentences.Clear();
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            LoadNextDialogue();
            return;
        }
        string name = names.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeName(name));
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeName(string name)
    {
        nameText.text = "";
        foreach (char letter in name.ToCharArray())
        {
            nameText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void LoadNextDialogue()
    {
        foreach (DialogueTrigger dialogueTrigger in dialogueTriggers)
        {
            if (dialogueTrigger.nextDialogue)
            {
                dialogueTrigger.nextDialogue.gameObject.SetActive(true);
            }
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
