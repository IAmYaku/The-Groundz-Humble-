using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SuperButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update         ...       and so is yo bihhhhh lol

    public Virtual_Controller virtual_Controller;
    public Animator superAnim;
    [HideInInspector]
    public bool Pressed;
    int holdCount = 0;
    public bool pushed;

    // Start is called before the first frame update
    void Start()
    {
        superAnim = GameObject.Find("Super UI").GetComponent<Animator>();


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
            superAnim.SetTrigger("Super");
            virtual_Controller.PlayButtonClip(1.33f + Random.Range(-0.05f, .05f));
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
