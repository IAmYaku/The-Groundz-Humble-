using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

	// Use this for initialization
	public VideoPlayer videoPlayer;
	void Start () {
		videoPlayer.Play ();
	}
	
	// Update is called once per frame
	void Update () {
        if (videoPlayer.isPlaying == false)
        {
            Invoke("NextScene", 0f);
        }
        }

	public void NextScene() {
		SceneManager.LoadScene ("GamemodeMenu");

	}
}
