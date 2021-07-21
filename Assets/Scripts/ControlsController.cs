using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump_P1") ||Input.GetButtonDown("Jump_P2") || Input.GetButtonDown("Jump_P3") || Input.GetButtonDown("Jump_P4")) {
			SceneManager.LoadScene ("GamemodeMenu");
		}
	}
}
