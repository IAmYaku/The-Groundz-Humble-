using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : AIState
{
    AI ai;
    GameObject gameObject;
    AI.Type type;
    bool inAction;
    float tF;
    float t0;
    float idle_time;

    int num = 6;
    string name = "Shake";


    public void Start(GameManager manager, AI ai_)
    {
        ai = ai_;
        gameObject = ai.gameObject;

    }
    public void Update(GameManager manager, AI ai)
    {
    }

    public void Action(GameManager manager, AI ai, float dur, Vector3 target)
    {
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