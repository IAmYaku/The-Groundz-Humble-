using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBallExpansion : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetSize;

    void Start()
    {
        targetSize = new Vector3(2f, 2f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.localScale.magnitude < targetSize.magnitude)
        {
            float x = transform.parent.localScale.x;
            float y = transform.parent.localScale.y;
            float z = transform.parent.localScale.z;
            transform.parent.localScale += new Vector3(1f, 1f, 1f);
        }
    }
}
