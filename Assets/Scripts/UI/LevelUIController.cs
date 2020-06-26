using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private Menu lose;
    [SerializeField] private WinMenu win;
    [SerializeField] private Menu settings;
    [SerializeField] private Player player;
    [SerializeField] private Level1 level;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI wowText;
    private int wowCounter = 0;

    private float elapsedTime = 0;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player.onDestroy += lose.Show;
        level.onStop += win.Show;
        level.onWow += (val) => 
        {
            if (val)
            {
                wowCounter += 2;
                wowText.text = wowCounter + "X";

            }
            else
            {
                wowCounter = 0;
                wowText.text = "";
            }
        };
        Level1.onPause += () => { isPaused = true; };
        Level1.onResume += () => { isPaused = false; };

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            timer.text = elapsedTime.ToString("F") + "s";
            elapsedTime += Time.deltaTime;
        }
    }
}
