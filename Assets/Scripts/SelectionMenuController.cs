using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionMenuController : MonoBehaviour
{

    // Use this for initialization
    public Button btnTheCenter;
    public Button btnTheGroundz;
    public Button btnTheBlock;

    private Button[] buttons = new Button[3];
    int i = 0;
    private bool wasRight;
    private bool wasLeft;
    private float axis;

    void Start()
    {
        buttons[0] = btnTheCenter;
        buttons[1] = btnTheGroundz;
        buttons[2] = btnTheBlock;

        btnTheGroundz.Select();


    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Left())
        {
            i = (i + 1) % buttons.Length;
            buttons[i].Select();
            print(buttons[i]);


        }
        else if (Right())
        {
            if (--i < 0)
            {
                i = buttons.Length - 1;
            }
            buttons[i].Select();
            print(buttons[i]);
        }
        

        if (Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown("joystick 2 button 1") ||
            Input.GetKeyDown("joystick 3 button 1") || Input.GetKeyDown("joystick 4 button 1"))
        {
            buttons[i].onClick.Invoke();
            print(buttons[i]);
        }
         */
    }

    bool Right()
    {
        if (wasRight && CheckAxis(0.10f, -4))
        {
            wasRight = false;
        }
        else if (!wasRight && CheckAxis(0.90f, 1))
        {
            wasRight = true;
            return true;
        }
        return false;
    }

    bool Left()
    {
        if (wasLeft && CheckAxis(-0.10f, 4))
        {
            wasLeft = false;
        }
        else if (!wasLeft && CheckAxis(-0.90f, -1))
        {
            wasLeft = true;
            return true;
        }
        return false;
    }

    bool CheckAxis(float value, int comp)
    {
        int a = Input.GetAxis("Horizontal_P1").CompareTo(value);
        int b = Input.GetAxis("Horizontal_P2").CompareTo(value);
        int c = Input.GetAxis("Horizontal_P3").CompareTo(value);
        int d = Input.GetAxis("Horizontal_P4").CompareTo(value);

        return a == 0 || a == comp || b == 0 || b == comp || c == 0 || c == comp || d == 0 || d == comp;
    }

}
