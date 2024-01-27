using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject content = default;
    [SerializeField] private TextMeshProUGUI dialogueText = default;
    [SerializeField] private List<DialogueOption> options = new List<DialogueOption>();

    private GameLogicManager gameLogicManager;

    void Start()
    {
        gameLogicManager = GameLogicManager.Instance;
        var opts = GetComponentsInChildren<DialogueOption>(true);
        foreach (var option in opts)
        {
            options.Add(option);
        }

        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent += EventsManager_OnDialogueEndedEvent;
        EventsManager.OnPlayerRespondedEvent += EventsManager_OnPlayerRespondedEvent;
    }



    private void OnDestroy()
    {
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent -= EventsManager_OnDialogueEndedEvent;
        EventsManager.OnPlayerRespondedEvent -= EventsManager_OnPlayerRespondedEvent;
    }

    private void EventsManager_OnPlayerRespondedEvent(JokeSO joke)
    {
        if (gameLogicManager.activeNpc.npcSo.correctJoke == joke)
        {
            dialogueText.text = gameLogicManager.activeNpc.npcSo.dialogue_success;
        }
        else
        {
            dialogueText.text = gameLogicManager.activeNpc.npcSo.dialogue_failure_notFunny;
        }
    }

    private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC activeNpc)
    {
        CheckUnlockedJokes();
        content.SetActive(true);
        dialogueText.text = dialogue;
    }

    private void EventsManager_OnDialogueEndedEvent()
    {
        content.SetActive(false);
    }

    private void CheckUnlockedJokes()
    {
        for (int i = 0; i < options.Count - 1; i++)
        {
            if (gameLogicManager.jokesList.Contains(options[i].jokeSo))
            {
                options[i].gameObject.SetActive(true);
            }
            else
            {
                options[i].gameObject.SetActive(false);
            }

        }
    }
}
