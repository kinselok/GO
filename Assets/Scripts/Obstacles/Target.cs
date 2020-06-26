using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : Obstacle
{
    [SerializeField] private float secondsForRemove;
    private IEnumerator pressCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        var swipeDetector = GetComponent<IInputHandler>();       
        swipeDetector.OnPress += () =>
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            pressCoroutine = OnPress();
            StartCoroutine(pressCoroutine);
        };
        swipeDetector.OnRelease += () =>
        {
            if(pressCoroutine != null)
                StopCoroutine(pressCoroutine);
        };
    }


    IEnumerator OnPress()
    {
        yield return new WaitForSeconds(secondsForRemove);
        OnWow(isClose);
        if (isClose)
        {
            OnNewElementRemoved();
            var colliders = gameObject.GetComponents<Collider>();
            foreach (var item in colliders)
                item.enabled = false;
            Time.timeScale = 0.5f;
            transform.DOScale(Vector3.zero, 0.5f).onComplete += () => 
            {               
                Remove();              
                transform.localScale = new Vector3(1f, 1f, 1f); 
                foreach (var item in colliders)
                    item.enabled = true;
                PlayerSettings.Instance.WowCounter++;
            };
        }
        else
        {           
            transform.localScale = new Vector3(1f, 1f, 1f);
            OnNewElementRemoved();
            Remove();
        }
    }
}
