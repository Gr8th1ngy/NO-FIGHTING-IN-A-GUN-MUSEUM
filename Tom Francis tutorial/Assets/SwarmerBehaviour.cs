using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehaviour : EnemyBehaviour
{
    public GameObject explosionPrefab;

    protected void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObject = collision.gameObject;
        PlayerBehaviour possiblePlayer = collidingObject.GetComponent<PlayerBehaviour>();

        if (possiblePlayer == References.thePlayer)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
