using UnityEngine;
using System.Collections;
using System;

public class GOSceneManager : MonoBehaviour
{
    public static GOSceneManager Instance { get; set; }

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayNext()
    {
        var cur = (int)PlayerSettings.Instance.LastLevel;
        if (Enum.IsDefined(typeof(SceneNames), ++cur))
            UnityEngine.SceneManagement.SceneManager.LoadScene(((SceneNames)cur).ToString());
        else
        {
            cur = (int)SceneNames.lvl1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(((SceneNames)(cur)).ToString());
        }

        PlayerSettings.Instance.LastLevel = (SceneNames)cur;
    }
}
