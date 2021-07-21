using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump_P1") ||Input.GetButtonDown("Jump_P2") || Input.GetButtonDown("Jump_P3") || Input.GetButtonDown("Jump_P4")) {
			SceneManager.LoadScene ("GamemodeMenu");
		}	
		if (Input.GetKeyDown ("joystick 1 button 1") || Input.GetKeyDown ("joystick 2 button 1") ||
			Input.GetKeyDown ("joystick 3 button 1") || Input.GetKeyDown ("joystick 4 button 1")) {
			SceneManager.LoadScene ("TheGrounds");
		}

	}
}
