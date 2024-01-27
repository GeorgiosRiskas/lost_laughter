using UnityEngine;

public class Piece : MonoBehaviour
{
    public Laughter laughter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.RaisePieceCollectedEvent();
        }
    }
}
