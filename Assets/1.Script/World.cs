using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public WorldType worldType;

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
