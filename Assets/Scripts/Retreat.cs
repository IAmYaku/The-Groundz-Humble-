﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : AIState {

    bool inAction;
    GameObject gameObject;
    AI ai;

    int num = 5;
    string name = "Retreat";

    GameManager gameManager;

    public void Start(GameManager manager, AI ai_)
    {
        ai = ai_;
        gameObject = ai.gameObject;
        gameManager = manager;

    }

    public void Update(GameManager manager, AI ai)
    {

        ai.EvaluateGameState();

        if (ai.gameState == AI.GameState.safe)
        {
            if (!inAction)
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);
                }
                else
                {
                    if (ai.GetNearestBall())
                    {
                        ai.SetState(ai.getBall_);
                    }
                    else
                    {
                        ai.SetState(ai.idle_);
                    }
                }
            }
            else
            {
                Action(gameManager, ai, 3, Vector3.zero);
            }
        }

        if (ai.gameState == AI.GameState.mildly_safe)
        {
            if (!inAction)
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);
                }
                else
                {
                    if (ai.GetNearestBall())
                    {
                        ai.SetState(ai.getBall_);
                    }
                    else
                    {
                        ai.SetState(ai.idle_);
                    }
                }
            }
            else
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }
        }

        if (ai.gameState == AI.GameState.mild)
        {
            if (!inAction)
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);
                }
                else
                {
                    if (ai.GetNearestBall())
                    {
                        ai.SetState(ai.getBall_);
                    }
                    else
                    {
                        ai.SetState(ai.idle_);
                    }
                }
            }
            else
            {
                Action(gameManager, ai, 3, Vector3.zero);
            }
        }

        if (ai.gameState == AI.GameState.mildly_dangerous)
        {
            if (!inAction)
            {
                if (ai.ballGrabbed)
                {
                    Action(gameManager, ai, 3, Vector3.zero);
                }
                else
                {
                    if (ai.GetNearestBall())
                    {
                        Action(gameManager, ai, 3, Vector3.zero);
                    }
                    else
                    {
                        ai.SetState(ai.panic_);
                    }
                }
            }
            else
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }
        }

        if (ai.gameState == AI.GameState.dangerous)
        {
            if (inAction)
            {
                Action(manager, ai, 0, Vector3.zero);  // <--- Should try wait code here too
            }
            else 
            {
                ai.SetState(ai.panic_);
            }

        }

        Debug.Log("InAction = " + inAction);

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

          Debug.Log("retreat point = " + ai.retreatPoint.position);
            ai.SetAgentDestination(ai.retreatPoint);
        }

        else
        {
            ai.FaceOpp();  //please an dthank you lol

            inAction = false;
            ai.EndAgentNavigation();
            Debug.Log("At retreat point ");
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
    public void SetInAction(bool x)
    {
        inAction = x;
    }

}
