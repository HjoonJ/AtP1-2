using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HumanTechInfoPanel : MonoBehaviour
{
    public static HumanTechInfoPanel instance;

    public TMP_Text infoText;
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void OpenTechInfo (TechType type)
    {
        gameObject.SetActive(true);
        TechState techState = TechManager.instance.GetTechState(type);
        infoText.text = techState.info;

    }

       

}
