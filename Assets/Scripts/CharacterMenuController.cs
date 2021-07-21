using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterMenuController : MonoBehaviour {

    // Use this for initialization

    public TeamSelect teamSelect;

	//public Button btnNina;
	//public Button btnMack;
	//public Button btnKing;

    string[] joysticks;
   
    public GameObject controllerPrefab;

    public List <GameObject> controllers;

	private Button[] buttons = new Button[3];

	int i = 0;
    int inc = 0;

	private bool wasRight;
	private bool wasLeft;
	private float axis;

    public float minX = 815f;
    public float maxX = 900f;
    public float scale = 1f;


    public float thresh = .2f;

    float lastXPos;
    GameObject lastChar;

    void Start () {
        /*
        joysticks = Input.GetJoystickNames();
        joysticks = GetActualJoysticks(joysticks);

        buttons [0] = btnNina;
		buttons [1] = btnMack;
		buttons [2] = btnKing;

        btnMack.Select();

        for (int i=0; i<joysticks.Length;i++)
        {
        Vector3 controllerPos = controllerPrefab.transform.position;
            InstantiateControllerObject(new Vector3(controllerPos.x + i * 20, controllerPos.y, controllerPos.z),i);
        }

        */

        
	}
	

	void Update () {

        /* 
         
        inc++;
        if (inc%10 == 0)
        {
            joysticks = GetActualJoysticks(joysticks);
        }
        else
        {
            if (inc % 5 == 0 && joysticks.Length>=1)
            {
                for (int j=1; j <= joysticks.Length; j++)
                {
                    if (j <= controllers.Count)
                    {
                        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .5)
                        {
                            controllers[j - 1].GetComponent<ControllerMenu>().isMyTurn = GetIsMyTurn(j - 1, controllers[j - 1]);

                            if (controllers[j - 1].GetComponent<ControllerMenu>().isMyTurn)
                            {
                                //float nuX = Mathf.Clamp((controllers[j - 1].transform.position.x + scale*Mathf.Sign(Input.GetAxis("Horizontal_P" + j))),ninaXPos,kingXPos);
                                float nuX = GetNuX((Input.GetAxis("Horizontal")), controllers[j - 1].transform.position);
                                Vector3 nuPos = new Vector3(nuX, controllers[j - 1].transform.position.y, controllers[j - 1].transform.position.z);
                                controllers[j - 1].transform.position = nuPos;
                            }
                        }
                    }
                }
            }
        }

        /*
		
		if (Left()) {
			i = (i + 1) % buttons.Length;
			buttons [i].Select();
            print(i);
            print("left "+buttons[i]);

        }
		else 	if (Right()) {
			if (--i < 0) {
				i = buttons.Length - 1;
			}
			buttons [i].Select ();
            print(i);
            print("right "+buttons[i]);
       
        }
       
		if (Input.GetKeyDown ("joystick 1 button 1") || Input.GetKeyDown ("joystick 2 button 1") ||
		    Input.GetKeyDown ("joystick 3 button 1") || Input.GetKeyDown ("joystick 4 button 1")) {
			buttons [i].onClick.Invoke ();
            print(buttons[i]);
		}
         */



    }

    private float GetNuX(float v, Vector3 position)
    {
        /*
            if (Mathf.Abs(v) > thresh)
            {
                if (v > 0)
                {
                    if (position.x < kingXPos)
                    {
                        return position.x + 42.5f;
                    }
                }
                else
                {
                    if (position.x > ninaXPos)
                    {
                        return position.x - 42.5f;
                    }
                }
            }

        print(EventSystem.current.currentSelectedGameObject);
        if (btnNina.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            lastXPos = ninaXPos;
            lastChar = btnNina.gameObject;

            return ninaXPos;
        }
        if (btnMack.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            lastXPos = mackXPos;
            lastChar = btnMack.gameObject;
            return mackXPos;
        }
        if (btnKing.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            lastXPos = kingXPos;
            lastChar = btnKing.gameObject;
            return kingXPos;
        }

        else
        {
            EventSystem.current.SetSelectedGameObject(lastChar);
            return lastXPos;
        }
        */
        
        return position.x;
    }

    private bool GetIsMyTurn(int v,GameObject controlla)
    {
        /*
        if (characterSelect)
        {
        //    if (characterSelect.selected== v)
            {
                if(controlla.GetComponent<ControllerMenu>().isMyTurn == false)
                {
                    controlla.transform.position = new Vector3(ninaXPos, controlla.transform.position.y, controlla.transform.position.z);
                }

                return true;
            }
        }
        */
        return false;
    }

    bool Right()	{
		if (wasRight && CheckAxis (0.10f, -4)) {
			wasRight = false;
		} else if (!wasRight && CheckAxis (0.90f, 1)) {
			wasRight = true;
			return true;
		}
		return false;
	}

	bool Left()	{
		if (wasLeft && CheckAxis (-0.10f, 4)) {
			wasLeft = false;
		} else if (!wasLeft && CheckAxis (-0.90f, -1)) {
			wasLeft = true;
			return true;
		}
		return false;
	}

	bool CheckAxis(float value, int comp){
		int a = Input.GetAxis ("Horizontal_P1").CompareTo (value);
		int b = Input.GetAxis ("Horizontal_P2").CompareTo (value);
		int c = Input.GetAxis ("Horizontal_P3").CompareTo (value);
		int d = Input.GetAxis ("Horizontal_P4").CompareTo (value);

		return a == 0 || a == comp || b == 0 || b == comp || c == 0 || c == comp || d == 0 || d == comp;
	}
	
    public void InstantiateControllerObject(Vector3 pos0, int index)
    {
       GameObject controller = Instantiate(controllerPrefab, Vector3.zero, Quaternion.identity);
        controller.transform.parent = gameObject.transform;
        controller.transform.localPosition = pos0;
      //  controller.transform.position = new Vector3(mackXPos, controller.transform.position.y, controller.transform.position.z);
       controller.transform.localScale = new Vector3(1.45f,1.45f,1.45f);

        index++;
        if (index == 1)
        {
            controller.GetComponent<Image>().color = new Color(.5f, .5f, .75f, 1f);

        }
        if (index == 2)
        {
            controller.GetComponent<Image>().color = new Color(.75f, .5f, .5f, 1f);

        }
        if (index == 3)
        {
            controller.GetComponent<Image>().color = new Color(.5f, 0f, .5f, 1f);

        }
        if (index == 4)
        {
            controller.GetComponent<Image>().color = new Color(0f, .25f, 0f, 1f);

        }

        controllers.Add(controller);
    }

    private string[] GetActualJoysticks(string[] stix)
    {
        int j = 0;
        string[] returnMe;
        List<string> addMe = new List<string>();

        for (int i = 0; i < joysticks.Length; i++)
        {
            if (stix[i] != "")
            {
                j++;
                addMe.Add(stix[i]);
            }
        }
        returnMe = new string[j];

        for (int k = 0; k < addMe.Count; k++)
        {
            returnMe[k] = addMe[k];
        }

        return returnMe;
    }

}
