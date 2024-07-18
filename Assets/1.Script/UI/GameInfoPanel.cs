using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoPanel : MonoBehaviour
{
    public TMP_Text yearText;

    void Update()
    {
        yearText.text = $"{GameManager.instance.currentYear}-Year";
    }
}
