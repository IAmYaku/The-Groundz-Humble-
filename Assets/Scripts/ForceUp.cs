using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUp : MonoBehaviour {
	Quaternion original;
	// Use this for initialization
	void Start () {
		original = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = original;
	}
}
