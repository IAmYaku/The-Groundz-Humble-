using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBall : MonoBehaviour {

    // Use this for initialization
    // NUHandSize
    // Ball Size
    // Ball Thrown
    // Button Pressed
   
    public AudioClip initAudioClip;
    public AudioClip hitAudioClip;
    public AudioClip releaseAudioClip;
    public PhysicMaterial superPhysicMaterial;
    public float mass;
    public int type;
    public float superTime;
    public float superMagnetism = 0f;
    public float damage = 60f;
    public float throwPowerMult = 2f;

    void Start () {
        if (type == 2)
        {
            superMagnetism = 100f;
        }
	}
	
	// Update is called once per frame
	void Update () {
           
	}
}
