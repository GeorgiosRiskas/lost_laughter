using UnityEngine;

public class NPC : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioSource greetingAudio;
	public NpcSO npcSo;
	private Animator animator;

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
		EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
		EventsManager.OnPlayerRespondedEvent += EventsManager_OnPlayerRespondedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
		EventsManager.OnPlayerRespondedEvent -= EventsManager_OnPlayerRespondedEvent;
	}

	private void EventsManager_OnPlayerRespondedEvent(JokeSO joke)
	{
		if (GameLogicManager.Instance.activeNpc != this)
			return;

		if (npcSo.correctJoke == joke)
		{
			greetingAudio.Stop();

			audioSource.clip = npcSo.laughterSfx;
			audioSource.Play();
			animator.SetBool("laugh", true);
		}
		else
		{
			greetingAudio.Play();
		}
	}

	private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC activeNpc)
	{
		if (activeNpc == this)
		{
			greetingAudio.Play();
		}
	}
}
