using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseMenu : Menu
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneNames.menu.ToString());
    }
}

