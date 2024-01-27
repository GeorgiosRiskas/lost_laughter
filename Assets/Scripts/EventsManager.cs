using System.Diagnostics;

public class EventsManager
{
    public delegate void OnJokeCollected(JokeSO joke);
    public static event OnJokeCollected OnJokeCollectedEvent;
    public static void RaiseJokeCollectedEvent(JokeSO joke)
    {
        OnJokeCollectedEvent?.Invoke(joke);
    }

    public delegate void OnDialogueStarted(string dialogue, NPC activeNpc);
    public static event OnDialogueStarted OnDialogueStartedEvent;
    public static void RaiseDialogueStartedEvent(string dialogue, NPC activeNpc)
    {
        OnDialogueStartedEvent?.Invoke(dialogue, activeNpc);
    }

    public delegate void OnDialogueEnded();
    public static event OnDialogueEnded OnDialogueEndedEvent;
    public static void RaiseDialogueEndedEvent()
    {
        OnDialogueEndedEvent?.Invoke();
    }

    public delegate void OnPlayerResponded(JokeSO joke);
    public static event OnPlayerResponded OnPlayerRespondedEvent;
    public static void RaisePlayerRespondedEvent(JokeSO joke)
    {
        OnPlayerRespondedEvent?.Invoke(joke);
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
