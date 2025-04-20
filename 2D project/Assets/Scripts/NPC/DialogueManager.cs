using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI contentText;

    private Queue<DialogueLine> dialogueQueue;
    private bool isSpeaking;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        dialogueQueue = new Queue<DialogueLine>();
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (isSpeaking) return;

        dialogueQueue.Clear();
        foreach (var line in dialogue.lines)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogueUI.SetActive(true);
        isSpeaking = true;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        var line = dialogueQueue.Dequeue();
        speakerNameText.text = line.speakerName;
        contentText.text = line.content;
    }

    private void EndDialogue()
    {
        isSpeaking = false;
        dialogueUI.SetActive(false);
    }

    public bool IsSpeaking()
    {
        return isSpeaking;
    }
}
