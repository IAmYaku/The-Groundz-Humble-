using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : AIState {

    AI ai;
    public bool inAction;
    GameObject gameObject;

    GameManager gameManager;

    public float vertInput;
    public float horzInput;
    public bool jumpInput;

    public bool action1Input;
    public bool rTriggerInput;
    public bool superInput;
    public bool blockInput;


    float sec = 0;
    float t0 = 0;

    public bool panicked;

    float panickTime;

    float panick0 = .6f;
       
    float panickDelayTime;
    float panickDelay;

    float panicked0;

    float aiCatchProb =.25f;
    int aiLevel;

    int num = 4;
    string name = "Panic";     // Rule number one, DONT PANIC lol


    float ranVelVec;

    public void Start(GameManager manager, AI ai_)
    {
        ai = ai_;
        gameObject = ai.gameObject;
        panickDelayTime = ai.GetPanickDelayTime();
        gameManager = manager;

    }


    // Update is called once per frame
    public void Update(GameManager manager, AI ai) {

        CheckPanickDelay();

        ai.EvaluateGameState();

        if (ai.type == AI.Type.timid)
        {
            TimidBehavior();
        }

        if (ai.type == AI.Type.aggresive)
        {
            AggressiveBehavior();
        }

        if (ai.type == AI.Type.random)
        {
            RandomBehavior();
        }

      
   
    }

    public void Action(GameManager manager, AI ai, float dur, Vector3 target) 
    {     //TBC
        Update(manager,ai);
        
        // ai.horzInput = Random.Range(-2.0f, 2.0f);
      //  ai.vertInput = Random.Range(-2.0f, 2.0f);
    }

    public void Action(float intensity, AI ai)
    {
        if ( panickTime == 0.0f)
        {
            panickTime = Mathf.Abs(intensity * panick0);
            panicked = false;
        }
        

            if (sec < panickTime)
            {
              //  Debug.Log("PANICKING");
                inAction = true;

                ai.FaceOpp();                                          // <-- this may not be working lol; 

                aiLevel = ai.GetLevel();

                aiCatchProb = ai.GetCatchProb();



            if (float.Parse(sec.ToString("F1")) % .5f == 0.0f ) 
                {
                    ranVelVec = Random.Range(-.1f, .1f) * (intensity / 10.0f);               // aiLevel stuff here
                }
            

                ai.vertInput = Mathf.Lerp(ranVelVec, ai.vertInput, .25f);

              //  Debug.Log(" ai.vertInput =  " + Mathf.Lerp(ranVelVec, ai.vertInput, .33f));
                

                if (!ai.ballGrabbed)
                {
                    if (CatchProb(aiCatchProb))
                    {
                        ai.action1Input = true;
                    }

                    else
                    {
                        ai.action1Input = false;
                    }
                }

                if (DodgeProb() )
            {
                ai.jumpInput = true;
            }

            //Debug.Log("ai.vertInput = "  + ai.vertInput);
            // ai.vertInput = Mathf.Clamp(ai.vertInput, -1.0f, 1.0f);

            sec += Time.deltaTime;
            }

            else
            {
             //   Debug.Log("panicked : @ " + sec);
                ai.vertInput = 0.0f;
                panickTime = 0.0f;
                sec = 0.0f;
                ai.action1Input = false;
                panicked = true;
                inAction = false;
                panicked0 = Time.realtimeSinceStartup;
            }
    }

    void TimidBehavior()
    {
        {
            if (ai.gameState == AI.GameState.safe)
            {
                if (inAction)
                {

                }
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
                if (panicked)
                {

                    ai.SetState(ai.retreat_);
                }
                else
                {
                    Action(ai.intensity/2f, ai);
                }
            }
            if (ai.gameState == AI.GameState.dangerous)
            {
                if (panicked)
                {

                    ai.SetState(ai.retreat_);
                }
                else
                {
                    Action(ai.intensity/2f, ai);
                }
            }
        }
    }

    void AggressiveBehavior()
    {

    }


    void RandomBehavior()
    {

    }

    void CheckPanickDelay()
    {

       if (panicked)
        {
            float panickedTf = Time.realtimeSinceStartup;
            panickDelay = panickedTf - panicked0;

            panickDelayTime = ai.GetPanickDelayTime();

            if (panickDelay >= panickDelayTime)
            {
              //  Debug.Log("panickDelay = " + panickDelay);

                panicked = false;
                panickDelay = 0;   
                
            }
        }
    }

    int AIState.GetNum()
    {
        return num;
    }
    string AIState.GetName()
    {
       // Debug.Log("Returning " + name);
        return name;
    }

    bool CatchProb( float prob)
    {
        float ran = UnityEngine.Random.Range(0.0f, 1.0f);

         if (ran < prob) {

            return true;
        }
         else
        {
            return false;
        }
    }

    bool DodgeProb()
    {
        float ran = Random.Range(0, 1);
        if (ran < ai.GetLevel() * .25f)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPanickTime( float t)
    {
        panickDelayTime = t;
    }

    public void SetInAction(bool x)
    {
        inAction = x;
    }
}
