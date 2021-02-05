using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiqueBehaviour : MonoBehaviour
{
    public void BeCollected()
    {
        Destroy(gameObject);
    }
}
