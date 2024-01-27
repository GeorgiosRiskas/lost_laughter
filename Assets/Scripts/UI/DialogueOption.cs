using TMPro;
using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    [SerializeField] private JokeSO jokeSo = default;
    [SerializeField] private TextMeshProUGUI dialogueOptionText = default;

    void Start()
    {
        dialogueOptionText.text = jokeSo.JokeDescription;
    }
}
