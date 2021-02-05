using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float secondsBetweenShots;
    public float accuracy;
    public int numberOfProjectiles;
    public float kickAmount;

    float secondsSinceLastShot;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public void Fire(Vector3 targetPosition)
    {
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            // Activate spawner if firing
            References.levelManager.alarmSounded = true;
            audioSource.Play();
            References.screenshake.joltVector = transform.forward * kickAmount;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);

                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;

                // Offset targetPosition according to our inaccuracy;
                Vector3 inaccuratePosition = targetPosition;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);
                bullet.transform.LookAt(inaccuratePosition);
                secondsSinceLastShot = 0;
            }
        }
    }

    public void PickedUpByPlayer()
    {
        References.thePlayer.weapons.Add(this);
        transform.position = References.thePlayer.transform.position;
        transform.rotation = References.thePlayer.transform.rotation;
        transform.SetParent(References.thePlayer.transform);
        References.thePlayer.SelectLatestWeapon();
    }
}
