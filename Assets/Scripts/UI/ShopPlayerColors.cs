using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPlayerColors : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void BuyColor(int color)
    {
        //manage money
        PlayerSettings.Instance.Color = (PlayerColors)color;
    }
}
