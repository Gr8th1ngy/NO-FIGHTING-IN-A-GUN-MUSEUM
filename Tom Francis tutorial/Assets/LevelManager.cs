using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
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
        References.alarmManager.SetUpLevel(3);
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies are dead go to next level
        if (References.allEnemies.Count < 1)
        {
            secondsBeforeNextLevel -= Time.deltaTime;

            if (secondsBeforeNextLevel <= 0)
            {
                SceneManager.LoadScene("Level 1");
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
