using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    public GameObject alertPipPrefab;

    public Sprite emptyPip;
    public Sprite filledPip;

    public int alertLevel;
    public int maxAlertLevel;

    List<Image> alertPips = new List<Image>();
    AudioSource alarmSound;

    private void Awake()
    {
        References.alarmManager = this;
        alarmSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (AlarmHasSounded() && !alarmSound.isPlaying)
        {
            alarmSound.Play();
        }
        else if (!AlarmHasSounded() && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }
    }

    public void SetUpLevel(int desiredMaxAlertLevel)
    {
        for (int i = 0; i < alertPips.Count; i++)
        {
            Destroy(alertPips[i].gameObject);
        }
        alertPips.Clear();

        maxAlertLevel = desiredMaxAlertLevel;

        for (int i = 0; i < maxAlertLevel; i++)
        {
            GameObject newPip = Instantiate(alertPipPrefab, transform);
            alertPips.Add(newPip.GetComponent<Image>());
        }

        alertPips.Reverse();
    }

    public void RaiseAlertLevel()
    {
        alertLevel++;
        UpdatePip();
    }

    public void SoundTheAlarm()
    {
        alertLevel = maxAlertLevel;
        UpdatePip();
    }

    public void StopTheAlarm()
    {
        alertLevel = 0;
        UpdatePip();
    }

    public bool AlarmHasSounded()
    {
        if (alertLevel == 0) return false;
        return alertLevel >= maxAlertLevel;
    }

    void UpdatePip()
    {
        for (int i = 0; i < alertPips.Count; i++)
        {
            if (i < alertLevel)
            {
                alertPips[i].sprite = filledPip;
            }
            else
            {
                alertPips[i].sprite = emptyPip;
            }
        }
    }
}
