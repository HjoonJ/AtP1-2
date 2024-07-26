using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoPanel : MonoBehaviour
{
    public TMP_Text yearText;
    public TMP_Text moneyText;

    void Update()
    {
        yearText.text = $"Year {GameManager.instance.currentYear}";
        moneyText.text = $"{GameManager.instance.money} Divine$";
    }
}
