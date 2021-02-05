using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public float joltDecayFactor;
    public Vector3 joltVector;

    public float shakeDecayFactor;
    public float shakeAmount;

    public float maxMoveSpeed;

    Vector3 normalPosition;
    Vector3 desiredPosition;

    private void Awake()
    {
        References.screenshake = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
