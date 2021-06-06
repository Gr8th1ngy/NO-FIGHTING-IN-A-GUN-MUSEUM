using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlinthBehaviour : MonoBehaviour
{
    public TextMeshProUGUI myLabel;
    public Transform spotForItem;
    public GameObject cage;

    public float secondsToLock;

    Useable myUseable;

    public void AssignItem(GameObject item)
    {
        myUseable = item.GetComponent<Useable>();

        myUseable.transform.position = spotForItem.position;
        myUseable.transform.rotation = spotForItem.rotation;

        myLabel.text = myUseable.displayName;
    }

    private void OnEnable()
    {
        References.plinths.Add(this);
    }

    private void OnDisable()
    {
        References.plinths.Remove(this);
    }

    private void Update()
    {
        if (myUseable != null && !myUseable.enabled)
        {
            // if usable is not enabled, player picked it up
            myUseable = null;
        }

        if (secondsToLock > 0 && References.alarmManager.AlarmHasSounded())
        {
            secondsToLock -= Time.deltaTime;

            if (secondsToLock <= 0)
            {
                cage.SetActive(true);
                myLabel.text = "ALARM";

                if (myUseable != null && myUseable.enabled)
                {
                    Destroy(myUseable);
                }
            }
        }
    }
}
