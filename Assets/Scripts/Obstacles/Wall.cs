using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wall : Obstacle
{
    private int clicks = 0;

    [SerializeField]private int clicksForDestroy;


    private void Start()
    {
        var swipeDetector = GetComponent<IInputHandler>();
        swipeDetector.OnPress += OnPress;
    }

    IEnumerator delayedRemove()
    {
        yield return new WaitForSeconds(2);
        Remove();
    }

    void OnPress()
    {
        clicks++;
        if(clicks >= clicksForDestroy)
        {
            OnWow(isClose);
            OnNewElementRemoved();
            if (isClose)
            {
                var colliders = gameObject.GetComponents<Collider>();
                foreach (var item in colliders)
                    item.enabled = false;
                Time.timeScale = 0.5f;
                transform.DOScale(Vector3.zero, 0.5f).onComplete += () =>
                {
                    Remove();
                    foreach (var item in colliders)
                        item.enabled = true;
                    PlayerSettings.Instance.WowCounter++;
                };
            }
            else
            {
                Remove();
            }
            clicks = 0;
        }
    }
}
