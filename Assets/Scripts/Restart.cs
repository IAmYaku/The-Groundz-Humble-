﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()

    {
        SceneManager.LoadScene("TheGrounds");
	}
}
