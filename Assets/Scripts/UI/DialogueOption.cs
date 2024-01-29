using TMPro;
using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    public JokeSO jokeSo = default;
    [SerializeField] private TextMeshProUGUI dialogueOptionText = default;

    void Start()
    {
        dialogueOptionText.text = jokeSo.JokeDescription;
    }

    public void EvaluateDialogue()
    {
        EventsManager.RaisePlayerRespondedEvent(jokeSo);
    }

    public void ExitDialogue()
    {
        EventsManager.RaiseDialogueExitedEvent();
    }
}
