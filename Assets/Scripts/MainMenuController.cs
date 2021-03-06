using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour {

    // Use this for initialization
    public Button btnPlayArcade;
	public Button btnSettings;
	public Button btnControls;
    public Button btnQuit;


    private Button[] buttons = new Button[4];
	int i = 0;
	public bool wasUp;
	public bool wasDown;
	private float axis;

    public float aV_0;

	void Start () {
        buttons[0] = btnPlayArcade;
		buttons [1] = btnSettings;
        buttons[2] = btnControls;
        buttons[3] = btnQuit;


        btnPlayArcade.Select ();


	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (Down()) {
			i = (i + 1) % buttons.Length;
			buttons [i].Select();

        }
		else 	if (Up()) {
			if (--i < 0) {
				i = buttons.Length - 1;
			}
			buttons [i].Select ();
        }
        
		if (Input.GetKeyDown ("joystick 1 button 1") || Input.GetKeyDown ("joystick 2 button 1") ||
		    Input.GetKeyDown ("joystick 3 button 1") || Input.GetKeyDown ("joystick 4 button 1")) {
			buttons [i].onClick.Invoke ();
		}
        */
    }

    bool Up()	{
		if (wasUp && CheckAxis (0, -1)) {
			wasUp = false;
		} else if (!wasUp && CheckAxis (1f, 1)) {
			wasUp = true;
			return true;
		}
        
		return false;
        
	}

	bool Down()	{
        
		if (wasDown && CheckAxis (0,1)) {
			wasDown = false;
		} else if (!wasDown && CheckAxis (-1f, -1)) {
			wasDown = true;
			return true;
		}
        
		return false;
	}

	bool CheckAxis(float value, int comp){
		int a = Input.GetAxis ("Vertical_P1").CompareTo (value);
       // aV_0 = Mathf.Lerp(aV_0, Input.GetAxis("Vertical_P1"), .25f);

        int b = Input.GetAxis ("Vertical_P2").CompareTo (value);
		int c = Input.GetAxis ("Vertical_P3").CompareTo (value);
		int d = Input.GetAxis ("Vertical_P4").CompareTo (value);

		return a == 0 || a == comp || b == 0 || b == comp || c == 0 || c == comp || d == 0 || d == comp;
	}
	

}
