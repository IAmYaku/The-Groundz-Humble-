using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    // Use this for initialization
    public AudioSource backSoundASource;
    public AudioSource backMusicASource;

    public bool shuffle;
    public AudioClip[] clips;
    int index = 0;

    void Start()
    {
        if (!backMusicASource || !backSoundASource)
        {
            backMusicASource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
            backSoundASource = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        }

     //   clips = Resources.LoadAll<AudioClip>("Audio/Music");

        if (shuffle)
        {
            backMusicASource.clip = clips[Random.Range(0, clips.Length - 1)];
            backMusicASource.Play();
        }

        else
        {
            if (backMusicASource.clip)
            {
                backMusicASource.Play();
            }
            else
            {
                backMusicASource.clip = clips[0];
                backMusicASource.Play();

            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		if (!backMusicASource.isPlaying) {
            if (shuffle)
            {
                backMusicASource.clip = clips[Random.Range(0, clips.Length - 1)];
                backMusicASource.Play();
            }
            else
            {
                index = (index + 1) % clips.Length;
                backMusicASource.clip = clips[index];
                backMusicASource.Play();
            }
			
		}
	
	}
}
