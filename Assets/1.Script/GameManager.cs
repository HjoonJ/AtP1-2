using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float pollution = 0; // 현재 오염도

    public float initPollutionPerTime = 0.6f;
    public float curPollutionPerTime; 

    public float happiness = 50;
    public float initHappinessPerTime = 0.2f;
    public float curHappinessPerTime;


    public int money;
    const int initIncome = 1; // 현재 수익

    float moneyIncreaseTimer = 0;
    const float initMoneyIncreaseTime = 1.5f; // 속도를 높이려면 숫자를 낮춰야함.
    float curMoneyIncreaseTime; // 테크를 반영한 시간값

    public const float MAX_PARAMETER_VALUE = 100f;

    public int currentYear;

    float yearTimer = 0;
    const float yearTime = 4; // one year per seconds



    [SerializeField] int techIndex = 0;

    void Start()
    {
        StartGame();
        
    }
    public void StartGame()
    {
        currentYear = 0;
        curMoneyIncreaseTime = initMoneyIncreaseTime;
        curPollutionPerTime = initPollutionPerTime;
        curHappinessPerTime = initHappinessPerTime;
        money = 10;
        StartCoroutine(CoIncreasePollution());
        StartCoroutine(CoDecreaseHappiness());
    }

    IEnumerator CoIncreasePollution()
    {
        while (true)
        {
            yield return null;
            AddPollution(curPollutionPerTime * Time.deltaTime);

        }

    }
    IEnumerator CoDecreaseHappiness()
    {
        while (true)
        {
            yield return null;
            AddHappiness(-curHappinessPerTime * Time.deltaTime);

        }

    }

    public void CompleteGame()
    {

    }

    public float GetParameterValue(ParameterType type)
    {
        if (type == ParameterType.Pollution)
        {
            return pollution;
        }
        else
            return happiness;

    }

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddPollution(5);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddPollution(-5);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddHappiness(5);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddHappiness(-5);
        }


        
        
        if (yearTime <= yearTimer)
        {
            NextYear();
            yearTimer = 0;
            return;
        }
        yearTimer += Time.deltaTime;


        if (curMoneyIncreaseTime <= moneyIncreaseTimer)
        {
            AddMoney();
            moneyIncreaseTimer = 0;
            return;
        }
        moneyIncreaseTimer += Time.deltaTime;

    }

    void AddMoney()
    {
        int income = initIncome;

        money += income;
    }

    void NextYear()
    {
        currentYear++;

        if (currentYear % 20 == 0)
        {
            if (techIndex < 4)
            {
                techIndex++;
                OpenTech(techIndex);
            }



        }

        if (currentYear == MAX_PARAMETER_VALUE)
        {
            CompleteGame();
        }

    }

    public void UpgradeTech(TechType type)
    {
        //Happiness Part
        // type에 해당되는 태크를 가지고 오기
        TechState curTechState = TechManager.instance.GetTechState(type);

        TechEffect happinessEffect = curTechState.GetTechEffect(TechEffectType.AddHappiness);
        if (happinessEffect != null)
        {
            int addHappiness = int.Parse(happinessEffect.techEffectValues[curTechState.lv-1]);
            AddHappiness(addHappiness);
        }

        //Pollution Part
        TechEffect pollutionEffect = curTechState.GetTechEffect(TechEffectType.AddPollution);
        if (pollutionEffect != null)
        {
            int addPollution = int.Parse(pollutionEffect.techEffectValues[curTechState.lv-1]);
            AddPollution(addPollution);
        }

        //Money Part
        TechState incomeIncreaseState = TechManager.instance.GetTechState(TechType.IncomeIncrease);
        TechState highSpeedInternetState = TechManager.instance.GetTechState(TechType.HighSpeedInternet);
        TechState commercialDistrictState = TechManager.instance.GetTechState(TechType.CommercialDistrict);

        curMoneyIncreaseTime = initMoneyIncreaseTime;

        if (incomeIncreaseState.lv > 0)
        {
            TechEffect effect = incomeIncreaseState.GetTechEffect(TechEffectType.AddMoney);
            curMoneyIncreaseTime *= (1f - float.Parse(effect.techEffectValues[incomeIncreaseState.lv - 1]));
        }
        if (highSpeedInternetState.lv > 0)
        {
            TechEffect effect = highSpeedInternetState.GetTechEffect(TechEffectType.AddMoney);
            curMoneyIncreaseTime *= (1f - float.Parse(effect.techEffectValues[highSpeedInternetState.lv - 1]));
        }
        if (commercialDistrictState.lv > 0)
        {
            TechEffect effect = commercialDistrictState.GetTechEffect(TechEffectType.AddMoney);
            curMoneyIncreaseTime *= (1f - float.Parse(effect.techEffectValues[commercialDistrictState.lv - 1]));
        }
    }

    void OpenTech(int index)
    {

    }

    public void AddPollution(float p)
    {
        
        pollution += p;
        if (pollution > MAX_PARAMETER_VALUE) 
        { 
            pollution = MAX_PARAMETER_VALUE;
            // Call GameOver()
            GameOver(ParameterType.Pollution);
        }

        if (pollution < 0) 
        { 
            pollution = 0; 
        }



    }

    public void AddHappiness(float h)
    {
        happiness += h;
        if (happiness > MAX_PARAMETER_VALUE) happiness = MAX_PARAMETER_VALUE;
        if (happiness < 0) 
        { 
            happiness = 0;
            // Call GameOver()
            GameOver(ParameterType.Happiness);
        }
    }
    public void GameOver(ParameterType parameterType)
    {
        // GameOver Ending1 = over Pollution

        // GameOver Ending1 = under Happiness
    }
}


public enum ParameterType
{
    Pollution,
    Happiness
}
