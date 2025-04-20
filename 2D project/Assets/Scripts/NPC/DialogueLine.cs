using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DialogueLine
{
   public string speakerName;
    [TextArea]
    public string content;
}

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> lines;
}
