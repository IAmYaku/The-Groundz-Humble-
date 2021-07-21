using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AIState {

    AI ai;
	GameObject gameObject;
	AI.Type type;
    bool inAction;
    float tF;
    float t0;
    float idle_time;

    int num = 0;
    string name = "Idle";


    public void Start(GameManager manager, AI ai_){
        ai = ai_;
        gameObject = ai.gameObject;

	}


    public void Update(GameManager manager, AI ai)     
    {
        ai.ValuateGameState();

        Action(manager, ai, 3, Vector3.zero);

        if (!inAction)
        {

            if (ai.type == AI.Type.timid)
            {
                if (ai.gameState == AI.GameState.safe)
                {
                    if (ai.ballGrabbed)
                    {
                        ai.SetState(ai.throwBall_);
                    }
                    else {
                        ai.SetState(ai.getBall_);
                    }
                    
                }

                if (ai.gameState == AI.GameState.mildly_safe)
                {

                    if (ai.ballGrabbed)
                    {
                        ai.SetState(ai.throwBall_);
                    }
                    else
                    {
                        ai.SetState(ai.getBall_);
                    }
                }

                if (ai.gameState == AI.GameState.mild)
                {
                    Action(manager, ai, 3, Vector3.zero);

                    if (ai.ballGrabbed)
                    {
                        ai.SetState(ai.throwBall_);     //   <-- Here we can introduce a coin flip to randomize throwing or idling.. (and can use in other places as well)
                    }
                    else
                    {
                        //ready
                        ai.SetState(ai.getBall_);     // <-- same here between get ball and ready... (weights can be applied based on difficulty)
                    }
                }

                if (ai.gameState == AI.GameState.mildly_dangerous)
                {
                    ai.SetState(ai.panic_);     
                }

                if (ai.gameState == AI.GameState.dangerous)
                {
                    ai.SetState(ai.panic_);
                }
            }
        }
    }

	public void Action(GameManager manager, AI ai, float dur, Vector3 target){

        //Debug.Log("Idling");
        CheckFace();
        inAction  = true;
		ai.vertInput = 0f;
		ai.horzInput = 0f;
        ai.navMeshAgent.isStopped = true;
        dur = 2;

        if (t0 == 0)    // first time catch
        {
            t0 = Time.realtimeSinceStartup;
        }

        if (!inAction)
        {
          
            t0 = Time.realtimeSinceStartup;
            idle_time = 0.0f;
        }

        else
        {
             tF = Time.realtimeSinceStartup;
             idle_time = tF - t0;
               
            if (idle_time >= dur)
            {
                ai.navMeshAgent.isStopped = false;
                inAction = false;
                t0 = 0;
            }
        }
	}

    private void CheckFace()
    {
        ai.FaceOpp();
        
    }


    int AIState.GetNum()
    {
        return num;
    }

    string AIState.GetName()
    {
        Debug.Log("Returning " + name);
        return name;
 
    }
}
