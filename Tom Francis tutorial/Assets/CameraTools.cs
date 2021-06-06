using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    public float joltDecayFactor;
    public Vector3 joltVector;

    public float shakeDecayFactor;
    public float shakeAmount;

    public float maxMoveSpeed;

    Vector3 normalPosition;
    Vector3 desiredPosition;
    public Vector3 cameraOffset;

    private void Awake()
    {
        References.cameraTools = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
        cameraOffset = transform.position - References.thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Set position by looking at player's position and adding the offset
        if (References.thePlayer != null)
        {
            normalPosition = References.thePlayer.transform.position + cameraOffset;
        }

        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosition = normalPosition + joltVector + shakeVector;
        
        // Screen jolt and shake
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, maxMoveSpeed * Time.deltaTime);

        joltVector *= joltDecayFactor;
        shakeAmount *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
