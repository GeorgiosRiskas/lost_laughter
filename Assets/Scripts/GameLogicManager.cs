using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance;
    public List<JokeSO> jokesList = new List<JokeSO>();
    public NPC activeNpc;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventsManager.OnJokeCollectedEvent += EventsManager_OnJokeCollectedEvent;
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
    }  

    private void OnDestroy()
    {
        EventsManager.OnJokeCollectedEvent -= EventsManager_OnJokeCollectedEvent;
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
    }
    private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC _activeNpc)
    {
        activeNpc = _activeNpc;
    }

    private void EventsManager_OnJokeCollectedEvent(JokeSO joke)
    {
        if (!jokesList.Contains(joke))
        {
            jokesList.Add(joke);
        }        
    }
}
