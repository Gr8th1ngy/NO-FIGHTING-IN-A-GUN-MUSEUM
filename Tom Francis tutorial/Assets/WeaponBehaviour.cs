using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float secondsBetweenShots;
    public float accuracy;
    public int numberOfProjectiles;
    public float kickAmount;
    public int ammo;

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
        if (secondsSinceLastShot >= secondsBetweenShots && ammo > 0)
        {
            // Activate spawner if firing
            References.alarmManager.SoundTheAlarm();
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

            ammo--;
        }
    }

    public void PickedUpByPlayer()
    {
        transform.position = References.thePlayer.transform.position;
        transform.rotation = References.thePlayer.transform.rotation;
        transform.SetParent(References.thePlayer.transform);
        References.alarmManager.RaiseAlertLevel();
        References.thePlayer.PickUpWeapon(this);
    }

    public void Drop()
    {
        transform.parent = null;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        GetComponent<Useable>().enabled = true;
    }
}
