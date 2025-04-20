using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public DialogueData dialogueData;

    public void TriggerAction(Character character)
    {
        if (!DialogueManager.Instance.IsSpeaking())
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
        else
        {
            DialogueManager.Instance.ShowNextLine();
        }
    }
}
