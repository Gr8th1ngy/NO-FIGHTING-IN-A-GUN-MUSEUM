using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;

    public Rigidbody ourRigidBody;
    public NavMeshAgent navAgent;

    protected void OnEnable()
    {
        References.allEnemies.Add(this);
    }

    protected void OnDisable()
    {
        References.allEnemies.Remove(this);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void ChasePlayer()
    {
        if (References.thePlayer != null)
        {
            navAgent.destination = References.thePlayer.transform.position;
        }
    }
}
