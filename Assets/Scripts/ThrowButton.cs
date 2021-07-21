using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public Virtual_Controller virtual_Controller;
    public Animator throwAnim;
    [HideInInspector]
    public bool Pressed;
    public bool pushed;

    int holdCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        throwAnim = GameObject.Find("Throw UI").GetComponent<Animator>();
        
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
            throwAnim.SetTrigger("Throw");
            virtual_Controller.PlayButtonClip(1.2f + Random.Range(-0.05f, .05f));
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
