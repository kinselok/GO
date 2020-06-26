using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Toggle soundButton;
    [SerializeField] private ShopPlayerColors shopPlayerColors;

    public void OnSoundChanged(bool value)
    {
        PlayerSettings.Instance.Sound = value;
    }

    public void Play()
    {
        SceneManager.LoadScene(PlayerSettings.Instance.LastLevel.ToString());
    }

    public void ShowShop()
    {
        shopPlayerColors.Show();
    }


    // Start is called before the first frame update
    void Start()
    {
        soundButton.isOn = PlayerSettings.Instance.Sound;
    }

}
