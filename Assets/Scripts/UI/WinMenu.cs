using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class WinMenu : Menu
{
    [SerializeField] private TextMeshProUGUI coins;
    public void PlayNext()
    {
        GOSceneManager.Instance.PlayNext();
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneNames.menu.ToString());
    }

    public void Show(int coins)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        this.coins.text = coins.ToString();
    }
}

