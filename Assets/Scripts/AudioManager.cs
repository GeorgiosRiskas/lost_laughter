using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceStart = default;
    [SerializeField] private AudioSource audioSourceMain = default;
    [SerializeField] private AudioSource audioSourceAristocrat = default;
    [SerializeField] private AudioSource audioSourceKing = default;
    [SerializeField] private AudioSource audioSourceOgre = default;

    [SerializeField] private AudioClip kingLaugh = default;
    [SerializeField] private AudioClip aristocratLaugh = default;
    [SerializeField] private AudioClip ogreLaugh = default;

    bool firstLaughtHasHappened;

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EventsManager.OnPlayerSuccededEvent += EventsManager_OnPlayerSuccededEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnPlayerSuccededEvent -= EventsManager_OnPlayerSuccededEvent;
    }

    private void EventsManager_OnPlayerSuccededEvent(NPC npc)
    {
        if(!firstLaughtHasHappened)
        {
            StartCoroutine(StartMainThemeAfterLaugh(npc));
        }
        else
        {
            if (npc.npcSo.laughterId == "Aristocrat")
            {
                audioSourceAristocrat.mute = false;
            }
            else if (npc.npcSo.laughterId == "King")
            {
                audioSourceKing.mute = false;
            }
            else if (npc.npcSo.laughterId == "Ogre")
            {
                audioSourceOgre.mute = false;
            }
        }
    }

    private IEnumerator StartMainThemeAfterLaugh(NPC npc)
    {
        audioSourceStart.Stop();

        if (npc.npcSo.laughterId == "Aristocrat")
        {
            yield return new WaitForSeconds(aristocratLaugh.length);
        }
        else if (npc.npcSo.laughterId == "King")
        {
            yield return new WaitForSeconds(kingLaugh.length);
        }
        else if (npc.npcSo.laughterId == "Ogre")
        {
            yield return new WaitForSeconds(ogreLaugh.length);
        }


        if (!firstLaughtHasHappened)
        {
            audioSourceMain.Play();
            audioSourceAristocrat.Play();
            audioSourceKing.Play();
            audioSourceOgre.Play();
        }


        if (npc.npcSo.laughterId == "Aristocrat")
        {
            audioSourceAristocrat.mute = false;
        }
        else if (npc.npcSo.laughterId == "King")
        {
            audioSourceKing.mute = false;
        }
        else if (npc.npcSo.laughterId == "Ogre")
        {
            audioSourceOgre.mute = false;
        }

        firstLaughtHasHappened = true;
    }
}
