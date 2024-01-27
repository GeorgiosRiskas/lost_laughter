using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueOption : MonoBehaviour
{
    [SerializeField] private JokeSO jokeSo = default;
    // TextMeshPro text ref
    [SerializeField] private TextMeshProUGUI dialogueOptionText = default;

    void Start()
    {
        // Use ref.text = jokeSo.jokeDescription;
        dialogueOptionText.text = jokeSo.JokeDiscription;
    }
}
