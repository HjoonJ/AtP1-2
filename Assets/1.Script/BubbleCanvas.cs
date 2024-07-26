using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class BubbleCanvas : MonoBehaviour
{
    [Tooltip("Text component for displaying dialogues.")]
    public TMP_Text dialogueText;

    private Coroutine showTextCoroutine;
    private bool showing = false;
    string currentMessage;
    public Action endCallback;
    public void ShowDialogue(string message, Action eCallback)
    {
        gameObject.SetActive(true);

        if (showing)
            return;
        
        endCallback = eCallback;
        currentMessage = message;
        
        showing = true;
        showTextCoroutine = StartCoroutine(ShowText(message, eCallback));
        //StartCoroutine(ShowText(message, voiceTime));
    }

    public void ShowFullDialogue(string text)
    {
        if (showTextCoroutine != null)
        {
            StopCoroutine(showTextCoroutine);
            showTextCoroutine = null;
        }
        dialogueText.text = text;
        showing = false;
    }
    public void OnClickedNextBtn()
    {
        if (showing == true)
        {
            ShowFullDialogue(currentMessage);
        }
        else
        {
            
            endCallback?.Invoke();
            endCallback = null;
        }
    }

    private IEnumerator ShowText(string message, Action eCallback)
    {
        char[] messageChars = message.ToCharArray();
        //float wordWaitTime = voiceTime / messageChars.Length;

        dialogueText.text = string.Empty;
        foreach (char c in messageChars)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        //eCallback?.Invoke();
        //eCallback = null;
        showing = false;
    }
}
