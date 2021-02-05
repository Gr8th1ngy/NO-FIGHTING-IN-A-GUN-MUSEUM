using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public Light myLight;
    float maxLightIntensity;

    public float shakeAmount;
    AudioSource audioSource;

    public float duration;
    float secondsLeft;

    // Start is called before the first frame update
    void Start()
    {
        References.screenshake.shakeAmount = shakeAmount;
        audioSource = GetComponent<AudioSource>();
        maxLightIntensity = myLight.intensity;
        secondsLeft = duration;
    }

    // Update is called once per frame
    void Update()
    {
        myLight.intensity = (secondsLeft / duration) * maxLightIntensity;
        secondsLeft -= Time.deltaTime;

        if (secondsLeft <= 0)
        {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
