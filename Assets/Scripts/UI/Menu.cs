using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }


}

