using UnityEngine;

[CreateAssetMenu(fileName = "npcX", menuName = "Lost Laughter/npc")]
public class NpcSO : ScriptableObject
{
    public string dialogue_greeting;
    public string dialogue_success;
    public string[] dialogue_failure_notFunny;
    public JokeSO correctJoke;
    public string laughterId;
    public AudioClip laughterSfx;
}
