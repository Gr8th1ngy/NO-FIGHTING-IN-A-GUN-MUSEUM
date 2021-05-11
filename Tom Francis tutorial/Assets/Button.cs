using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    RectTransform rectangle;
    Image image;

    public UnityEvent eventToTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rectangle = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //If mouse is in button, clicking can happen
        if (RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition))
        {
            image.color = Color.black;

            if (Input.GetButtonDown("Fire1"))
            {
                eventToTrigger.Invoke();
            }
        }
        else
        {
            image.color = Color.grey;
        }
    }

}
