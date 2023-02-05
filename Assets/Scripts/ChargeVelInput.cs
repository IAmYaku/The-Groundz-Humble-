using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeVelInput : MonoBehaviour
{
    public float t0;
    public float tF;

    int bufferSize = 2; //frames

    Vector2 muv;
    Vector2 muv0;
    Vector2 muv_delta;
    List<Vector2> muvs;

    float deltaMuvAv;
    float deltaMuvAv0;
    public float k = 1f; // weight

    public float muvXceleration;
    bool isFilling;

    int index = 0;


    public ChargeVelInput()
    {
        muvs = new List<Vector2>();

    }

    void Start()
    {
       
    }

    void Update()
    {
        
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

    internal void Input(float horizontal, float vertical)
    {
        muv = new Vector2();
        muv.x = horizontal;
        muv.y = vertical;

        muvs.Add(muv);
                                                  
    }
  

    public Vector2 GetMuvAverage()
    {
        Vector2 addMe = Vector2.zero;

        for (int i = 0; i < muvs.Count; i++)
        {
            addMe += muvs[i];
        }
        return (addMe / muvs.Count);
    }

    public Vector2 GetWeightedMuvAverage()
    {
        Vector2 addMe = Vector2.zero;

        for (int i = muvs.Count-1; i>=0; i--)
        {
            addMe += muvs[i]/(i+1);
        }
        return (addMe / muvs.Count);
    }

    public void PrintMoveAv()
    {
        float printMe = 0;
        print(" MOoovesss");
        for (int i = 0; i < muvs.Count; i++)
        {
            printMe += muvs[i].magnitude;
        }
        print(" move average = " + printMe / bufferSize);

    }



    public List<Vector2> GetMuvs()
    {
        return muvs;
    }

    public void ClearMuvs()
    {
        muvs.Clear();
    }

}
