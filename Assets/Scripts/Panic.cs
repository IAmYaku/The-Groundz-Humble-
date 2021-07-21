using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : AIState {

    AI ai;
    public bool inAction;
    GameObject gameObject;


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

    }


    // Update is called once per frame
    public void Update(GameManager manager, AI ai) {

        CheckPanickDelay();

        if (!inAction) {

            ai.ValuateGameState();

            if (ai.type == AI.Type.random) {

                Action(manager, ai, 2, Vector3.zero);
            }

            if (ai.type == AI.Type.timid)
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
                    if (panicked)
                    {

                        ai.SetState(ai.retreat_);
                    }
                    else
                    {
                        Action(ai.intensity, ai);
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
                        Action(ai.intensity, ai);
                    }
                }
            }
        }
        else
        {
            Action(3, ai);   // intensity number  is faulty
        }
    }

    public void Action(GameManager manager, AI ai, float dur, Vector3 target) 
    {     //TBC
        Update(manager,ai);
        
        // ai.horzInput = Random.Range(-2.0f, 2.0f);
      //  ai.vertInput = Random.Range(-2.0f, 2.0f);
    }

    public void Action(int intensity, AI ai)
    {
        float panickTime = Mathf.Abs(intensity * panick0);

        if (!inAction)   // first time we're here
        {

            // panickTime is global ^^^
            panicked = false;   // shouldn't need due to CheckPanicDelay but fuck it lol
            t0 = Time.realtimeSinceStartup;
            intensity = Mathf.Abs(intensity *2);     // *arb
            sec = 0;
            ai.FaceOpp();

        }

        {

            if (sec < panickTime)
            {
              //  Debug.Log("PANICKING");
                inAction = true;

                ai.FaceOpp();                                          // <-- this may not be working lol; 

                aiLevel = ai.GetLevel();
                aiCatchProb = ai.GetCatchProb();


                if (sec < .1f  || (sec % Mathf.Clamp(.3f - intensity/100f,0.01f,10f)) == 0)
                {
                    ranVelVec = Random.Range(-.1f, .1f) * (intensity / 2.0f);               // aiLevel stuff here
                }
            

                ai.vertInput = Mathf.Lerp(ranVelVec, ai.vertInput, .33f);

              //  Debug.Log(" ai.vertInput =  " + Mathf.Lerp(ranVelVec, ai.vertInput, .33f));
                
                if (CatchProb(aiCatchProb))
                {
                    ai.action1Input = true;
                }

                else
                {
                    ai.action1Input = false;
                }
                
               //Debug.Log("ai.vertInput = "  + ai.vertInput);
               // ai.vertInput = Mathf.Clamp(ai.vertInput, -1.0f, 1.0f);


                float tF = Time.realtimeSinceStartup;
                sec = tF - t0;
            }

            else
            {
             //   Debug.Log("panicked : @ " + sec);
                ai.vertInput = 0.0f;
                ai.action1Input = false;
                panicked = true;
                inAction = false;
                panicked0 = Time.realtimeSinceStartup;
            }
        }
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
        Debug.Log("Returning " + name);
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

    public void SetPanickTime( float t)
    {
        panickDelayTime = t;
    }
}
