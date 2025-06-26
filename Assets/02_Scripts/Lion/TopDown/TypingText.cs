using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypingText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private string currentText;
    public float typingSpeed;

    private void Awake()
    {
        currentText = text.text;
    }

    private void OnEnable()
    {
        text.text = string.Empty;
        StartCoroutine(nameof(Typing));
    }

    IEnumerator Typing()
    {
        int textLength = currentText.Length;
        for (int i = 0; i < textLength; i++)
        {
            text.text += currentText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
