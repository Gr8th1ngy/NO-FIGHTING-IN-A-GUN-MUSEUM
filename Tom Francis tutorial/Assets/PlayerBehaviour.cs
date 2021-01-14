using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;

    //// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float maxDistanceToMove = speed * Time.deltaTime;

        // Vertical movement
        transform.position += Vector3.forward * Input.GetAxis("Vertical") * maxDistanceToMove;
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * maxDistanceToMove;
    }
}
