using TMPro;
using UnityEngine;

public class BookDiscoverySystem : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI titleText = default;
	[SerializeField] private TextMeshProUGUI jokeText = default;

	[SerializeField] private GameObject content = default;

	void Start()
	{
		EventsManager.OnJokeCollectedEvent += EventsManager_OnJokeCollectedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnJokeCollectedEvent -= EventsManager_OnJokeCollectedEvent;
	}

	private void EventsManager_OnJokeCollectedEvent(JokeSO joke)
	{
		Time.timeScale = 0;
		content.SetActive(true);
		titleText.text = joke.JokeComment;
		jokeText.text = joke.JokeDescription;
		//EventsManager.RaiseShowBookTextEvent(true);
	}

	public void ExitBookView()
	{
		content.SetActive(false);
		Time.timeScale = 1;
	}
}
