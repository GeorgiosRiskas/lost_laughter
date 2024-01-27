using UnityEngine;

public class Piece : MonoBehaviour
{
    public NPC laughter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.RaisePieceCollectedEvent();
        }
    }
}
