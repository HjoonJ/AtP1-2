using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float pollution = 0;
    public float happiness = 50;

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
