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
            TechManager.instance.UpgradeTech(techType);
            UpdateButton();
 

    }

    //public RectTransform infoPositionTr;
    public void EnterPointer()
    {
        //TechInfoPanel.instance.transform.position = infoPositionTr.position;
        if (techType == TechType.TreePlanting ||
                techType == TechType.WasteSorting ||
                techType == TechType.ForestReserve ||
                techType == TechType.AirPurification ||
                techType == TechType.RenewableEnergy ||
                techType == TechType.UrbanGreenery ||
                techType == TechType.CarbonNeutral)
        {
            NatureTechInfoPanel.instance.OpenTechInfo(techType);
        }
        else if (techType == TechType.IncomeIncrease ||
                 techType == TechType.HousingImprovement ||
                 techType == TechType.HighSpeedInternet ||
                 techType == TechType.CommercialDistrict ||
                 techType == TechType.PublicTransport ||
                 techType == TechType.HealthEducation ||
                 techType == TechType.AdvancedResearch)
        {
            HumanTechInfoPanel.instance.OpenTechInfo(techType);
        }
    }
    public void ExitPointer()
    {
        if (techType == TechType.TreePlanting ||
           techType == TechType.WasteSorting ||
           techType == TechType.ForestReserve ||
           techType == TechType.AirPurification ||
           techType == TechType.RenewableEnergy ||
           techType == TechType.UrbanGreenery ||
           techType == TechType.CarbonNeutral)
        {
            NatureTechInfoPanel.instance.gameObject.SetActive(false);
        }
        else if (techType == TechType.IncomeIncrease ||
                 techType == TechType.HousingImprovement ||
                 techType == TechType.HighSpeedInternet ||
                 techType == TechType.CommercialDistrict ||
                 techType == TechType.PublicTransport ||
                 techType == TechType.HealthEducation ||
                 techType == TechType.AdvancedResearch)
        {
            HumanTechInfoPanel.instance.gameObject.SetActive(false);
        }
    }
}

