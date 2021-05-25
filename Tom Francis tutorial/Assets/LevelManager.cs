using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public string firstLevelName;
    public float graceTimeAtEndOfLevel;
    public float secondsBeforeNextLevel;
    public float secondsBeforeDeathMenu;

    bool showDeathMenu;

    private void Awake()
    {
        References.levelManager = this;
        showDeathMenu = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(firstLevelName);
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
    }

    public void StartNewGame()
    {
        Destroy(References.essentials.gameObject);
        SceneManager.LoadScene("Startup");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies are dead go to next level
        if (References.allEnemies.Count < 1)
        {
            // grace period before level transition
            secondsBeforeNextLevel -= Time.deltaTime;

            // stop alarm
            References.alarmManager.StopTheAlarm();

            if (secondsBeforeNextLevel <= 0)
            {
                SceneManager.LoadScene(References.levelGenerator.nextLevelName);
            }
        }
        else
        {
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }

        if (References.thePlayer == null && !showDeathMenu)
        {
            secondsBeforeDeathMenu -= Time.deltaTime;

            if (secondsBeforeDeathMenu <= 0)
            {
                References.theCanvas.ShowMainMenu();
                showDeathMenu = true;
            }
        }
    }
}
