using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [HideInInspector]
    public bool Pressed;
    int holdCount = 0;
    public bool pushed;
    public Virtual_Controller virtual_Controller;


    // Start is called before the first frame update
    void Start()
    {

        if (!virtual_Controller)
        {
            GameObject.Find("Virtual Controller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (holdCount == 0 && Pressed)
        {
            pushed = true;
            virtual_Controller.PlayButtonClip(0);
        }
        else
        {
            pushed = false;
        }

        if (Pressed)
        {
            holdCount++;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        pushed = false;

        if (holdCount >= 1)
        {
            holdCount = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        print("presed");
    }
}
