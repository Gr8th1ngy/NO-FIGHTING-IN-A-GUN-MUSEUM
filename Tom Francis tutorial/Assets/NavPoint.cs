using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = false;
        References.navPoints.Add(this);
    }

    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
