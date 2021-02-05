using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float secondsToExist;
    public GameObject soundObject;
    public float damage;

    float secondsAlive;

    // Start is called before the first frame update
    void Start()
    {
        secondsAlive = 0;
        Instantiate(soundObject, transform.position, transform.rotation);
    }


    private void FixedUpdate()
    {
        secondsAlive += Time.fixedDeltaTime;

        float lifeFraction = secondsAlive / secondsToExist;
        Vector3 maxScale = Vector3.one * 5;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction);

        if (secondsAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damage health system when they touch explosion
        HealthSystem theirHealthSystem = other.gameObject.GetComponentInParent<HealthSystem>();

        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(damage);
        }
    }
}
