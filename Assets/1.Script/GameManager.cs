using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    const int initIncome = 2; // 현재 수익

    float moneyIncreaseTimer = 0;
    const float initMoneyIncreaseTime = 1.4f; // 속도를 높이려면 숫자를 낮춰야함.
    float curMoneyIncreaseTime; // 테크를 반영한 시간값

    public const float MAX_PARAMETER_VALUE = 100f;

    public int currentYear;

    float yearTimer = 0;
    const float yearTime = 4; // one year per seconds

    public EndingCanvas endingCanvas; // EndingCanvas를 연결

    [SerializeField] int techIndex = 0;

    public OpenTechInfo[] natureOpenTechInfos;
    public OpenTechInfo[] humanOpenTechInfos;

    public bool playing;
    public string[] introTexts;// 
    void Start()
    {
        playing = false;
        //StartGame();

        for (int i = 0; i < natureOpenTechInfos.Length; i++)
        {
            foreach (Button btn in natureOpenTechInfos[i].techButtons)
            {
                btn.interactable = false;
            }
        }
        for (int i = 0; i < humanOpenTechInfos.Length; i++)
        {
            foreach (Button btn in humanOpenTechInfos[i].techButtons)
            {
                btn.interactable = false;
            }
        }

        currentYear = 0;
        curMoneyIncreaseTime = initMoneyIncreaseTime;
        curPollutionPerTime = initPollutionPerTime;
        curHappinessPerTime = initHappinessPerTime;
        money = 10;

        techIndex = 0;
        OpenTech(techIndex);

        StartCoroutine(CoStartIntro());
        
    }

    IEnumerator CoStartIntro()
    {
        BubbleCanvas bubbleCanvas = FindObjectOfType<BubbleCanvas>();
        for (int i = 0; i < introTexts.Length; i++)
        {
            string str = introTexts[i];
            bool endDialogue = false;
            bubbleCanvas.ShowDialogue(str, () => { 
                endDialogue = true;
            });


            yield return new WaitUntil(() => endDialogue);
        }
        bubbleCanvas.gameObject.SetActive(false);
        StartCoroutine(FadeEffectCanvas.instance.FadeTo(1, 0));
        StartCoroutine(FadeEffectCanvas.instance.FadeTo(0, 1));
        yield return new WaitForSeconds(0.8f);
        StartGame();
    }


    public void StartGame()
    {
        playing=true;

       

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

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void CompleteGame()
    {
        Debug.Log("Game Complete!");
        DetermineEnding();
    }

    void DetermineEnding()
    {
        bool isPollutionLow = pollution <= 50;
        bool isHappinessHigh = happiness >= 50;

        if (isPollutionLow && !isHappinessHigh)
        {
            Debug.Log("Ending 1: Low Pollution, Low Happiness");
            endingCanvas.ShowEnd(1);
            // Ending 1 logic
        }
        else if (isPollutionLow && isHappinessHigh)
        {
            Debug.Log("Ending 2: Low Pollution, High Happiness");
            endingCanvas.ShowEnd(2);
            // Ending 2 logic
        }
        else if (!isPollutionLow && !isHappinessHigh)
        {
            Debug.Log("Ending 3: High Pollution, Low Happiness");
            endingCanvas.ShowEnd(3);
            // Ending 3 logic
        }
        else if (!isPollutionLow && isHappinessHigh)
        {
            Debug.Log("Ending 4: High Pollution, High Happiness");
            endingCanvas.ShowEnd(4);
            // Ending 4 logic
        }
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
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    AddPollution(5);
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    AddPollution(-5);
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    AddHappiness(5);
        //}
        if (Input.GetKeyDown(KeyCode.D)) // 시간 빨리 감기
        {
            //AddHappiness(-5);
            Time.timeScale += 1;
        }



        if (playing == false)
            return;

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
        for (int i = 0; natureOpenTechInfos[index].techButtons.Length > i; i++)
        {
            natureOpenTechInfos[index].techButtons[i].interactable = true;
        }
        for (int i = 0; humanOpenTechInfos[index].techButtons.Length > i; i++)
        {
            humanOpenTechInfos[index].techButtons[i].interactable = true;
        }

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
        if (parameterType == ParameterType.Pollution)
        {
            Debug.Log("Game Over due to Pollution");
            endingCanvas.ShowEnd(5); // Pollution으로 인한 게임오버
            //pollutionGameOverPanel.SetActive(true);
        }
        // GameOver Ending1 = under Happiness
        else if (parameterType == ParameterType.Happiness)
        {
            Debug.Log("Game Over due to Happiness");
            endingCanvas.ShowEnd(6); // Happiness 감소로 인한 게임오버
            //happinessGameOverPanel.SetActive(true);
        }
    }
}


public enum ParameterType
{
    Pollution,
    Happiness
}

[System.Serializable]
public class OpenTechInfo
{
    public Button[] techButtons;
}