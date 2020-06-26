using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ObstacleInfo", menuName = "ObstacleInfo", order = 0)]
class ObstacleInfo : ScriptableObject
{
    [SerializeField] private GameObject prefab;  
    public GameObject Prefab
    {
        get { return prefab; }
        set { prefab = value; }
    }

    [SerializeField] private bool isNew;   
    public bool IsNew
    {
        get { return isNew; }
        set { isNew = value; }
    }
}

