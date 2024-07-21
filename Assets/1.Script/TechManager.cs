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

    }

    public List<TechEffect> GetTechEffects(TechEffectType effect)
    {
        List<TechEffect> list = new List<TechEffect>();
        for (int i = 0; i < techStates.Length; i++)
        {
            if (techStates[i].lv <= 0)
                continue;

            for (int j = 0; j < techStates[i].techEffects.Length; j++)
            {
                if (techStates[i].techEffects[j].techEffectType == effect)
                {
                    list.Add(techStates[i].techEffects[j]);
                    break;
                }
            }
        }
        return list;
    }

    public void UpgradeTech(TechType techType)
    {
        // 눌릴때 마다 type에 해당되는 태그를 1 레벨 증가시키기.
        

        TechState techState = System.Array.Find(techStates, t => t.techType == techType);
        if (techState != null)
        {
            techState.lv++;
            GameManager.instance.UpgradeTech(techType);

            World[] worlds = FindObjectsOfType<World>();
            worlds[0].CheckWorld();
            worlds[1].CheckWorld();
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
    public TechEffect[] techEffects;

    public TechEffect GetTechEffect(TechEffectType techEffectType)
    {
        for (int i = 0; i < techEffects.Length; i++)
        {
            if (techEffects[i].techEffectType == techEffectType)
            {
                return techEffects[i];
            }
        }
        return null;
    }

}
[System.Serializable]
public class TechEffect
{
    public TechEffectType techEffectType;
    public string[] techEffectValues;
    
}
public enum TechEffectType
{
    AddMoney,
    AddPollution,
    AddHappiness
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

