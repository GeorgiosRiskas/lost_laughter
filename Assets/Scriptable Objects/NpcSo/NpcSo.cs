using UnityEngine;

[CreateAssetMenu(fileName = "npcX", menuName = "Lost Laughter/npc")]
public class NpcSO : ScriptableObject
{
    public string dialogue_greeting;
    public string dialogue_failure_notFunny;
    public string dialogue_failure_oldJoke;
    public string correctJokeId;
    public string laughterId;
    public AudioClip laughterSfx;
}
