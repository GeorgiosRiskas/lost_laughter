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
        EventsManager.OnPlayerSuccededEvent += EventsManager_OnPlayerSuccededEvent;
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnPlayerSuccededEvent -= EventsManager_OnPlayerSuccededEvent;
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
    }

    private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC activeNpc)
    {
        if (activeNpc == this)
        {
            greetingAudio.Play();
        }
    }

    private void EventsManager_OnPlayerSuccededEvent(NPC npc)
    {
        if (npc == this)
        {
            audioSource.clip = npcSo.laughterSfx;
            audioSource.Play();
            animator.SetBool("laugh", true);
        }
    }
}
