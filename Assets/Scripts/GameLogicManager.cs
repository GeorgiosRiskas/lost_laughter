using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance;
    public List<JokeSO> jokesList = new List<JokeSO>();
    public NPC activeNpc;
    public List<NPC> completedNpcList = new List<NPC>();
    public const int npcAmount = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventsManager.OnJokeCollectedEvent += EventsManager_OnJokeCollectedEvent;
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
        EventsManager.OnPlayerSuccededEvent += EventsManager_OnPlayerSuccededEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnJokeCollectedEvent -= EventsManager_OnJokeCollectedEvent;
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
        EventsManager.OnPlayerSuccededEvent -= EventsManager_OnPlayerSuccededEvent;
    }

    private void EventsManager_OnPlayerSuccededEvent(NPC npc)
    {
        if (!completedNpcList.Contains(npc))
        {
            completedNpcList.Add(npc);
            if(completedNpcList.Count == npcAmount)
            {
                Invoke(nameof(LoadEndScene), 5);
            }
        }
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("End");
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
