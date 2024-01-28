using UnityEngine;

public class Joke : MonoBehaviour
{
    public JokeSO jokeSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("!!!!!!!!!!!!");
            EventsManager.RaiseJokeCollectedEvent(jokeSo);
            gameObject.SetActive(false);
        }
    }
}
