using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeTechButton : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text capacityText;
    [SerializeField] TMP_Text priceText;

    public TechType techType;

    private void Awake()
    {

    }
    [SerializeField] TechState techState;
    private void Start()
    {
       techState = TechManager.instance.GetTechState(techType);
        priceText.text = $"${techState.price}";
        titleText.text = techType.ToString();
        UpdateButton();
    }
    public void UpdateButton()
    {
        capacityText.text = $"{techState.lv}/{techState.maxLv}";
    }
    public void OnClickedUpgradeBtn()
    {
        if (techState.lv >= techState.maxLv)
            return;
        
        Debug.Log($"{techType} Improved");

        TechManager.instance.UpgradeTech( techType );
        UpdateButton();

    }

}

