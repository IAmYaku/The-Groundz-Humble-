using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeVelInput : MonoBehaviour
{
    public float t0;
    public float tF;

    int bufferSize = 2; //frames

    Vector2 muv;

    List<Vector2> muvs;
    List<Vector2> velocities;

    public float k = 1f; // weight

    public float muvXceleration;



    public ChargeVelInput()
    {
        muvs = new List<Vector2>();
        velocities = new List<Vector2>();
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
        Vector2 velocityInput = new Vector2();
        velocityInput.x = horizontal;
        velocityInput.y = vertical;

        velocities.Add(velocityInput);
                                                  
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

    public Vector2 GetWeightedVelAverage()
    {
        Vector2 addMe = Vector2.zero;

        for (int i = velocities.Count-1; i>=0; i--)
        {
            // addMe += velocities[i]/(i+1);
            addMe += velocities[i];

        }
        return (addMe / velocities.Count);
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

    public void ClearVelocities()
    {
        muvs.Clear();
        velocities.Clear();
    }

}
