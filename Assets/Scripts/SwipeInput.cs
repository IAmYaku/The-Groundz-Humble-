using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    // Start is called before the first frame update
    public float t0;
    public float tF;

   public static int bufferSize =4; //frames

    Vector2 muv;
    Vector2 muv0;
    Vector2[] moves;
    float deltaMuvAv;
    float deltaMuvAv0;
   public float k = 2.45f; // weight

    public float muvXceleration;
    bool isFilling;

    int index = 0;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      if (!isFilling)
        {
            print("!not filling");
            if (index > 0)
            {
                index--;
            }
           
        }  
    }

    internal float InputX(float horizontal, float accelerationTime)
    {
        muv.x = horizontal;
        return muv.x;

    }

    internal float InputY(float vertical, float accelerationTime)
    {

        muv.y = vertical;
        return muv.y;
    }

    internal Vector2 Input(float horizontal, float vertical, float accelerationTime)
    {
        muv = new Vector2();
        muv.x = horizontal;
        muv.y = vertical;
        fillMoves(muv-muv0);
        muvXceleration = GetDeltaMuvAv_k();
        deltaMuvAv0 = GetDeltaMuvAv_Di();
        muv0 = muv;

        return muv;
    }

    private void fillMoves(Vector2 swipe_v)
    {
        Vector2 prev;
        isFilling = true;

       if (index < bufferSize)
        {
            
            moves[index] = swipe_v;
            index++;
        }
       else
        {
            prev = Vector2.zero;

        for (int i =0; i<bufferSize; i++)
           {
                Vector2 hold = moves[i];
                moves[i] = prev;
                prev = hold;
            }
            moves[0] = swipe_v;
        }

      
        
    }

    private float GetDeltaMuvAv_Di()
    {
        float addMe = 0;
        int d_i = (int)(moves.Length/(1+Mathf.Abs(deltaMuvAv0)));


        for (int i = 0; i < d_i; i++)
        {
            addMe += moves[i].magnitude;
        }

        deltaMuvAv = (addMe / bufferSize);

        return deltaMuvAv;
    }


    private float GetDeltaMuvAv()
    {
        float addMe = 0;
        for (int i = 0; i < moves.Length; i++)
        {
            addMe += moves[i].magnitude;
        }
        return (addMe / bufferSize);
    }

    private float GetDeltaMuvAv_k()
    {
        float addMe = 0;
        for (int i = 0; i < moves.Length; i++)
        {
            addMe += moves[i].magnitude *(bufferSize/(i+1)/k);
        }
        return (addMe / bufferSize);
    }

    public void PrintMoveAv()
    {
        float printMe = 0;
        print(" MOoovesss");
       for (int i=0; i< moves.Length; i++)
        {
            printMe += moves[i].magnitude;
        }
        print(" move average = " + printMe/bufferSize);

    }

    public void SetMoves()
    {
        moves = new Vector2[bufferSize];
    }

    internal void SetMoves(int bufferSize)
    {
        moves = new Vector2[bufferSize];
    }
}
