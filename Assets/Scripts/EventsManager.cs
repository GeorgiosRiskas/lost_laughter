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

    public delegate void OnDialogueEnded();
    public static event OnDialogueEnded OnDialogueEndedEvent;
    public static void RaiseDialogueEndedEvent()
    {
        OnDialogueEndedEvent?.Invoke();
    }

    public delegate void OnShowNotification(string notification);
    public static event OnShowNotification OnShowNotificationEvent;
    public static void RaiseShowNotification(string notification)
    {
        OnShowNotificationEvent?.Invoke(notification);
    }

    public delegate void OnHideNotification();
    public static event OnHideNotification OnHideNotificationEvent;
    public static void RaiseHideNotificationEvent()
    {
        OnHideNotificationEvent?.Invoke();
    }
}
