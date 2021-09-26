using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class QuickCharacterSelect : TeamSelect
{    
    public override void Start()
    {
        print("------- QuickCharacterSelect -------");
        base.Start();
    }
    public override void Update() 
    {
        Gamepad g = Gamepad.current;
        if(null != g)
        {
            if(!ready && g.buttonWest.ReadValue() > 0)
            {
                PickMack();
            }


            if (!ready && g.buttonEast.ReadValue() > 0)
            {
                PickKing();
            }
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.A))
            {
                PickMack();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                PickMack();
            }
        }

        base.Update();
    }


    void PickMack()
    {
        SetModule1CharacterType("Mack");

        print("PlayerReady");
        PlayerReady(1);
    }

    void PickKing()
    {
        SetModule1CharacterType("King");

        print("PlayerReady");
        PlayerReady(1);
    }
}
