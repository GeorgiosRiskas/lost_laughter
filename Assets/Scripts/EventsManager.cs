public class EventsManager
{
    public delegate void OnJokeCollected(Joke joke);
    public static event OnJokeCollected OnJokeCollectedEvent;
    public static void RaiseJokeCollectedEvent(Joke joke)
    {
        OnJokeCollectedEvent?.Invoke(joke);
    }

    public delegate void OnDialogueStarted(string dialogue);
    public static event OnDialogueStarted OnDialogueStartedEvent;
    public static void RaiseDialogueStartedEvent(string dialogue)
    {
        OnDialogueStartedEvent?.Invoke(dialogue);
    }
}
