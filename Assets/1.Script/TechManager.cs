using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager instance;
    public TechState[] techStates;

    
    public void Awake()
    {
        instance = this;
        //techStates = new TechState[16];
        //for (int i = 0; i < techStates.Length; i++)
        //{
        //    techStates[i] = new TechState();
        //    techStates[i].techType = (TechType)i;
        //    techStates[i].lv = 0;
        //    techStates[i].maxLv = 5;
        //    if (techStates[i].techType == TechType.MarineProtection ||
        //       techStates[i].techType == TechType.UrbanGreenery)
        //    {
        //        techStates[i].maxLv = 4;
        //    }
        //    if (techStates[i].techType == TechType.CarbonNeutral)
        //    {
        //        techStates[i].maxLv = 3;
        //    }
        //}
    }

    public void UpgradeTech(TechType techType)
    {
        // 눌릴때 마다 type에 해당되는 태그를 1 레벨 증가시키기.
        
        TechState techState = System.Array.Find(techStates, t => t.techType == techType);
        if (techState != null)
        {
            techState.lv++;
        }
    }

    public TechState GetTechState(TechType type)
    {
        for (int i = 0; i < techStates.Length; i++)
        {
            if (techStates[i].techType == type)
            {
                return techStates[i];
            }
            
        }
        return null;
    }

}

// TechState
[System.Serializable]
public class TechState
{
    public TechType techType;
    public int lv;
    public int maxLv;
    public int price;

}

public enum TechType
{
    TreePlanting,
    WasteSorting,
    ForestReserve,
    AirPurification,
    RenewableEnergy,
    MarineProtection,
    UrbanGreenery,
    CarbonNeutral,

    IncomeIncrease,
    HousingImprovement,
    HighSpeedInternet,
    CommercialDistrict,
    PublicTransport,
    HealthEducation,
    CulturalLeisure,
    AdvancedResearch

}

