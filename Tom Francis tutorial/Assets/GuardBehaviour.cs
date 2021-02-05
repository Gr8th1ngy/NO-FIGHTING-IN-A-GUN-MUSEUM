using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : EnemyBehaviour
{
    public float turnSpeed;
    public float visionRange;
    public float visionConeAngle;
    public Light myLight;
    public WeaponBehaviour myWeapon;
    public float reactionTime;

    bool alerted;
    float secondsSeeingPlayer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        alerted = false;
        GoToRandomNavPoint();
        secondsSeeingPlayer = 0;
    }

    void GoToRandomNavPoint()
    {

        int randomNavPointIndex = Random.Range(0, References.navPoints.Count);
        navAgent.destination = References.navPoints[randomNavPointIndex].transform.position;
    }

    protected bool CanSeePlayer()
    {
        if (References.thePlayer == null)
        {
            return false;
        }

        Vector3 playerPosition = References.thePlayer.transform.position;
        Vector3 toPlayer = playerPosition - transform.position;

        // If true ray hits a wall before player
        return !Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude, References.wallsLayer);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (References.levelManager.alarmSounded)
        {
            alerted = true;
        }

        if (References.thePlayer != null)
        {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 toPlayer = playerPosition - transform.position;
            myLight.color = Color.white;

            if (alerted)
            {
                ChasePlayer();
                myLight.color = Color.red;

                if (CanSeePlayer())
                {
                    secondsSeeingPlayer += Time.deltaTime;
                    transform.LookAt(playerPosition);

                    if (secondsSeeingPlayer >= reactionTime)
                    {
                        myWeapon.Fire(playerPosition);
                    }
                }
                else
                {
                    secondsSeeingPlayer = 0;
                }
            } 
            else
            {
                if (navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

                // Checking if player is seen
                if (Vector3.Distance(playerPosition, transform.position) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, toPlayer) <= visionConeAngle)
                    {
                        if (!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude, References.wallsLayer))
                        {
                            alerted = true;
                            References.levelManager.alarmSounded = true;
                        }
                    }
                }
            }

        }
    }

    public void KnockoutAttempt()
    {
        if (!References.levelManager.alarmSounded)
        {
            GetComponent<HealthSystem>().KillMe();
        }
    }
}
