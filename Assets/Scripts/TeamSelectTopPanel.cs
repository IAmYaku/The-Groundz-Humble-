using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class TeamSelectTopPanel : MonoBehaviour
{
    public MultiplayerEventSystem multiplayerEventSystem;

    GameObject setObject;
    bool toggled;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggled)
        {
            if (setObject != multiplayerEventSystem.currentSelectedGameObject)
            {
                SetSelected(setObject);
            }
            else
            {
                print("Selected");
                toggled = false;
            }
        }
    }

   public void SetSelected (GameObject uigGameObject)
    {
         toggled = true;
        multiplayerEventSystem.SetSelectedGameObject(uigGameObject);
        setObject = uigGameObject;


    }
}
