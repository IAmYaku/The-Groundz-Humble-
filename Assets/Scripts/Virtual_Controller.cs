using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virtual_Controller : MonoBehaviour
{
    // Start is called before the first frame update
   public AudioSource audioSource;
   AudioClip[] clips;

   public  AudioClip buttonClip0;
  //  AudioClip buttonClip1;




    void Start()
    {
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (!buttonClip0)
        {
            buttonClip0 = Resources.Load<AudioClip>("Audio/Misc/Button 0");

        }

        audioSource.volume = .25f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClip()
    {

        audioSource.clip = buttonClip0;
        audioSource.time = .1f;
        audioSource.Play();

    }

    public void PlayButtonClip(float x)
    {

        audioSource.clip = buttonClip0;
        audioSource.time = .1f;
        audioSource.pitch = x;
        audioSource.Play();

    }

}
