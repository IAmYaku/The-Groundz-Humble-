using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Back : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {


	}

    public void BackToMenu()
    {
        SceneManager.LoadScene("GamemodeMenu");
    }

}
