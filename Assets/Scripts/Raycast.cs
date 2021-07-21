using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject display;
    ParticleSystem.MainModule psm;
    float pSize;
    float TargetDistance;
    float rayDistance = 20f;
    public bool isOnline;

    void Start() {
        if (!display)
        {
            display = GameObject.Find("Trigger Ready Display");
            if (display)
            {
               // psm = display.GetComponent<ParticleSystem>().main;
               // pSize = psm.startSizeMultiplier;
              //  pSize = 0;
            }
           
        }
      
        

    }


    // Update is called once per frame
    void Update()
    {
        if (false)          //temp
        {

            if (player.GetComponent<Player>().hasJoystick)
            {
                if (player.GetComponent<Player>().joystick.trainingWheels == true)
                {
                    RaycastHit hit;
                    Transform spriteTrans = player.transform.GetChild(0).transform;
                    Vector3 playerVel = spriteTrans.gameObject.GetComponent<Rigidbody>().velocity;
                    Debug.DrawRay(spriteTrans.position, playerVel * rayDistance, Color.blue);
                    if (Physics.Raycast(spriteTrans.position, playerVel, out hit))
                    {
                        if (hit.transform.gameObject.tag == "Player Sprite")
                        {
                            isOnline = true;
                            Debug.DrawRay(spriteTrans.position, playerVel * hit.distance, Color.red);
                            if (player.transform.GetChild(0).gameObject.GetComponent<Controller3D>().ballGrabbed)
                            {
                                if (display)
                                {
                                    //  if (psm.startSizeMultiplier < 2) 
                                    //   psm.startSizeMultiplier += 1f;
                                }
                            }

                        }
                        else
                        {
                            isOnline = false;
                            if (display)
                            {
                                //   psm.startSizeMultiplier -= .1f;
                            }

                        }
                    }
                }
            }
        }
    }
}



