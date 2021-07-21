using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour {

	// Use this for initialization
	public float farSideLine; // +z
	public float nearSideLine; // -z
	public float halfCourtLine; // +x , 0
	public float baseLineLeft; //-x
	public float baseLineRight; // +x
    public float floor;
    public float roof;

    public GameObject BottomPlane;
    public GameObject FrontPlane;
    public GameObject BackPlane;
    public GameObject LeftPlane;
    public GameObject RightPlane;
    public GameObject TopPlane;

    public GameObject[] walls = new GameObject[4];

    public GameObject halfCourtBox;


    public GameObject playingLevelPlane;

	void Start () {


        if (!playingLevelPlane || !BottomPlane || !LeftPlane || !RightPlane || !TopPlane || !FrontPlane || !BackPlane)
        {

            BottomPlane = GameObject.Find("Bottom Plane");
            LeftPlane = GameObject.Find("Left Plane");
            RightPlane = GameObject.Find("Right Plane");
            TopPlane = GameObject.Find("Top Plane");
            FrontPlane = GameObject.Find("Front Plane");
            BackPlane = GameObject.Find("Back Plane");
            playingLevelPlane = GameObject.FindGameObjectWithTag("Playing Level");


        }

         walls[0] = FrontPlane;
         walls[1] = BackPlane;
         walls[2] = LeftPlane;
         walls[3] = RightPlane;


         Bounds playingLevelBounds = playingLevelPlane.GetComponent<BoxCollider>().bounds;

        farSideLine = BackPlane.transform.position.z;
		nearSideLine = FrontPlane.transform.position.z; 
        halfCourtLine = halfCourtBox.transform.position.x;
        baseLineLeft = LeftPlane.transform.position.x;
        baseLineRight = RightPlane.transform.position.x;
        floor = playingLevelPlane.transform.position.y;
        roof = TopPlane.transform.position.y;

        print("Right= " + baseLineRight);
        print("Half= " + halfCourtLine);
        print("Left= " + baseLineLeft);
        print("Far= " + farSideLine);
        print("Near= " + nearSideLine);

        print ("bottom = "+BottomPlane.transform.position);
		print ("top = "+ TopPlane.transform.position);
        print("left = " + LeftPlane.transform.position);
        print("right = " +RightPlane.transform.position);
        print("front = " + FrontPlane.transform.position);
        print("back = " + BackPlane.transform.position);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal GameObject[] GetWalls()
    {
        return walls;
    }

}
