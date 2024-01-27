using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJokeController : MonoBehaviour
{
    public List<Joke> jokesList = new List<Joke>();

    private void Start()
    {
        EventsManager.OnJokeCollectedEvent += EventsManager_OnJokeCollectedEvent;
    }

    private void EventsManager_OnJokeCollectedEvent(Joke joke)
    {
        if (!jokesList.Contains(joke))
        {
            jokesList.Add(joke);
        }        
    }
}
