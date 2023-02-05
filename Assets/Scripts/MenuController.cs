using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	public Button btnResume;
	public Button btnStageSelect;
	public Button btnRestart;
	public Button btnControl;
    public Button btnKeyboardView;
    public Button btnEasyAdv;
    public Button btnQuit;
	private Button[] buttons = new Button[4];
	int i = 0;
	private bool wasUp;
	private bool wasDown;
	private float axis;

	void Start () {
		buttons [0] = btnResume;
		buttons [1] = btnControl ;
		buttons [2] = btnRestart;
		buttons [3] = btnQuit ;
		////  buttons[4] = btnKeyboardView;
		//  buttons[5] = btnEasyAdv;
		//   buttons[6] = btnStageSelect;

		btnResume.Select ();


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
		if (wasUp && CheckAxis (0.25f, -1)) {
			wasUp = false;
		} else if (!wasUp && CheckAxis (0.75f, 1)) {
			wasUp = true;
			return true;
		}
		return false;
	}

	bool Down()	{
		if (wasDown && CheckAxis (-0.25f, 1)) {
			wasDown = false;
		} else if (!wasDown && CheckAxis (-0.75f, -1)) {
			wasDown = true;
			return true;
		}
		return false;
	}

	bool CheckAxis(float value, int comp){
		int a = Input.GetAxis ("Vertical_P1").CompareTo (value);
		int b = Input.GetAxis ("Vertical_P2").CompareTo (value);
		int c = Input.GetAxis ("Vertical_P3").CompareTo (value);
		int d = Input.GetAxis ("Vertical_P4").CompareTo (value);

		return a == 0 || a == comp || b == 0 || b == comp || c == 0 || c == comp || d == 0 || d == comp;
	}
	

}
