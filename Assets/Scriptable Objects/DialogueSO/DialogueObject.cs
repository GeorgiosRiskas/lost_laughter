using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dialogueX", menuName = "DialogueObjectSO")]
public class DialogueObject : ScriptableObject
{
    public List<string> dialogue = new List<string>();
}
