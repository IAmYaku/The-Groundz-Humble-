using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;



public class CamController : MonoBehaviour {


	public GameManager gameManager;
    LevelManager levelManager;


	public float posX;
	public float posY;
	public float posZ;
    
    private float offsetX;

	public float padding=20f;
    public float zoomWeight = .1f;
    public float xWeight = .75f;

    private float xDamp;
    private float zoomDamp;
	public float cameraSmoothe = .1f;

    public float playerWeight = 1.1f;

    public bool movable = false;

    private bool isGlitching;
    private bool isShaking;
    private float glitchIntensity;
    private float shakeIntensity;
    private float shakeTime;
    private float glitchTime;
    public float shakeAmp = .125f;
    public float shakeWeight = 1f;
    private bool normaled;

   //lol

    //...  
    
    //lolol!
    
	void Start () {
		posX = gameObject.transform.position.x;
		posY = gameObject.transform.position.y;
		posZ = gameObject.transform.position.z;

		GameObject gameManagerObject = GameObject.Find ("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager> ();
        levelManager = gameManager.levelManager;
        levelManager.SetCamera(this);
        
	}

	// Update is called once per frame
	void LateUpdate () {


        if (levelManager.isPlaying)
        {
            if (movable)
            {
                if (isShaking == false)
                {
                    float nuSize = padding + Mathf.Abs(MaxDistance()) * zoomWeight; 
                    float size0 = Camera.main.orthographicSize;
                    Camera.main.orthographicSize = Mathf.SmoothDamp(size0, nuSize, ref zoomDamp, cameraSmoothe);
                    Vector3 average = new Vector3(GetAverage(), 0.0f, 0.0f);                                                               // blows up if there ant any balls or players
                    float deltaX = (average.x - transform.position.x) * xWeight;
                    float nuX = Mathf.SmoothDamp(transform.position.x, average.x, ref xDamp, cameraSmoothe);
                    gameObject.transform.position = new Vector3(nuX, posY, posZ);
                }
            }
        }

        if (isShaking)
        {
            Shake(shakeTime);
            shakeTime--;
        }

        if (shakeTime <=0 )
        {
            isShaking = false;


        }

        if (isGlitching)
        {
            
        }

       
		
	} 

	float MaxDistance(){
		float min = 1000000.0f;
		float max = -1000000.0f;

		foreach (GameObject player in levelManager.GetPlayers()) {
			if (player.transform.GetChild (0).position.x < min) {
				min = player.transform.GetChild (0).position.x;

			}

			if (player.transform.GetChild (0).position.x > max) {
				max = player.transform.GetChild (0).position.x;
			}
		}
		foreach (GameObject ball in gameManager.levelManager.balls) {
			if (ball.transform.position.x < min) {
				min = ball.transform.position.x;
			}
			if (ball.transform.position.x > max) {
				max = ball.transform.position.x;
			}
		}

		return max - min;
	}

	public float GetAverage(){
		float sum = 0.0f;
		float objectsCount = gameManager.levelManager.balls.Count +  levelManager.GetPlayers().Count;
		foreach (GameObject ball in gameManager.levelManager.balls) {
            sum += ball.transform.position.x;
		}
		foreach (GameObject player in levelManager.GetPlayers()) {
            sum += player.transform.position.x * playerWeight;
		}
		return sum / (objectsCount);
	}

    public void Shake( float time) 
    {
         float nuX = transform.position.x + shakeIntensity * Mathf.Sin(time);
         transform.position = new Vector3 (Mathf.Lerp(transform.position.x,nuX,cameraSmoothe), transform.position.y, transform.position.z);
           
    }

    public void ZoomToSide(int side)
    {
        if (side == 1)
        {
           Quaternion camRot =  gameObject.GetComponent<Camera>().transform.localRotation;
            camRot.y = -5;
            float nuX = Mathf.SmoothDamp(transform.position.x, posX-5, ref xDamp, cameraSmoothe);
            gameObject.transform.position = new Vector3(nuX, posY, posZ);
            float size0 = Camera.main.orthographicSize;
            Camera.main.orthographicSize = Mathf.SmoothDamp(size0, 16, ref zoomDamp, cameraSmoothe);

        }
        if (side == 2)
        {
            Quaternion camRot = gameObject.GetComponent<Camera>().transform.localRotation;
            camRot.y = 10;
            float nuX = Mathf.SmoothDamp(transform.position.x, posX +5, ref xDamp, cameraSmoothe);
            gameObject.transform.position = new Vector3(nuX, posY, posZ);
            float size0 = Camera.main.orthographicSize;
            Camera.main.orthographicSize = Mathf.SmoothDamp(size0, 16, ref zoomDamp, cameraSmoothe);
        }
    }

    internal void Normal()
    {
        gameObject.transform.position = new Vector3(posX, posY, posZ);
        Quaternion camRot = gameObject.GetComponent<Camera>().transform.rotation;
        camRot.x = 14;
        camRot.x = 0;
        camRot.x = 0;
        gameObject.GetComponent<Camera>().orthographicSize = 20;  
    }

    internal void TrigCamShake(float intensity, Transform playerTrans)
    {
        isShaking = true;
        shakeTime = Mathf.Clamp(intensity/1,0f,8f);
        shakeIntensity = Mathf.Clamp(intensity/100,0f,3f);
      //  print("shakeIntensity = " + shakeIntensity);
       // print("shakeTime = " + shakeTime);

    }

    internal void ActivateGlitch(float intensity)
    {
        isGlitching = true;
        glitchIntensity = intensity;

    }
}
