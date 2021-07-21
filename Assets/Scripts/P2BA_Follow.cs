using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2BA_Follow : MonoBehaviour
{
    //  public Transform ball;

    Quaternion rotation;
    void Awake()
    {
        rotation = Quaternion.Euler(-90f, 0, 0);
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
        transform.position = new Vector3(transform.parent.position.x, 0, transform.parent.position.z);

    }
}