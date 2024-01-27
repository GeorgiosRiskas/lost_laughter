public class EventsManager
{
    public delegate void OnPieceCollected();
    public static event OnPieceCollected OnPieceCollectedEvent;
    public static void RaisePieceCollectedEvent()
    {
        OnPieceCollectedEvent?.Invoke();
    }
}
