using UnityEngine;

public class NPC : MonoBehaviour
{
    public NpcSO laughter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.RaisePieceCollectedEvent();
        }
    }
}
