using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterBar : MonoBehaviour
{
    public ParameterType parameterType;
    Image processingBar;
    private void Awake()
    {
        processingBar = GetComponent<Image>();
    }
    public void UpdateProgressingBar()
    {
        float parameterValue = GameManager.instance.GetParameterValue(parameterType);
        processingBar.fillAmount = parameterValue / GameManager.MAX_PARAMETER_VALUE;
    }
    private void Update()
    {
        UpdateProgressingBar();
    }
    private void Start()
    {
        UpdateProgressingBar();
    }
}
