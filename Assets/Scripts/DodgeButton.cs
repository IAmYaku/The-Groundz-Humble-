using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DodgeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Virtual_Controller virtual_Controller;
    public Animator dodgeAnim;
    [HideInInspector]
    public bool Pressed;
    int holdCount = 0;
    public bool pushed;

    // Start is called before the first frame update
    void Start()
    {
        dodgeAnim = GameObject.Find("Dodge UI").GetComponent<Animator>();


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
            dodgeAnim.SetTrigger("Dodge");
            virtual_Controller.PlayButtonClip(.85f + Random.Range(-0.05f, .05f));
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
