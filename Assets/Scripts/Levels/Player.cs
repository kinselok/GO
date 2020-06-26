using System;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IDamageHandler
{
    public event Action onDestroy;
    public Material[] materials;

    private void Start()
    {
        //GetComponent<MeshRenderer>().material = materials[(int)PlayerSettings.Instance.Color];
    }

    public void ApplyDamage(int amount)
    {
        Destroy();
    }

    void Destroy()
    {
        onDestroy?.Invoke();
        PlayerSettings.Instance.Deaths++;
    }
}
