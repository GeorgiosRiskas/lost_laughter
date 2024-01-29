using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
	[SerializeField] private NPC parentNpc = default;
	private bool readyToSpeak;

	private string talkInstruction;
	private string completedInstruction;

	PlayerControls inputActions;
	private GameLogicManager gameLogicManager;

	private Collider thisCollider;
	private Coroutine resetTrigger;

	private void Start()
	{
		talkInstruction = "Press F to talk";
		completedInstruction = "This person is already happy!";
		thisCollider = GetComponent<Collider>();

		gameLogicManager = GameLogicManager.Instance;
		inputActions = new PlayerControls();
		inputActions.PlayerMovement.Talk.performed += Talk_performed;
		inputActions.Enable();

		EventsManager.OnDialogueExitedEvent += EventsManager_OnDialogueExitedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnDialogueExitedEvent -= EventsManager_OnDialogueExitedEvent;
	}

	private void EventsManager_OnDialogueExitedEvent()
	{
		EventsManager.RaiseHideNotificationEvent();
		readyToSpeak = false;
		EventsManager.RaiseDialogueEndedEvent();

		if (resetTrigger == null)
			resetTrigger = StartCoroutine(ResetTrigger());
	}

	private IEnumerator ResetTrigger()
	{
		thisCollider.enabled = false;
		yield return new WaitForSeconds(.2f);
		thisCollider.enabled = true;
		resetTrigger = null;
	}

	private void Talk_performed(InputAction.CallbackContext obj)
	{
		if (!readyToSpeak)
			return;

		EventsManager.RaiseDialogueStartedEvent(parentNpc.npcSo.dialogue_greeting, parentNpc);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (gameLogicManager.completedNpcList.Contains(parentNpc))
			{
				EventsManager.RaiseShowNotification(completedInstruction);
				return;
			}

			EventsManager.RaiseShowNotification(talkInstruction);
			readyToSpeak = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(resetTrigger != null)
			{
				StopCoroutine(resetTrigger);
				thisCollider.enabled = false;
			}

			EventsManager.RaiseHideNotificationEvent();
			readyToSpeak = false;
			EventsManager.RaiseDialogueEndedEvent();
		}
	}
}
