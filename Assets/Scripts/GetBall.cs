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


    float counter;
    float t0;
    float tF;

    int num = 1;
    string name = "GetBall";

    GameManager gameManager;

    public void Start(GameManager manager, AI ai_)          // make gameManager
    {
        ai = ai_;
        gameObject = ai.gameObject;
        gameManager = manager;

    }


    public void Update(GameManager manager, AI ai)
    {

        ai.EvaluateGameState();

        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        GameObject nearestBall = GetNearestBall(pos, manager);

        if (ai.type == AI.Type.timid)
        {
            TimidBehavior(nearestBall);
        }

        if (ai.type == AI.Type.aggresive)
        {
            AggressiveBehavior(nearestBall);
        }

        if (ai.type == AI.Type.random)
        {
            RandomBehavior(nearestBall);
        }

        Debug.Log("InAction = " + inAction);
    }

    public void Action(GameManager manager, AI ai, float urgency, Vector3 tar)                  // Make better
    {
     //   Debug.Log("GrabInput Action");

        inAction = true;
        Vector3 pos = ai.navMeshAgent.gameObject.transform.position;
        int count = 0;
        int team = ai.gameObject.GetComponentInParent<Player>().team;

          //  Debug.Log("urgency");
            GameObject nearestBall = GetNearestBall(pos, manager);

            if (nearestBall != null)
            {
              //  Debug.Log("Found Ball");

                if (Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position) <= ai.grabRadius) // 
                {
                    Debug.Log("ActionInput");
                    ai.action1Input = true;
                    ai.EndAgentNavigation();

                    if (ai.ballGrabbed)
                    {
                        inAction = false;
                    }
                }
                else
                {
                    if (ai.navMeshAgent.isStopped || (nearestBall && ai.ballGrabbed))
                    {
                        ai.navMeshAgent.isStopped = false;
                    }

                    // Vector3 move = GetMoveTowards(pos, nearestBall);    // deprecated
                    //    ai.horzInput = move.x;
                    //    ai.vertInput = move.z;

                 //  Debug.Log("Moving towards ball");
                   // Debug.Log("GrabRadius = "+ ai.grabRadius);
                   // Debug.Log("Distance = " + Vector3.Distance(nearestBall.transform.position, ai.navMeshAgent.gameObject.transform.position));

                   // ai.navSpeed
                    ai.SetAgentDestination(nearestBall.transform);
                }
            }

            else
            {
              Debug.Log("No Ball found");
                inAction = false;

            }
        

    }

    #region Behaviors
    void TimidBehavior(GameObject nearestBall)
    {
        if (ai.gameState == AI.GameState.safe)
        {
            if (inAction) { 
                Action(gameManager, ai, 3, Vector3.zero);
        }
            else
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);
                }
                else
                {
                if (ai.GetNearestBall())
                    {
                        Action(gameManager, ai, 3, Vector3.zero);
                    }
                    else
                    {
                        ai.SetState(ai.idle_);
                    }
                }
            }
        }

        if (ai.gameState == AI.GameState.mildly_safe)
        {
            if (inAction)
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }
            else
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);
                }
                if (ai.GetNearestBall())
                {
                    Action(gameManager, ai, 2, Vector3.zero);
                }
                else
                {
                    ai.SetState(ai.idle_);
                }
            }
        
        }

        if (ai.gameState == AI.GameState.mild)
        {
            if (inAction)
            {
                Action(gameManager, ai, 1, Vector3.zero);
            }

            else
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.throwBall_);     // <--- Should try wait code here too
                }
                if (ai.GetNearestBall())
                {
                    Action(gameManager, ai, 1, Vector3.zero);
                }
                else
                {
                    ai.SetState(ai.idle_);
                }
            }
            }

            if (ai.gameState == AI.GameState.mildly_dangerous)
        {
            if (inAction)
            {
                Action(gameManager, ai, 0, Vector3.zero);
            }

            else
            {
                if (ai.ballGrabbed)
                {
                    ai.SetState(ai.retreat_);     // <--- Should try wait code here too
                }
                if (ai.GetNearestBall())
                {
                    Action(gameManager, ai, 0, Vector3.zero); // <--- Should try wait code here too
                }
                else
                {
                    ai.SetState(ai.retreat_);
                }
            }

        }

        if (ai.gameState == AI.GameState.dangerous)
        {
            if (!inAction)
            {
                ai.SetState(ai.panic_);
            }

        }
    }

    void AggressiveBehavior (GameObject nearestBall)
    {
        if (ai.ballGrabbed)
        {
            ai.SetState(ai.throwBall_);
        }
        else
        {
            if (nearestBall)
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }
            else
            {
                ai.SetState(ai.idle_); // or run pattern
            }
            
        }
    }

    void RandomBehavior(GameObject nearestBall)
    {
        float randomInt = UnityEngine.Random.Range(0, 3f);

        if (randomInt < 1 && randomInt > 0)
        {
            if (nearestBall)
            {
                ai.SetState(ai.throwBall_);
            }
            else
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }
        }

        if (randomInt > 1 && randomInt < 2)
        {
            ai.SetState(ai.idle_);
        }

        if (randomInt > 2 && randomInt < 3)
        {
            ai.SetState(ai.panic_);
        }


    }
    #endregion

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
      //  Debug.Log("Returning " + name);
        return name;
    }

    public void SetInAction(bool x)
    {
        inAction = x;
    }
}




/*
 * 
 * if (!ai.ballGrabbed && nearestBall)
        {
            if (ai.gameState == AI.GameState.safe)
            {
                Action(gameManager, ai, 2, Vector3.zero);
            }

            if (ai.gameState == AI.GameState.mildly_safe)
            {
                Action(gameManager, ai, 1, Vector3.zero);
            }

            if (ai.gameState == AI.GameState.mild)
            {
                Action(gameManager, ai, 0, Vector3.zero);
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
 * */