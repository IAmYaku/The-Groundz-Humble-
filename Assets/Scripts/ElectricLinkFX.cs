using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricLinkFX : MonoBehaviour
{
   public List<Transform> positions;
    public List<VisualEffect> visualEffects;
    bool isLinked;

    GameObject ball;
    GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLinked)
        {

            if (!ball.GetComponent<Ball>().grounded)
            {
                positions[0].transform.position = ball.transform.position;

                positions[1].transform.position = ( ball.transform.position); //lerp

                positions[2].transform.position = (player.transform.position); //lerp

                positions[3].transform.position = player.transform.position;
            }
            else
            {
                Destroy(this.gameObject);

            }

        }
    }

    internal void SetLink(GameObject ball_in, GameObject hittee)
    {
       // if (positions.Count==4)
        {

            isLinked = true;
            ball = ball_in;
            player = hittee;

        }
    }
}
