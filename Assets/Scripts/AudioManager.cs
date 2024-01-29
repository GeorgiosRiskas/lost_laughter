using System.Collections;
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
	private GameLogicManager gameLogic;

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

	private void OnEnable()
	{
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void OnDisable()
	{
		EventsManager.OnPlayerSuccededEvent -= EventsManager_OnPlayerSuccededEvent;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		if (arg0.name == "Level")
		{
			gameLogic = GameLogicManager.Instance;
			EventsManager.OnPlayerSuccededEvent += EventsManager_OnPlayerSuccededEvent;
		}
	}

	private void EventsManager_OnPlayerSuccededEvent(NPC npc)
	{
		StartCoroutine(StartThemeAfterLaugh(npc));
	}

	private IEnumerator StartThemeAfterLaugh(NPC npc)
	{
		if (!firstLaughtHasHappened)
		{
			audioSourceStart.Stop();

			// Wait for the laugh to happen, without music
			yield return WaitForLaughter(npc);

			StartMainAudioSources();
			RememberUnlockedTracks();
			firstLaughtHasHappened = true;
		}
		else
		{
			MuteMainAudioSources();

			// Wait for the laugh to happen, without music
			yield return WaitForLaughter(npc);

			RememberUnlockedTracks();
		}
	}

	private IEnumerator WaitForLaughter(NPC npc)
	{
		// Wait for the laugh to happen, without music
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
	}

	private void StartMainAudioSources()
	{
		audioSourceMain.Play();
		audioSourceAristocrat.Play();
		audioSourceKing.Play();
		audioSourceOgre.Play();
	}

	private void MuteMainAudioSources()
	{
		audioSourceMain.mute = true;
		audioSourceAristocrat.mute = true;
		audioSourceKing.mute = true;
		audioSourceOgre.mute = true;
	}

	private void UnmuteNpcLaughterTrack(NPC npc)
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

	void RememberUnlockedTracks()
	{
		audioSourceMain.mute = false;

		for (int i = 0; i < gameLogic.completedNpcList.Count; i++)
		{
			if (gameLogic.completedNpcList[i].npcSo.laughterId == "Aristocrat")
			{
				audioSourceAristocrat.mute = false;
			}
			else if (gameLogic.completedNpcList[i].npcSo.laughterId == "King")
			{
				audioSourceKing.mute = false;
			}
			else if (gameLogic.completedNpcList[i].npcSo.laughterId == "Ogre")
			{
				audioSourceOgre.mute = false;
			}
		}
	}
}
