using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public Material material;
    public float[] samples = new float[512];
    private int bin;
    public float sample;
    public float sampleAv;
    private float lastSampleAv;

    public float band1;
    public float band2;
    public float band3;
    public float band4;
    public float band5;
    public float band6;
    public float band7;
    public float band8;

    public float rMult = 10f;
    public float bMult = 10f;
    public float gMult = 10f;


    public float offScale =10f;
    public float animScale = 5f;


    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        samples = AudioPeer._samples;
        ScaleAndAverageAudio();
        SetMaterial();
    }

    private void SetMaterial()
    {
        material.SetFloat("_OffsetSize", sampleAv * offScale);
        //material.SetFloat("_AnimationSpeed", 10f); ;
        Color colour = new Color(Mathf.Clamp((band4 + band5 + band6) * rMult, 0, 1) , Mathf.Clamp((band1+band1+band3 )* gMult,0,1), Mathf.Clamp(sampleAv*bMult,0,1));
        material.SetColor("_Color", colour);
        material.SetFloat("_Rand", UnityEngine.Random.Range(-1f, 1f));
        material.SetFloatArray("_FreqBins", samples);
        material.SetFloat("_SampleAv", sampleAv);




    }

    private void ScaleAndAverageAudio()
    {
        sampleAv = 0;
        band1 = 0;
        band2 = 0;
        band3 = 0;
        band4 = 0;
        band5 = 0;
        band6 = 0;
        band7 = 0;
        band8 = 0;



        for (int i = 0; i < 512; i++)
        {
            bin = i;
            sample = samples[i];
            //print("FreqBin " + material.GetVector("_FreqBins"));
            sampleAv += sample;

            if (i >= 0 && i < 64)
            {
                band1 += sample;
            }

            if (i >= 64 && i < 128)
            {
                band2 += sample;
            }
            if (i >= 128 && i < 192)
            {
                band3 += sample;
            }
            if (i >= 192 && i < 256)
            {
                band4 += sample;
            }
            if (i >= 256 && i < 320)
            {
                band5 += sample;
            }
            if (i >= 320 && i < 384)
            {
                band6 += sample;
            }
            if (i >= 384 && i < 448)
            {
                band7 += sample;
            }
            if (i >= 448 && i < 512)
            {
                band8 += sample;
            }

        }

        sampleAv /= samples.Length;
        sampleAv *= 100;
        sampleAv = Mathf.Clamp(sampleAv, 0f, 1f);
        material.SetFloat("_SampleAv", sampleAv);
        lastSampleAv = sampleAv;
       // print("Sample Ave = " + sampleAv);

        band1 /= 64;
        //  print("Band1 = " + band1);
        band2 /= 64;
        // print("Band2 = " + band2);
        band3 /= 64;
        // print("Band3 = " + band3);
        band4 /= 64;
        // print("Band4 = " + band4);
        band5 /= 64;
        //  print("Band5 = " + band5);
        band6 /= 64;
        // print("Band6 = " + band6);
        band7 /= 64;
        // print("Band7 = " + band7);
        band8 /= 64;
        // print("Band8 = " + band8);

    }
}
