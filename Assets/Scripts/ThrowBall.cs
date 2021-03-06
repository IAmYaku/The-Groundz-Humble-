using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThrowBall : AIState
{

    bool inAction;
    GameObject gameObject;

    AI ai;

    public float vertInput;
    public float horzInput;
    public bool jumpInput;

    public bool action1Input;
    public bool rTriggerInput;
    public bool superInput;
    public bool blockInput;
    public bool ballGrabbed;

    int num = 2;
    string name = "ThrowBall";


    public void Start(GameManager manager, AI ai_)
    {
        ai = ai_;
        gameObject = ai.gameObject;

    }


    public void Update(GameManager manager, AI _ai)
    {
        ai.ValuateGameState();

        if (ai.type == AI.Type.timid)

        {
            if (!inAction)
            {
                if (ai.gameState == AI.GameState.safe)
                {
                    if (ai.ballGrabbed)
                    {
                        Action(manager, ai, 2, Vector3.zero);
                    }
                    else
                    {
                        ai.rTriggerInput = false;
                        ai.SetState(ai.getBall_);
                    }
                }
                if (ai.gameState == AI.GameState.mildly_safe)
                {
                    if (ai.ballGrabbed)
                    {
                        Action(manager, ai, 2, Vector3.zero);
                    }
                    else
                    {
                        ai.rTriggerInput = false;
                        ai.SetState(ai.getBall_);
                    }
                }

                if (ai.gameState == AI.GameState.mild)
                {
                    if (ai.ballGrabbed)
                    {
                        Action(manager, ai, 2, Vector3.zero);
                    }
                    else
                    {
                        ai.rTriggerInput = false;
                        ai.SetState(ai.getBall_);
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

            else                                                                              
            {                                                                            // iffy code
                if (ai.gameState == AI.GameState.safe)
                {
                    if (ai.ballGrabbed)
                    {
                        Action(manager, ai, 1, Vector3.zero);
                    }
                    else
                    {
                        ai.SetState(ai.getBall_);
                    }
                }
                if (ai.gameState == AI.GameState.mildly_safe)
                {
                    if (ai.ballGrabbed)
                    {
                        Action(manager, ai, 1, Vector3.zero);
                    }
                    else
                    {
                        ai.SetState(ai.getBall_);
                    }
                }
                if (ai.gameState == AI.GameState.mild)
                {
                    ai.SetState(ai.idle_);
                }
                if (ai.gameState == AI.GameState.mildly_dangerous)
                {
                    ai.SetState(ai.retreat_);
                }
                if (ai.gameState == AI.GameState.dangerous)
                {
                    ai.SetState(ai.panic_);
                }

            }
        }

    }


    public void Action(GameManager manager, AI _ai, float urgency, Vector3 target)
    {
        ai = _ai;
        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;


        if (urgency == 2)
        {
            inAction = true;
            float proximity = 50 - 10*urgency;
            if (Vector3.Distance(pos, GetNearestOpp(manager, ai)) < proximity)
            {
                FaceOpp();
                ai.rTriggerInput = true;
                inAction = false;
                ai.SetState(ai.idle_);

                Debug.Log("AI Throwing");
            }
            else
            {
                MoveTowardsOpp(manager, ai);
                Debug.Log("Moving to Player");
            }
        }
        if (urgency == 1)
        {

            FaceOpp();
            inAction = true;
            float proximity = 50 - 10 * urgency;
            if (Vector3.Distance(pos, GetNearestOpp(manager, ai)) < proximity)
            {
                FaceOpp();
                ai.rTriggerInput = true;
                inAction = false;
                ai.SetState(ai.idle_);
            }
            else
            {
                MoveTowardsOpp(manager, ai);
            }
        }
        if (urgency == 0)
        {
            FaceOpp();
            inAction = true;
            float proximity = 50 - 10 * urgency;
            if (Vector3.Distance(pos, GetNearestOpp(manager, ai)) < proximity)
            {
                FaceOpp();
                ai.rTriggerInput = true;
                inAction = false;
                ai.SetState(ai.idle_);
            }
            else
            {
                MoveTowardsOpp(manager, ai);
            }
        }
    }

    private void FaceOpp()
    {
        bool isFacingRight = !ai.spriteRenderer.flipX;

       if (ai.playerScript.team == 1 && !isFacingRight)
        {
            ai.SpriteFlip();
        }
       else
        {
            if (ai.playerScript.team == 2 && isFacingRight)
            {
                ai.SpriteFlip();
            }
        }
    }

    private Vector3 GetNearestOpp(GameManager manager,AI _ai )
    {
        LevelManager lm = manager.levelManager;
        ai = _ai;
        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        Vector3 move;
        float min = 10000000f;
        GameObject closestOpp = null;

        if (ai.transform.GetComponentInParent<Player>().team == 1)
        {
            foreach (GameObject opp in lm.tm2.players)
            {
                if (Vector3.Distance(opp.transform.GetChild(0).position,pos) < min)
                {
                    closestOpp = opp;
                }
            }
        }
        else
        {
            foreach (GameObject opp in lm.tm1.players)
            {
                if (Vector3.Distance(opp.transform.GetChild(0).position, pos) < min)
                {
                    closestOpp = opp;
                }
            }
        }
        return closestOpp.transform.GetChild(0).position;
    }
    private void MoveTowardsOpp(GameManager manager, AI _ai)
    {
        ai = _ai;
        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        Vector3 nearestOppPos = GetNearestOpp( manager, ai);
        float aiXVelocity;
        aiXVelocity = ( nearestOppPos.x -pos.x ) * ai.xSpeed;    //  * Mathf.Clamp(ai.navSpeed,1f,12f)
        float aiZVelocity;
        aiZVelocity = (nearestOppPos.z -pos.z) * ai.zSpeed;

        
        ai.SetNavVelocity(new Vector3(aiXVelocity/1000 , 0f, aiZVelocity/1000 ));           // *arbitrary nums

      //  Debug.Log("aiVelocity= " + ai.navMesh.velocity);
        //Debug.Log("aiXVelocity= " + aiXVelocity);
        //Debug.Log("aiZVelocity= " + aiZVelocity);

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
