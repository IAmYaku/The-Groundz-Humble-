using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : AIState {

    bool inAction;
    GameObject gameObject;
    AI ai;

    int num = 5;
    string name = "Retreat";

    public void Start(GameManager manager, AI ai_)
    {
        ai = ai_;
        gameObject = ai.gameObject;

    }

    public void Update(GameManager manager, AI ai)
    {

        ai.ValuateGameState();

        if (!inAction)
        {

            if (ai.type == AI.Type.timid)
            {
                if (ai.IsAtRetreatPoint())
                {

                    if (ai.gameState == AI.GameState.safe)
                    {
                        if (!ai.ballGrabbed)
                        {
                            ai.SetState(ai.getBall_);
                        }
                        else
                        {
                            ai.SetState(ai.throwBall_);
                        }
                    }
                    if (ai.gameState == AI.GameState.mildly_safe)
                    {
                        if (!ai.ballGrabbed)
                        {
                            ai.SetState(ai.getBall_);
                        }
                        else
                        {
                            ai.SetState(ai.throwBall_);
                        }
                    }
                    if (ai.gameState == AI.GameState.mild)
                    {
                        ai.SetState(ai.idle_);
                    }

                    if (ai.gameState == AI.GameState.mildly_dangerous)
                    {
                        ai.SetState(ai.idle_);
                    }
                    if (ai.gameState == AI.GameState.dangerous)
                    {
                        ai.SetState(ai.idle_);
                    }
                }

                else
                {
                    Action(manager, ai, 2, Vector3.zero);
                }
            }
        }

    }

    public void Action(GameManager manager, AI ai, float intensity, Vector3 target)
    {
        if (!ai.IsAtRetreatPoint())
        {
            if (ai.navMeshAgent.isStopped)
            {
                ai.navMeshAgent.isStopped = false;
            }

            float sec = intensity;
            float t0 = Time.realtimeSinceStartup;
            int team = ai.gameObject.GetComponentInParent<Player>().team;

         //   Debug.Log("retreat point = " + ai.retreatPoint.position);
            ai.SetAgentDestination(ai.retreatPoint);
        }

        else
        {
            ai.FaceOpp();  //please an dthank you lol

            inAction = false;
            ai.EndAgentNavigation();
        //    Debug.Log("At retreat point ");
        }


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
