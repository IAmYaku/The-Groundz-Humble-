using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]


public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    private float buffSize = 512;
    public static float[] _samples = new float[512];
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
       // GetOutputData();
    }

    private void GetOutputData()
    {
       
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
   
    }
}
