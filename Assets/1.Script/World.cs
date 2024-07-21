using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public WorldType worldType;

    WorldObject[] worldObjects;
    private void Start()
    {
        worldObjects = GetComponentsInChildren<WorldObject>(true);
    }

    public void CheckWorld()
    {
        for (int i = 0; i < worldObjects.Length; i++)
        {
            worldObjects[i].CheckCondition();
        }
    }

#if UNITY_EDITOR
    public float worldSize;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize, worldSize, worldSize));
    }
#endif
}
public enum WorldType
{
    Nature,
    Human
}
