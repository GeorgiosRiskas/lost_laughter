using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPC parentNpc = default;
    private bool readyToSpeak;

    [SerializeField] private string talkInstruction = default;
    [SerializeField] private string completedInstruction = default;

    PlayerControls inputActions;
    private GameLogicManager gameLogicManager;

    private void Start()
    {
        gameLogicManager = GameLogicManager.Instance;
        inputActions = new PlayerControls();
        inputActions.PlayerMovement.Talk.performed += Talk_performed;
        inputActions.Enable();
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
            EventsManager.RaiseHideNotificationEvent();
            readyToSpeak = false;
            EventsManager.RaiseDialogueEndedEvent();
        }
    }
}
