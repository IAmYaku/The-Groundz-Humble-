using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        /*
        if (!(ballHit.GetComponent<TrailRenderer>().startWidth == 2))     // multiplier based
        {
            if (ballHit.GetComponent<TrailRenderer>().enabled == false)
            {
                ballHit.GetComponent<TrailRenderer>().enabled = true;
            }
            else
            {
                ballHit.GetComponent<TrailRenderer>().startWidth = 2f;
            }
        }
        */
        /*

        ParticleSystem ball_hit_ps = ballHit.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();  // ball fx stuff, to remove

        ball_hit_ps.GetComponent<Renderer>().sortingOrder = 3; ballHit = collision.gameObject;   // ball fx stuff, to remove

        ball_hit_ps.startSize = ballVelocity; ballHit = collision.gameObject; // ball fx stuff, to remove

        */



        /*
            ParticleSystem.MainModule sup_main_ps = collision.gameObject.GetComponentInChildren<ParticleSystem>().main;
                            sup_main_ps.simulationSpeed = 20f;
                            sup_main_ps.startSizeX = 10f;
                            sup_main_ps.startSizeMultiplier = 10f;

                        levelManager.CamGlitch(ballVelocity);    // super fx stuff, to remove
        */


    }
}
