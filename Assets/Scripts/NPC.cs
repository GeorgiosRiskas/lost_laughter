using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public NpcSO npcSo;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        EventsManager.OnPlayerSuccededEvent += EventsManager_OnPlayerSuccededEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnPlayerSuccededEvent -= EventsManager_OnPlayerSuccededEvent;
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
