using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject content = default;
    [SerializeField] private TextMeshProUGUI dialogueText = default;

    void Start()
    {
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent += EventsManager_OnDialogueEndedEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent -= EventsManager_OnDialogueEndedEvent;
    }

    private void EventsManager_OnDialogueStartedEvent(string dialogue)
    {
        content.SetActive(true);
        dialogueText.text = dialogue;
    }

    private void EventsManager_OnDialogueEndedEvent()
    {
        content.SetActive(false);
    }
}
