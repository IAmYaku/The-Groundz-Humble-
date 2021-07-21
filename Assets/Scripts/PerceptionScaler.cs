using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionScaler : MonoBehaviour {

	//  Should be used on a global or levelMaanger basis..

	private Controller3D controller3D;
	private GameRules gameRules;
	private Transform transform;
	private float sizeX;
	private float sizeY;
	public float scaleFactor =350f;
	public float yFactor =5f;

	void Start () {
		transform = gameObject.transform;
		sizeX = transform.localScale.x;
		sizeY = transform.localScale.y;
//		GameObject stageObject = GameObject.Find ("Stage");
//		gameRules = stageObject.GetComponent<GameRules> ();
//		GameObject playerObject  = GameObject.Find ("Player");
//		controller3D = playerObject.GetComponent<Controller3D> ();
	}
	
	// Update is called once per frame
	void Update () {
	//	PerceptionScale ();
	//	YSpeed ();
	}

	void PerceptionScale(){
		transform.localScale = new Vector3 ( sizeX- transform.position.z/scaleFactor, sizeY - transform.position.z/scaleFactor, transform.localScale.z);
	}

	public void YSpeed() {

	//	if (controller3D.InBounds())
		{
			
			 Rigidbody body = gameObject.GetComponent<Rigidbody> ();
			Vector3 velocity = body.velocity; 
			print (gameObject.tag);

		if (velocity.z < 0)  {
			if (!Physics.CheckBox (transform.position + velocity, transform.localScale/2)) {
				velocity.y = yFactor * velocity.z;
				body.velocity = velocity;
					print (body.velocity);
			} else {
				body.velocity = new Vector3(0f, 0f, 0f);
				Collider[] colliders = (Physics.OverlapBox(transform.position + velocity, transform.localScale/2));
				foreach (Collider collider in colliders) {
				//	print ("A = " + collider.gameObject.tag + "is there");
				}
			}
			// print (Physics.OverlapSphere (transform.position, transform.localScale.magnitude, 0));
		}
		if (velocity.z > 0)  {
			if (!Physics.CheckBox (transform.position + velocity, transform.localScale/2)) {
				velocity.y = yFactor* velocity.z;
				body.velocity = velocity;
					print (body.velocity);
			} else {
				body.velocity = new Vector3 (0f, 0f, 0f);
				Collider[] colliders = (Physics.OverlapBox(transform.position + velocity, transform.localScale/2));
					foreach (Collider collider in colliders) {
						//print ("A = " + collider.gameObject.tag + "is there");
					}
				}
			}
			// print (Physics.OverlapSphere (transform.position, transform.localScale.magnitude, 0));
		}
	}

}
