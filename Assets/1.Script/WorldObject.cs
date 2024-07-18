using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public Condition condition;
    public void CheckCondition()
    {
        if (gameObject.activeSelf)
            return;

        TechState techState = TechManager.instance.GetTechState(condition.techType);
        if (techState.lv >= condition.lv)
        {
            gameObject.SetActive(true);
        }
    }
}
[System.Serializable]
public class Condition
{
    public TechType techType;
    public int lv;
}
