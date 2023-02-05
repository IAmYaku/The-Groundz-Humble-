using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadow : MonoBehaviour {

	// Use this for initialization
	public SpriteRenderer shadowImage;
	 GameObject shadow;
	public GameObject ground;
	public float maxDistance = 5f;
	public Transform parent;

	void Start () {

		//shadowImage = gameObject.GetComponent<SpriteRenderer> ();  // playerConfig
		shadow = new GameObject ("Shadow");
		shadow.AddComponent<SpriteRenderer> ();
		shadow.GetComponent<SpriteRenderer> ().sprite = shadowImage.sprite;
		shadow.GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 0.5f);         //also scene dependent
		shadow.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		shadow.transform.parent = parent;
		shadow.transform.localPosition = Vector3.zero;
		shadow.transform.localEulerAngles = Vector3.zero;
	}
	
	
	void LateUpdate ()
	{																			//also scene dependent
		if (ground)
        {
			shadow.GetComponent<SpriteRenderer>().sprite = shadowImage.sprite;
			shadow.transform.localScale = new Vector3(-1 * transform.localScale.x * Mathf.Clamp(GetDistance(), 0.0f, 1.0f) * (shadowImage.flipX ? -1 : 1), transform.localScale.y, transform.localScale.z * Mathf.Clamp(GetDistance(), 0.0f, 1.0f));
			shadow.transform.position = new Vector3(transform.position.x, ground.transform.position.y, transform.position.z);
            shadow.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, Mathf.Clamp(maxDistance / GetDistance(), 0.0f, 0.35f));

		}

	}

	public float GetDistance() {
		float delta =  (transform.position.y - ground.transform.position.y);
		return Mathf.Clamp(delta, 0.0001f, maxDistance);
	}

	public void SetGroundObject(GameObject x)
    {
		ground = x;
    }
}
