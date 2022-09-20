using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text headLabel;
    [SerializeField] private Text textLabel;
    [SerializeField] Color endColor;

    public bool IsOpen { get; private set; }
    public GameObject kapiUI;
    public GameObject kapiShutdown;

    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;
    private LevelChanger levelChanger;
    private AudioTrigger audioTrigger;

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        levelChanger = FindObjectOfType<LevelChanger>();
        audioTrigger = FindObjectOfType<AudioTrigger>();
        //CloseDialogueBox();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepTroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepTroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            string title = dialogueObject.Title[i];

            headLabel.text = title;
            //headLabel.color = Color.cyan;
            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            if (i == dialogueObject.Dialogue.Length - 1 && !dialogueObject.HasResponses)
            {
                textLabel.rectTransform.sizeDelta = new Vector2(-65,-65);
                textLabel.alignment = TextAnchor.UpperCenter;
                textLabel.color = endColor;
                textLabel.fontSize = 55;
                yield return new WaitForSeconds(2f);
                kapiShutdown.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                kapiUI.SetActive(true);
                yield return new WaitForSeconds(1f);
                levelChanger.ShowMenu();
            }
            
            yield return RunTypingEffect(dialogue);
            
            

            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);
        //typeWriterEffect.Run(dialogue, headLabel);

        while (typeWriterEffect.IsRunning)
        {
            yield return null;
        }
    }
    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        headLabel.text = string.Empty;
        textLabel.text = string.Empty;
    }
}
