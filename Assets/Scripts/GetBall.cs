using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetBall : AIState {

    // Use this for initialization
    bool inAction;

    AI ai;
    GameObject gameObject;


	public float vertInput;
	public float horzInput;
	public bool jumpInput;

	public bool action1Input;
	public bool rTriggerInput;
	public bool superInput;
	public bool blockInput;

	public bool ballGrabbed;

    float counter;
    float t0;
    float tF;

    int num = 1;
    string name = "GetBall";

    public void Start(GameManager manager, AI ai_)          // make gameManager
    {
        ai = ai_;
        gameObject = ai.gameObject;

    }


    public void Update(GameManager manager, AI ai)
    {

        ai.ValuateGameState();

        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        GameObject nearestBall = GetNearestBall(pos, manager);

        if (ai.type == AI.Type.timid)
        {

            if (!ai.ballGrabbed && nearestBall)
            {
                if (ai.gameState == AI.GameState.safe)
                {
                    Action(manager, ai, 2, Vector3.zero);
                }

                if (ai.gameState == AI.GameState.mildly_safe)
                {
                    Action(manager, ai, 1, Vector3.zero);
                }

                if (ai.gameState == AI.GameState.mild)
                {
                    Action(manager, ai, 0, Vector3.zero);
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

            else

            {
                if (ai.ballGrabbed)
                {

                    if (ai.gameState == AI.GameState.safe)
                    {
                        ai.SetState(ai.throwBall_);
                    }

                    if (ai.gameState == AI.GameState.mildly_safe)
                    {
                        ai.SetState(ai.throwBall_);
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

                else
                {
                    if (!nearestBall)
                    {
                        if (ai.gameState == AI.GameState.safe)
                        {
                           ai.SetState(ai.idle_);
                        }

                        if (ai.gameState == AI.GameState.mildly_safe)
                        {
                            ai.SetState(ai.idle_);
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
        }
    }

    public void Action(GameManager manager, AI ai, float urgency, Vector3 tar)                  // Make better
    {
        

     //   Debug.Log("GrabInput Action");

        inAction = true;
        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        int count = 0;
        int team = ai.gameObject.GetComponentInParent<Player>().team;

        if (urgency == 2)
        {
          //  Debug.Log("urgency 2");
            GameObject nearestBall = GetNearestBall(pos, manager);

            if (nearestBall != null)
            {
              //  Debug.Log("Found Ball");


                if (Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position) <= ai.grabRadius) // 
                {
                    Debug.Log("ActionInput");
                    ai.action1Input = true;
                    ai.EndAgentNavigation();
                }
                else
                {
                    if (ai.navMeshAgent.isStopped)
                    {
                        ai.navMeshAgent.isStopped = false;
                    }

                    // Vector3 move = GetMoveTowards(pos, nearestBall);    // deprecated
                    //    ai.horzInput = move.x;
                    //    ai.vertInput = move.z;

                 //  Debug.Log("Moving towards ball");
                   // Debug.Log("GrabRadius = "+ ai.grabRadius);
                   // Debug.Log("Distance = " + Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position));


                    ai.SetAgentDestination(nearestBall.transform);
                }
            }

            else
            {
              Debug.Log("No Ball found");
                inAction = false;

            }
        }
        if (urgency == 1)
        {
         //   Debug.Log("urgency 1");

            GameObject nearestBall = GetNearestBall(pos, manager);

            if (nearestBall != null)
            {
            //   Debug.Log("Found Ball");

                if (Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position) < ai.grabRadius)
                {
                    Debug.Log("ActionInput");
                    ai.action1Input = true;
                    ai.EndAgentNavigation();
                }
                else
                {
                    if (ai.navMeshAgent.isStopped)
                    {
                        ai.navMeshAgent.isStopped = false;
                    }
                    //     Vector3 move = GetMoveTowards(pos, nearestBall);
                    //   ai.horzInput = move.x;
                    //    ai.vertInput = move.z;

                  //  Debug.Log("Moving towards ball");
                   // Debug.Log("GrabRadius = " + ai.grabRadius);
                   // Debug.Log("Distance = " + Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position));

                    ai.SetAgentDestination(nearestBall.transform);
                }
            }
            else
            {
             Debug.Log("No Ball found");
                inAction = false;
                ai.DoIdle();


            }
        }
        if (urgency == 0)
        {
         //   Debug.Log("urgency 0");

            GameObject nearestBall = GetNearestBall(pos, manager);

            if (nearestBall != null)
            {
             //  Debug.Log("Found Ball");
                if (Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position) < ai.grabRadius)
                {
                    Debug.Log("ActionInput");
                    ai.action1Input = true;
                    ai.EndAgentNavigation();

                }
                else
                {
                    if (ai.navMeshAgent.isStopped)
                    {
                        ai.navMeshAgent.isStopped = false;
                    }


                    //     Vector3 move = GetMoveTowards(pos, nearestBall);
                    //   ai.horzInput = move.x;
                    //  ai.vertInput = move.z;

                  // Debug.Log("Moving towards ball");
                 //   Debug.Log("GrabRadius = " + ai.grabRadius);
                  //  Debug.Log("Distance = " + Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position));


                    ai.SetAgentDestination(nearestBall.transform);

                }
            }

            else
            {
               Debug.Log("No Ball found");
                inAction = false;
                ai.DoIdle();


            }
        }

    }

	GameObject GetNearestBall( Vector3 pos, GameManager manager){


		float min = 10000000f;
		GameObject nearestBall =null;
        int team = gameObject.GetComponentInParent<Player>().team;
        float halfCourt = manager.levelManager.stage.halfCourtLine;

        LevelManager lm = manager.levelManager;

       // Debug.Log("team :" + team);

        if (team == 1) {
            foreach (GameObject ball in lm.balls) {
                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript.grounded && !ballScript.grabbed && ball.transform.position.x <= halfCourt -.5) {                 // gameRule config
                    if (Vector3.Distance(pos, ball.transform.position) < min) {
                            if (!ballScript.isSupering)
                            {
                                min = Vector3.Distance(pos, ball.transform.position);
                                nearestBall = ball;
                                return nearestBall;
                            }
                    }
                }
            }
        }
        if (team == 2)
        {
            foreach (GameObject ball in lm.balls)
            {
                Ball ballScript = ball.GetComponent<Ball>();

                    if (ballScript.grounded && !ballScript.grabbed   && ball.transform.position.x >= halfCourt + .5)               // gameRule config
                {
                        if (Vector3.Distance(pos, ball.transform.position) < min)
                        {
                        if (!ballScript.isSupering)
                        { 
                            min = Vector3.Distance(pos, ball.transform.position);
                            nearestBall = ball;
                            return nearestBall;
                        } 
                    }
                 }
            }
        }

       // Debug.Log(" null balls");
        return nearestBall;
	}

	Vector3 GetMoveTowards( Vector3 pos, GameObject target){
		Vector3 move = new Vector3 ();
		move.x = (target.transform.position.x - pos.x)/15;
		move.z = (target.transform.position.z - pos.z)/15;
		return move;
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
