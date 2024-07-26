using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTimeBar : MonoBehaviour
{
    Image timeBar;
    float timer;
    void Start()
    {
        timeBar = GetComponent<Image>();
        timeBar.fillAmount = 0; 
    }


    void Update()
    {
        if (GameManager.instance.playing == false)
            return;
        timer += Time.deltaTime;
        timeBar.fillAmount = timer / 400f;
    }
}
