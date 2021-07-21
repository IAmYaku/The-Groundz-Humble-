using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyInput : MonoBehaviour
{
    public float t0;
    public float tF;

    int bufferSize = 2; //frames

    Vector2 muv;
    Vector2 muv0;
    Vector2 muv_delta;
    Vector2[] muvs_delta;

    float deltaMuvAv;
    float deltaMuvAv0;
    public float k = 1f; // weight

    public float muvXceleration;
    bool isFilling;

    int index = 0;


    public JoyInput(int v)
    {
        bufferSize = v;
        muvs_delta = new Vector2[bufferSize];
    }

    void Start()
    {
        muvs_delta = new Vector2[bufferSize];

        print("debug: muvs loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFilling)
        {
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

    internal Vector2 Input(float horizontal, float vertical)
    {
        muv = new Vector2();
        muv.x = horizontal;
        muv.y = vertical;

        muv_delta = muv - muv0;
        fillMuvsDelta(muv_delta);


        //muvXceleration = GetMuvDiff();
       // print("muvXeleration = " + muvXceleration);
     //   deltaMuvAv0 = GetDeltaMuvAv_Di();
        muv0 = muv;

        return muv;                                                      // might not need return
    }

    private void fillMuvsDelta(Vector2 muvX)
    {
        Vector2 prev;
        isFilling = true;

        if (index < bufferSize)                  // buffer not filled yet-
        {

            muvs_delta[index] = muvX;
            index++;
        }

        else
        {
            prev = Vector2.zero;

            for (int i = 0; i < bufferSize; i++)              // scoovher
            {
                Vector2 hold = muvs_delta[i];
                muvs_delta[i] = prev;
                prev = hold;
            }

            muvs_delta[0] = muvX;
        }



    }
     
    private float GetDeltaMuvAv_Di()
    {
        float addMe = 0;
        int d_i = (int)(muvs_delta.Length / (1 + Mathf.Abs(deltaMuvAv0)));


        for (int i = 0; i < d_i; i++)
        {
            addMe += muvs_delta[i].magnitude;
        }

        deltaMuvAv = (addMe / bufferSize);

        return deltaMuvAv;
    }


    private float GetDeltaMuvAv()
    {
        float addMe = 0;
        for (int i = 0; i < muvs_delta.Length; i++)
        {
            addMe += muvs_delta[i].magnitude;
        }
        return (addMe / bufferSize);
    }

    public void PrintMoveAv()
    {
        float printMe = 0;
        print(" MOoovesss");
        for (int i = 0; i < muvs_delta.Length; i++)
        {
            printMe += muvs_delta[i].magnitude;
        }
        print(" move average = " + printMe / bufferSize);

    }

    public void SetMuvsDelta()
    {
        muvs_delta = new Vector2[bufferSize];
    }
    private float GetDeltaMuvAv_k()
    {
        float addMe = 0;
        for (int i = 0; i < muvs_delta.Length; i++)
        {
            addMe += muvs_delta[i].magnitude * (bufferSize / (i + 1) / k);
        }
        return (addMe / bufferSize);
    }

    public Vector2 GetMuvDelta()
    {
        return muv_delta;
    }

}
