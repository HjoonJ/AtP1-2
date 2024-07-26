using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingCanvas : MonoBehaviour
{
    public TMP_Text messageText;
    public GameObject endBtnObject;
    public GameObject backgroundSound;

    // 엔딩 메시지들
    string ending1 = "Low Pollution, Low Happiness: A difficult balance, but you managed.";
    string ending2 = "Low Pollution, High Happiness: You have achieved harmony!";
    string ending3 = "High Pollution, Low Happiness: The world is in turmoil.";
    string ending4 = "High Pollution, High Happiness: People are happy, but the world suffers.";
    string gameOverPollution = "The world is too polluted to continue.";
    string gameOverHappiness = "The people have lost all hope.";

    public void ShowEnd(int endingType)
    {
        gameObject.SetActive(true);
        messageText.text = null;
        backgroundSound.SetActive(false);

        switch (endingType)
        {
            case 1:
                StartCoroutine(ShowText(ending1));
                break;
            case 2:
                StartCoroutine(ShowText(ending2));
                break;
            case 3:
                StartCoroutine(ShowText(ending3));
                break;
            case 4:
                StartCoroutine(ShowText(ending4));
                break;
            case 5:
                StartCoroutine(ShowText(gameOverPollution));
                break;
            case 6:
                StartCoroutine(ShowText(gameOverHappiness));
                break;
        }
    }

    IEnumerator ShowText(string message)
    {
        char[] messageChars = message.ToCharArray();
        string curMessage = "";
        for (int i = 0; i < messageChars.Length; i++)
        {
            curMessage += messageChars[i];
            messageText.text = curMessage;
            yield return new WaitForSeconds(0.05f);
        }
        endBtnObject.SetActive(true);
    }

    public void OnClickedEndBtn()
    {
        // To the lobby
        SceneManager.LoadScene("LobbyScene");
    }
}