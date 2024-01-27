using UnityEngine;

public class Joke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.RaiseJokeCollectedEvent(this);
        }
    }
}
