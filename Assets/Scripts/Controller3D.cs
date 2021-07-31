using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller3D : MonoBehaviour {


    public GameObject parent;
    public Player playerScript;


    public PlayerInput playerInput;

    private LevelManager levelManager;

    GameObject playerConfigObject;

    public SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform transform;
    private Rigidbody rigidbody;
    private Collider collider;   //which?
    private float sizeX;
    private float sizeY;
    private float sizeZ;

    public GameObject playerAura;
    Color color;

    GameObject superPackage;

    public static String mode = "Keyboard";

    private SwipeInput swipeInput = new SwipeInput();
    private SwipeInput vJSwipeInput = new SwipeInput();
    private JoyInput joyInput = new JoyInput(2);
    private Vector3 move;
    private Vector3 move0;

    public float maxSpeed = 40f;
    public float xSpeed = 40.0f;
    public float zSpeed = 40.0f;
    private float xCeleration = 0;
    public float maxCeleration = 6;
     float acceleration = 1f;
    float accelLogCount = .0001f;
    public float ChargePowerAlpha => Mathf.Clamp01(throwCharge / gameObject.GetComponentInParent<Player>().maxThrowPower);

    private Vector3 velocityDamp;
    public float jumpSpeed = 10.0f;
    private Vector3 velocity;

    float accelerationTime = .85f;

    Vector3 chargeVel;

    public float throwPower = 200f;
    public float standingThrowPower = 100f;
    float standingThrowThresh = 20f;
    public float maxThrowPower = 240f;
    public float maxStandingThrowPower = 1600;

    float throwCharge;
    bool isCharging;

    public Vector3 handSize = new Vector3(3f, 3f, 3f);
    public float grabRadius = 5f;

    public GameObject nearestBall;
    public GameObject ball;                 // esentially ballGrabbed
    public GameObject ballHit;

    bool catchReady = true;
    public float catchLagTime;
    private float catchCoolDown;
    //  int catchFrameCount;             // just one frame per catch for player
    int catchFrameCool;

    public bool ballGrabbed = false;
    public bool ballThrown;    //?
    public bool ballCaught;

    public static bool hasGrabMag = false;
    public static float grabMag = 10f;
    public static bool hasThrowMag = false;
    public static float throwMagnetism = 5.65f;
    public static float maxSeekVec = 100f;

    private Vector3 throwDirection;
    private Vector3 throww;
    public bool ballContact;

    private bool isBlocking;

    private bool isSupering = false;
    public float superCoolDown = 10f;
    private GameObject ballSupered;
    public bool isKnockedOut;
    private float knockedOutTime;
    private float angle;

    private bool isFacingRight;

    private bool wallCollision;
    public bool inBounds;
    private  float pushVal = .265f;

    public bool onGround = true;


    public float stamina;                               // ! *Frame dependent  -> Time
    public float staminaCool;
    public float staminaCoolRate;

    public float throwStaminaCost;                  // should probably invert stamina method
    public float moveStaminaCost;
    public float catchStaminaCost;
    public float pickUpStaminaCost;
    public float staminaDodgeCost = 10.0f;
    public float staminaReadyCost = 5.0f;

    public float toughness = 2;   // important when taking into account how players react to getting hit.. i.e stall time

    bool inBallPause;
    bool ballPauseReady = true;

    public static bool easyMove;
    public static bool canJumpThrow = false;

    private bool isJumping;
    private bool canDodge;
    private bool isDodging;
    public float dodgeSpeed = 20f;


    private float t_catch0;
    private float t_catchF;

    private float t_contact0;
    private float t_contactF;

    private float t_knock0;
    private float t_knockF;

    private float t_s0;
    private float t_sF;

   
    public bool isAudioReactive;

    private bool hasPerks;
    private float perkDur;

    private string vertInput = "Vertical_P1";               // shouldnt these liv in MyJoystick
    private string horzInput = "Horizontal_P1";
    private string jumpInput = "Jump_P1";
    private string action1Input = "joystick 1 button 1";
    private string rTriggerInput = "Fire_P1";
    private string superInput = "Super_P1";
    private string blockInput = "Block_P1";
    private string pauseInput = "joystick 1 button 9";
    private string altAction1Input = "h";
    private string altSuper1Input = "j";
    private string altDodge1Input = "k";

    bool IsKeyPickUp;

    private Touch touch0;
    private Touch touch1;
    private Touch touchGrab;
    private Touch touchThrow;

    private Vector3 touch0_0;
    private Vector3 touch0_F;
    private Vector3 touch1_0;
    private Vector3 touch1_F;
    private bool playerIsTouched;
    public static float playerTouchThresh = 3f;
    private bool isTapThrowReady;
    private UnityEngine.TouchPhase touch1Phase_prev;
    private float touch0Reset = 1f;

    private TouchFX touchFX;

    public static Joystick vJoystick;
    public static ThrowButton throwButton;
    public static PickUpButton pickUpButton;
    public static DodgeButton dodgeButton;
    public static SuperButton superButton;
    public static PauseButton pauseButton;



    private void Awake()
    {
        if (!playerScript)
        {
            playerScript = gameObject.transform.parent.gameObject.GetComponent<Player>();
        }
    }

    private void OnEnable()
    {
        if (playerScript)
        {
            if (playerScript.hasJoystick)
            {
            //    playerInput.enabled = true;
            }
        }
    }

    private void OnDisable()
    {
     //   playerInput.enabled = false;
    }
    void Start() {

        GameObject gameManagerObject = GameObject.Find("GameManager");

        levelManager = gameManagerObject.GetComponent<LevelManager>();

        playerConfigObject = playerScript.playerConfigObject;
        animator = playerConfigObject.GetComponent<Animator>();
        spriteRenderer = playerConfigObject.GetComponent<SpriteRenderer>();
        rigidbody = playerConfigObject.GetComponent<Rigidbody>();

        transform = gameObject.transform;     // iffy

        collider = playerConfigObject.GetComponent<Collider>();
        sizeX = collider.bounds.max.x - collider.bounds.min.x;
        sizeY = collider.bounds.max.y - collider.bounds.min.y;
        sizeZ = collider.bounds.max.z - collider.bounds.min.z;
        color = gameObject.GetComponentInParent<Player>().color;


    }

    private void InitTouchComponents()
    {
        vJoystick = FindObjectOfType<Joystick>();

        throwButton = FindObjectOfType<ThrowButton>();
        pickUpButton = FindObjectOfType<PickUpButton>();
        if (!dodgeButton)
        {
            dodgeButton = FindObjectOfType<DodgeButton>();
        }
        superButton = FindObjectOfType<SuperButton>();
        pauseButton = FindObjectOfType<PauseButton>();

        swipeInput.SetMoves(SwipeInput.bufferSize);
        vJSwipeInput.SetMoves(SwipeInput.bufferSize);

      //  touchFX = playerScript.playerConfigObject.transform.GetChild(2).gameObject.GetComponent<TouchFX>();

    }



    // Update is called once per frame
    void Update() {


        if (levelManager.isPlaying)
        { 
            // * !STOP reading from playerScript 

            MoveInput();
            GrabInput();
            BlockInput();
            SuperInput();
            HandleContact();
           // HandlePerks(perkDur,1f);
           

        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

            PauseInput();
    }

    private void HandlePerks(float dur, float mult)
    {
       if (hasPerks)
        {
           
        }
    }

    private void PauseInput()
    {
        /*
        if (pauseButton.Pressed)
        {
            gameManager.PauseGame();
        }
        */
    }

    private void HandleContact()
    {
        if (ballContact)
        {
            TriggerHitIndicators();

            float contactSlowDownfactor = .4f;             // should be rekative to ballHit speed
            SlowDown(contactSlowDownfactor);

            float deltaT = t_contactF - t_contact0;                                        // < -- iffy, prolly should bbe t_c0 for "c"ontact
            if (deltaT <= 5 / toughness)  // *arbitrary num
            {
                t_contactF += Time.realtimeSinceStartup;
                //  velocity = new Vector3(velocity.x / 2, velocity.y / 2, velocity.z / 2);
            }
           

        }
    }

  

    private void TriggerHitIndicators()
    {
        // could use work ...


        ParticleSystem ps = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ring_ps = ps.main;
        ParticleSystem ps_2 = transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule spikes_ps = ps_2.main;

        ring_ps.simulationSpeed = 300 / Mathf.Clamp(Vector3.Distance(ballHit.transform.position, levelManager.stage.BottomPlane.transform.position), .001f, 100);   // * arbitrary num
        spikes_ps.simulationSpeed = 300 / Mathf.Clamp(Vector3.Distance(ballHit.transform.position, levelManager.stage.BottomPlane.transform.position), .001f, 100);  // * arbitrary num
    }

    void MoveInput()
    {

        playerConfigObject.transform.localEulerAngles = new Vector3( playerConfigObject.transform.localEulerAngles.x, 0f, playerConfigObject.transform.localEulerAngles.z);


        if (rigidbody.velocity.magnitude < 3f)             // *arb = moveThresh
            {
            
                    if (staminaCool > 0.0f)      // should invert ... i.e - cost, as opposed to + cost
                   {
                        staminaCool-= staminaCoolRate;        //  *Should go time dependent
                   }
            }

        

      /*  if (Mathf.Abs(Input.GetAxis("R Horizontal_P1"))> 0.0f || Mathf.Abs(Input.GetAxis("R Vertical_P1")) > 0.0f)
        {
            print(" r.X = " + Input.GetAxis("R Horizontal_P1"));
            print(" r.Y = " + Input.GetAxis("R Vertical_P1"));
        }

    */

        if (playerScript.joystick.number == 1 || playerScript.joystick.number == 0)                   // sort of deprecated

        {
            Vector2 swipeMuv = Vector2.zero;
            Vector2 vJSwipeMuv = Vector2.zero;
            Vector3 keyMuv = Vector2.zero;

            float keyLeftVal = 0f;
            float keyRightVal = 0f;
            float keyUpVal = 0f;
            float keyDownVal = 0f;


            Vector2 joyMuv = Vector2.zero;



            
            if (mode =="Keyboard" || mode == "Joystick")                     // deprecated
            {

                // joyMuv = joyInput.Input(Input.GetAxis(playerScript.joystick.horzInput), Input.GetAxis(playerScript.joystick.vertInput));

                joyMuv = new Vector2 (Input.GetAxis(playerScript.joystick.horzInput), Input.GetAxis(playerScript.joystick.vertInput));
                joyInput.Input(joyMuv.x, joyMuv.y);

                keyLeftVal = 0f;
                keyRightVal = 0f;
                keyUpVal = 0f;
                keyDownVal = 0f;

                float keyMult = 1.0f;

                if (Input.GetKey(playerScript.joystick.altLeft1Input))
                {
                    keyLeftVal = -1f * keyMult;
                 //   print("keyLeft");
                }
                if (Input.GetKey(playerScript.joystick.altRight1Input))
                {
                    keyRightVal = 1f * keyMult;
                 //   print("keyRight");
                }
                if (Input.GetKey(playerScript.joystick.altUp1Input))
                {
                    keyUpVal = 1f * keyMult;
                  //  print("keyUp"); ;
                }
                if (Input.GetKey(playerScript.joystick.altDown1Input))
                {
                    keyDownVal = -1f * keyMult;
                 //   print("keyDown");
                }

                keyMuv = new Vector3(keyRightVal + keyLeftVal, 0f, keyUpVal + keyDownVal);
                keyMuv.Normalize();
            }
            

                move.x = joyMuv.x + swipeMuv.x + vJSwipeMuv.x + keyMuv.x;
                move.z = joyMuv.y + swipeMuv.y + vJSwipeMuv.y + keyMuv.z;

        }

        else
        {
               Vector2 joyMuv = joyInput.Input(Input.GetAxis(playerScript.joystick.horzInput), Input.GetAxis(playerScript.joystick.vertInput));
            // Vector2 joyMuv = new Vector2(Input.GetAxis(playerScript.joystick.horzInput), Input.GetAxis(playerScript.joystick.vertInput));
            move.x = joyMuv.x;
            move.z = joyMuv.y;

        }


        if (InBounds())
        {
            if (!isKnockedOut)
            {

                if (onGround)
                {
                    if (staminaCool < stamina - .1f)
                    {
                        // Slow Down for Grab Assistance
    
                            nearestBall = GetNearestBall();

                        float distanceScale = 1f;
                        float slowDownThresh = 10f;
                        float velMag = rigidbody.velocity.magnitude;
                       // print("VelMag = " + velMag);

                        if ( !ballGrabbed && BallIsInGrabDistance(nearestBall, distanceScale) && (velMag > slowDownThresh ))  
                        {
                            if (!nearestBall.GetComponent<Ball>().thrown && IsFacingObj(nearestBall))
                            {
                                //  print("Slowing down for ball");
                                float grabHelpSlowDownfactor = .125f + velMag / 1000f;     // should be game level dependent
                              //  print("grabHelpSlowDownFactor = "+ grabHelpSlowDownfactor);
                                SlowDown(grabHelpSlowDownfactor);

                                accelerationTime = .25f;
                                Invoke("NormalAccelerationTime", 1f);
                            }
                        }

                        // Slow down for Player Collision

                        if (PlayerNear() && rigidbody.velocity.magnitude > slowDownThresh)     // can impeded on tag idea
                        {
                         //   if (GetComponent<Raycast>().isOnline)
                            {
                           //     print("Slowing down for player");
                                float playerSlowDownfactor = .5f;
                                SlowDown(playerSlowDownfactor);
                            }
                        }

                        //Slow Down for Wall

                        if (IsWallNear())
                        {
                            float wallSlowDownfactor = .85f;
                            SlowDown(wallSlowDownfactor);
                        }

                       if (!inBallPause && !IsAction1Input() && !isDodging && !isCharging)
                        {

                            ApplyVelocity(move);
                            UpdateAcceleration(move);
                        }
                      

                        AnimateMovement();

                        if (rigidbody.velocity.magnitude > 10f)        // *arb
                        {
                            playerScript.playFootsteps();
                            staminaCool += .025f;            // *arb .. stamina move cost
                        }
                    }

                    //handle Dodge/Jump Input


                    if ((Input.GetButtonDown(playerScript.joystick.jumpInput) || Input.GetKeyDown(playerScript.joystick.altDodge1Input) || (playerScript.joystick.number == 1 && dodgeButton && dodgeButton.Pressed)) && staminaCool <= stamina / 2f)   // * arb...dod     
                    {
                        if (InBounds())
                        {
                            //Dodge
                            if (IsInDodgeRange() && !isDodging)
                            {
                                if (Mathf.Abs(move.z) > Mathf.Abs(move.x))
                                {
                                    print("Dodge!");
                                    isDodging = true;

                                    if (animator)
                                    {
                                        animator.SetTrigger("Dodge");
                                    }
                                    
                                    staminaCool += staminaDodgeCost;    
                                    rigidbody.AddForce(new Vector3(0f, jumpSpeed / 4f, Mathf.Sign(rigidbody.velocity.z) * dodgeSpeed) * 75, ForceMode.Impulse);  //*arb
                                    playerScript.PlayDodgeSound();

                                    float dodgeCool = .25f + (rigidbody.velocity.magnitude/1000f);
                                    Invoke("SetDodgingF", dodgeCool);

                                }
                                else
                                {
                                    print("Dodge!");
                                    isDodging = true;

                                    if (animator)
                                    {
                                        animator.SetTrigger("Dodge");
                                    }

                                    staminaCool += staminaDodgeCost;
                                    rigidbody.AddForce(new Vector3(0f, jumpSpeed / 4f, GetDodgeDirection() *100.0f * dodgeSpeed), ForceMode.Impulse);  //*arb
                                    playerScript.PlayDodgeSound();

                                    float dodgeCool = 1f + (rigidbody.velocity.magnitude / 1000f);
                                    Invoke("SetDodgingF", dodgeCool);
                                }
                            }


                            //jump throw init
                            if (canJumpThrow)
                            {
                                /*
                                if (Mathf.Abs(rigidbody.velocity.z) < Mathf.Abs(rigidbody.velocity.x) && ballGrabbed)
                                {
                                    if (animator)
                                    {
                                        if (animator.GetBool("Jumping") == false)
                                        {
                                            animator.SetTrigger("Jump");
                                            animator.SetBool("Jumping", true);
                                        }
                                    }
                                    rigidbody.AddForce(new Vector3(Mathf.Sign(rigidbody.velocity.x), jumpSpeed, 0f), ForceMode.Impulse);
                                    isJumping = true;
                                    onGround = false;
                                }

                                playerScript.ToggleActivateDodge();
                                Invoke("SetDodgingF", 1);

                                */
                            }
                        }
                    }

                }
            }


            else
            {
                // knocked out 
                t_knockF = Time.realtimeSinceStartup;
                knockedOutTime -= t_knockF - t_knock0;
                t_knock0 = Time.realtimeSinceStartup;

                if (knockedOutTime <= 0f)
                {
                    isKnockedOut = false;
                }
            }


        }
    }

    private float GetDodgeDirection()
    {
        float zPos = playerConfigObject.transform.position.z;

        if (zPos > -10.0f && zPos < 10.0f)
        {
            return CoinFlip();
        }

        if (zPos < -10.0f)
        {
            return 1.0f;
        }

        if (zPos > 10.0f)
        {
            return -1.0f;
        }

        return 0.0f;
    }

    private float CoinFlip()
    {
        float ran = (UnityEngine.Random.Range(-1.0f, 1.0f));

        if (ran > 0.0f)
        {
            return 1.0f;
        }
        else
        {
            return -1.0f;
        }
    }

    private void UpdateAcceleration(Vector3 move)
    {
        float accelerationThresh = .85f;
        float accelerationCap = 1.75f;
        float accelerationCurve = 1.15f;
        float decelerationMult = 60.0f;
        float logMult = 0.05f;
        float logOffset = 36f;

        if (move.magnitude >= accelerationThresh && acceleration <= accelerationCap)
        {
            accelLogCount += Time.deltaTime;
            acceleration = Mathf.Clamp(( Mathf.Log(accelLogCount, accelerationCurve) +logOffset) * logMult ,1f,accelerationCap);
        }
        else
        {
           // print("move.magnitude = " + move.magnitude);
           // print("acceleration = " + acceleration);
          //  print("acceleration = " + acceleration);

            if (acceleration > 1.0)
            {
                accelLogCount -= Time.deltaTime * decelerationMult;
                accelLogCount = Mathf.Clamp(accelLogCount, 0.00001f, accelLogCount);
                acceleration = Mathf.Clamp((Mathf.Log(accelLogCount, accelerationCurve) + logOffset) * logMult, 1f, accelerationCap);
            }
        }
    }

    internal void SetAccelerationime(float v)
    {
        accelerationTime = v;
    }

    private bool IsFacingObj(GameObject obj)
    {
        if (isFacingRight && (playerConfigObject.transform.position.magnitude - nearestBall.transform.position.magnitude > 0))
        {
            return true;
        }

        if (!isFacingRight && (playerConfigObject.transform.position.magnitude - nearestBall.transform.position.magnitude < 0))
        {
            return true;
        }
        return false;
    }

    private void AnimateMovement()
    {
        float moveThresh =  .2f;

        if (Mathf.Abs(move.x) > moveThresh || Mathf.Abs(move.z) > moveThresh)          // *arb num  ... moveThesh
        {
           // print("moving");

            if (!isDodging || !isJumping)
            {
                float mack3MoveSpeedScale = .042f;
                float moveAnimSpeed = Mathf.Clamp((rigidbody.velocity.magnitude - (Mathf.Clamp(Mathf.Abs(rigidbody.velocity.z), 1f, Mathf.Abs(rigidbody.velocity.z))) * .25f) * mack3MoveSpeedScale, .50f, 2f);
              //  print("moveAnimSpeed = " + moveAnimSpeed);
                animator.SetFloat("Speed", moveAnimSpeed); // *arbitrary num, should be animation dependent

                if (animator)
                {
                    if (animator.GetBool("Running") == false)
                    {
                        animator.SetBool("Running", true);
                      

                    }
                }

                if (move.x > moveThresh)
                {
                    if (!isFacingRight)
                    {
                        isFacingRight = true;
                        spriteRenderer.flipX = false;
                     //   animator.SetTrigger("Pivot");
                        float pivotTime = .1f;
                   //     Invoke("FlipRight", pivotTime);
                   
                    }
                    
                    throwDirection.x = 1;

                }
                if (move.x < -moveThresh)
                {
                    if (isFacingRight)
                    {
                        isFacingRight = false;
                        spriteRenderer.flipX = true;
                     //   animator.SetTrigger("Pivot");
                        float pivotTime = .1f;
                      //  Invoke("FlipLeft", pivotTime);
                    
                    }
                    
                    throwDirection.x = -1;
                }

                if (move.z > 0)           // idk?
                {
                    throwDirection.z = 1;
                }
                if (move.z < 0)
                {
                    throwDirection.z = -1;
                }

            }
        }

        else
        {
            if (animator)
            {
                if (animator.GetBool("Running") == true)
                {
                    animator.SetBool("Running", false);


                }
            }
        }

       
    }

    private void FlipRight()
    {
        spriteRenderer.flipX = false;
    }

    private void FlipLeft()
    {
        spriteRenderer.flipX = true;
    }

    private void SlowDown(float scale)                                                     
    {
     
        move.x = (move.x) * scale;
       move.z = (move.z) *scale;
        

    }

    private void SlowDownByVelocity(float scale)
    {

        if (isCharging)
        {
            rigidbody.velocity = Vector3.Lerp(chargeVel , Vector3.zero, scale);
            chargeVel = rigidbody.velocity;

        }

        else
        {
            rigidbody.velocity *= scale;
        }

    }

    float LerpFloatDT(float start, float end, float t, float speed)
    {
        float delta = end - start;
        if (delta == 0)
            return end;
        start += (t * speed * Mathf.Sign(delta));
        if ((end - start) * delta < 0)
            return end;
        return start;
    }

    public void SetTouch0FXActivate(bool v)
    {
      //  touchFX.Set0Acivate(v);
    }

    public void SetTouch1FXActivate(Vector3 t1pos)
    {
        touchFX.Touch1Acivate(t1pos);
    }

    private Vector3 GetTouchToWorld(Touch screen_touch0)
    {
        Vector3 touch2World = Camera.main.ScreenToWorldPoint(new Vector3(screen_touch0.position.x, screen_touch0.position.y, 0));
        Vector3 offset = new Vector3(0f, 200f, 0f);
        float zRemap = ReMap(screen_touch0.position.y, 300f, 650f, -44.0f, 65f);
       return new Vector3(touch2World.x, touch2World.z + offset.y, zRemap);
    }

    private float ReMap( float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    private bool IsInDodgeRange()
    {
       if (rigidbody.velocity.z > 0)
        {
            if (ObjectIsInGrabDistance(levelManager.stage.BackPlane))
            {
                return false;
            }
        }
        else
        {
            if (ObjectIsInGrabDistance(levelManager.stage.FrontPlane))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsWallNear()
    {
        GameObject[] walls = levelManager.stage.GetWalls();

        foreach (GameObject wall in walls)
        {

            return (ObjectIsInGrabDistance(wall));
        }
        return false;
    }

    internal void SpeedBoost(float dur, float mult)
    {
        xSpeed = xSpeed * mult;
        zSpeed = zSpeed * mult;
    }

    
    private bool PlayerNear()
    {
        int team = playerScript.GetComponent<Player>().team;
        int number = playerScript.GetComponent<Player>().number;
        
        foreach (GameObject other in levelManager.GetPlayers())
        {
            int otherNum = other.GetComponent<Player>().number;
            GameObject otherConfig = other.transform.GetChild(0).gameObject;

            if (otherNum != number)
            {
                if (ObjectIsInGrabDistance(otherConfig))  
                {
                    print("Player Near");
                    return true;
                }
            }

        }
        return false;
    }

    private void ApplyVelocity(Vector3 move)
    {


        if (mode == "Keyboard" || mode == "Joystick") 
        {
            float maxKoJVel = 4f;

            //float xVelocity = move.x * Time.deltaTime * (Mathf.Pow(xSpeed, 1f + joyInput.muvXceleration * acceleration));
            // float zVelocity = move.z * Time.deltaTime * (Mathf.Pow(zSpeed, 1f + joyInput.muvXceleration * acceleration));     

         //   float xVelocity = Mathf.Clamp(move.x * Time.deltaTime * xSpeed * xCelerate, -maxKoJVel, maxKoJVel);
        //    float zVelocity = Mathf.Clamp(move.z * Time.deltaTime * zSpeed * zCelerate, -maxKoJVel, maxKoJVel);

            // float xCelerate = (Mathf.Pow(1.85f, 1.5f + joyInput.muvXceleration * acceleration));
            // float zCelerate = (Mathf.Pow(1.85f, 1.5f + joyInput.muvXceleration * acceleration));

            float muvXcel_x = Mathf.Abs(joyInput.GetMuvDelta().x);
            float muvXcel_z = Mathf.Abs( joyInput.GetMuvDelta().y);
            float muvMag = Vector2.SqrMagnitude(new Vector2(muvXcel_x, muvXcel_z));

         //   print("muvXcel_x = " + muvXcel_x);
        //    print("muvXcel_z = " + muvXcel_z);

            float pow0_x = 4.6f;
            float pow0_z = 6.0f;

            //float powMult = 2.0f;
            float clampMult_x = 20.0f;
            float clampMult_z = 30.0f;



            float accMult_z = 1.0f;

            float xCelerate = Mathf.Clamp((Mathf.Pow(muvXcel_x * acceleration, pow0_x + muvXcel_x)), 0.0f, clampMult_x);                       // impartial but feels good
            float zCelerate = Mathf.Clamp((Mathf.Pow(muvXcel_z * acceleration * accMult_z, pow0_z + muvXcel_z)), 0.0f, clampMult_z);



           //   print("xCelerate = " + xCelerate);
          //    print("zCelerate = " + zCelerate);

            //print("Celerate Mag = " + Vector2.SqrMagnitude(new Vector2(xCelerate, zCelerate)));

            float xMultiplier = 6f;
            float zMultiplier = 8f;   //   < -- faulty, but feels good
            float frameMult = 0.017f;

            float xVelocity = move.x * frameMult * xSpeed *  xMultiplier * acceleration;
            float zVelocity = move.z * frameMult  * zSpeed * zMultiplier * acceleration;
            

            // rigidbody.velocity =  new Vector3(xVelocity,rigidbody.velocity.y, zVelocity);
             // print("xVeclocity = " + xVelocity);
            // print("zVeclocity = " + zVelocity);

            // rigidbody.AddForce(move.x * xCelerate , 0 , move.z * zCelerate, ForceMode.VelocityChange);

            Vector3 velVec = new Vector3(xVelocity, 0f, zVelocity);

            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, velVec, accelerationTime);
        }

        // clamp velcocity to max velocity
        if (rigidbody.velocity.magnitude> maxSpeed)
        {
               //rigidbody.velocity /= 1.5f;
           // print("rigidbody.velocity.mag = " + rigidbody.velocity.magnitude);
        }

    }


    void GrabInput() {
        // A ~ Pick up/Drop, X~ Throw Mechanic


        PickUpActivate();
        CheckHasBallAnim();

        CheckTouchInput();

        CheckCatchCool();


        if (IsAction1Input() && !ballGrabbed)                                       // ~ pick up /catch              
        {
            float action1Cost = 5f;
           DepleteStamina(action1Cost);

            if ((staminaCool < stamina - 1) && catchReady)                   // will change on stamina system revision pass.. see DepleStamina comments
            {
                nearestBall = GetNearestBall();                                         // set in PickupActivate but whatevs

                if (ObjectIsInGrabDistance(nearestBall))
                {

                    ball = nearestBall;
                    var ballComp = ball.GetComponent<Ball>();
                    if (!ballComp.isSupering)
                    {

                        if (mode == "Touch" || (mode == "Virtual Joystick" ))
                        {
                            Invoke("SetTapThrowReadyToTrue", 1f);
                            touchGrab = touch1;
                        }

                        CheckKeyPickUp();

                        // should have a bit of recoil based on ball velocity
                        Vector3 velocityCaught = ball.GetComponent<Rigidbody>().velocity;
                        ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

                        ballGrabbed = true;
                        ballComp.grounded = false;                         //methodize
                        ballComp.grabbed = true;
                        ball.GetComponent<SpriteRenderer>().enabled = false;
                        ball.transform.GetChild(3).gameObject.SetActive(false);
                        ball.GetComponent<SphereCollider>().enabled = false;
                        ball.GetComponent<Rigidbody>().useGravity = false;
               
                        ball.transform.GetChild(1).gameObject.SetActive(false);   // most likely pikUp Deactivaate


                        ballComp.PickUpDeactivate();
                      
                        Physics.IgnoreLayerCollision(5, 3, false);              //?


                        if (ThrownByOpp(ball, 2) || ThrownByOpp(ball, 1))
                       {
                            
                            if (animator)
                            {
                                animator.SetTrigger("Catch");
                                Invoke("ResetCatchTrigger", 1f);
                                
                            }

                            ballContact = false;
                            ballCaught = true;

                            ballComp.playCatch();

                            levelManager.ClearContacts(ball);
                            levelManager.OutDisplay(levelManager.throws[ball].transform.GetChild(0).gameObject);
                            levelManager.AddCatch(ball, parent);
                            levelManager.LastThrowerOut(ball);
                            levelManager.GetAnotherPlayer(gameObject.GetComponentInParent<Player>().team);
                            levelManager.RemoveHit(ball);
                            levelManager.CatchDisplay(playerConfigObject.transform.position, (Vector3.Magnitude(rigidbody.velocity) + Vector3.Magnitude(velocityCaught)));
                            ballComp.DeactivateThrow();

                         

                            float hitPauseDuration = velocityCaught.magnitude / 100f;
                            float hitPausePreDelay = .25f;

                            HitFX(ballComp, hitPauseDuration, hitPausePreDelay);

                            print("~!Caught!~");
                        }
                        else
                        {
                            

                            if (animator)
                            {
                                if (ball.transform.position.y < 2f)
                                {
                                    animator.SetTrigger("PickUp");
                                    animator.ResetTrigger("PickUp");
                                }
                            }

                            
                        }
                    }
                }

                else
                {
                    animator.SetTrigger("Ready");
                    if (staminaCool < stamina)  // Might not be neccessary since we do this within the first few lines
                    {
                      //  staminaCool += staminaReadyCost;  // *arbitray num
                    }
                }
              }

            if (catchReady)       // and catchframecount <= 0
            {
                catchReady = false;
                catchCoolDown = catchLagTime;
            }

          //  print("vel mag = " + (rigidbody.velocity.magnitude) / 100f);
            float action1SlowDownfactor = 1.0f - (rigidbody.velocity.magnitude)/100f;     //split to ready pickUp catch and character attribute specific
            //SlowDownByVelocity(action1SlowDownfactor);
            accelerationTime = .5f;
            Invoke("NormalAccelerationTime", accelerationTime);
        }


        else        // <-- Important else! Don't delete
        {
            /// throw
           
            //charge     
            // ^^ DO Something  with this.. \/ ($dexterity key)


            if (ballGrabbed && (Input.GetButton(playerScript.joystick.rTriggerInput) || (Input.GetKey(playerScript.joystick.altAction1Input)  && !IsKeyPickUp) || (playerScript.joystick.number == 1 && throwButton && throwButton.Pressed))) // if touch0Vel touch1Vel are opposite - > ( if throw is in direction)
            {
                float chargeCost = .25f;

                if (!isCharging)
                {
                    chargeVel = rigidbody.velocity;
                    print("chargeVel = " + chargeVel);
                    isCharging = true;
                }

            

                                  //make time dependent

                float chargeThrowSlowDownfactor = 1f;

                if (chargeVel.magnitude <= standingThrowThresh && (standingThrowPower + throwCharge) < maxStandingThrowPower)     // *arbs
                {
                    print("Charging Stand");
                    print("Charge @ " + throwCharge);
                    float chargeRate = 30; 
                    throwCharge += chargeRate;  // * Time.deltaTime
                    throwCharge = Mathf.Clamp(throwCharge, 0f, maxStandingThrowPower - standingThrowPower);

                     chargeThrowSlowDownfactor = .0125f; // * Time.deltaTime
                    SlowDownByVelocity(chargeThrowSlowDownfactor);

                    accelerationTime = .25f;
                    Invoke("NormalAccelerationTime", 1f);

                }
                else
                {
                    CheckStandingMaxedThrow(chargeVel.magnitude);
                }

                if (chargeVel.magnitude > standingThrowThresh && (throwPower + throwCharge) < maxThrowPower)
                {
                    
                    print("Charging moving");
                    float charge = 10;
                   throwCharge += charge;  // * Time.deltaTime
                    // clamp throwCharge

                    print("charge vel = " + chargeVel);
                    chargeThrowSlowDownfactor = .0075f;   // * Time.deltaTime
                  SlowDownByVelocity(chargeThrowSlowDownfactor);
                   
                    accelerationTime = .25f;
                    Invoke("NormalAccelerationTime", 1f);
                    
                }
                else
                {
                    CheckMovingMaxedThrow(chargeVel.magnitude);
                }

                

                  DepleteStamina(chargeCost);

                animator.SetTrigger("Charge");
               // Invoke("ResetChargeAnimations", .05125f);    //arbs
                  
            }

            //release
            
            if (ballGrabbed && (Input.GetButtonUp(playerScript.joystick.rTriggerInput) || (Input.GetKeyUp(playerScript.joystick.altAction1Input) && !IsKeyPickUp) || (playerScript.joystick.number == 1 && throwButton && throwButton.Pressed) || (Input.touchCount>=2 && isTapThrowReady &&  playerIsTouched && !isVirtualJoystickButtonsPressed())))
            {
             
                {
                    float mackThrowDelay = .1f;
                  //  Invoke("Throw", mackThrowDelay);
                    Throw();
                }

                if (animator)
                {
                    float mackThrowDelay = .1f;
                    animator.SetBool("hasBall", false);
                    Invoke("ResetThrowAnimations", .05125f);    //arbs
                }

                float throwSlowDownfactor = .25f;
             //   SlowDownByVelocity(throwSlowDownfactor);

            }
            
            // drop
            if (ballGrabbed && (Input.GetKeyDown(playerScript.joystick.action1Input) ||/* Input.GetKeyDown(playerScript.joystick.altAction1Input) || */ (playerScript.joystick.number == 1 && pickUpButton && pickUpButton.pushed)))
            {
                DropBall();

                float dropSlowDownfactor = .5f;
                SlowDown(dropSlowDownfactor);
            }

        }
        // move ball
        if (ballGrabbed && !isBlocking) {
            Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude/1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
            if (levelManager.IsInGameBounds(cockBackPos)) {
                if (!isSupering)
                {
                   // float nuBallX = transform.position.x + throwDirection.x * handSize.x;
                  //  float nuBallY = transform.position.y + handSize.y;
                    //left handed or right handed
                //    float nuBallZ = transform.position.z + handSize.z;

                    ball.transform.position = cockBackPos;
                }
                else
                {
                    Vector3 nuVec = cockBackPos * 1.25f;
                    ball.transform.position = nuVec;

                }
            }
        }

        if (mode == "Touch" || mode == "Virtual Joystick")
        {
            touch1Phase_prev = touch1.phase;
        }
    }

    private void CheckStandingMaxedThrow(float chargeVel)
    {
        float chargeCost = .25f;

        if ((standingThrowPower + throwCharge) >= maxStandingThrowPower && chargeVel <= standingThrowThresh)
        {
            print("Charge throw maxed @ " + throwCharge);
            /*
            chargeThrowSlowDownfactor = .0125f;
            SlowDownByVelocity(chargeThrowSlowDownfactor);

            accelerationTime = .25f;
            Invoke("NormalAccelerationTime", .95f);
            */

            {
                float mackThrowDelay = .1f;
                //  Invoke("Throw", mackThrowDelay);
                Throw();
            }

            if (animator)
            {
                float mackThrowDelay = .1f;
                animator.SetBool("hasBall", false);
                Invoke("ResetThrowAnimations", .05125f);    //arbs
            }

            float throwSlowDownfactor = .25f;
            //   SlowDownByVelocity(throwSlowDownfactor);



            DepleteStamina(chargeCost);
        }
    }

    private void CheckMovingMaxedThrow(float chargevel)
    {
        float chargeCost = .25f;

        if ((throwPower + throwCharge) >= maxThrowPower && chargevel > standingThrowThresh)
        {
            print("Charge throw maxed @ " + throwCharge);
            /*
            chargeThrowSlowDownfactor = .0125f;
            SlowDownByVelocity(chargeThrowSlowDownfactor);

            accelerationTime = .25f;
            Invoke("NormalAccelerationTime", .95f);
            */

            {
                float mackThrowDelay = .1f;
                //  Invoke("Throw", mackThrowDelay);
                Throw();
            }

            if (animator)
            {
                float mackThrowDelay = .1f;
                animator.SetBool("hasBall", false);
                Invoke("ResetThrowAnimations", .05125f);    //arbs
            }

            float throwSlowDownfactor = .25f;
            //   SlowDownByVelocity(throwSlowDownfactor);



            DepleteStamina(chargeCost);
        }
    }

    private void ResetCatchTrigger()
    {
        animator.ResetTrigger("Catch");
    }

    private void NormalAccelerationTime()
    {
        accelerationTime = .85f;
    }

    private void CheckKeyPickUp()
    {
        if (Input.GetKeyDown(playerScript.joystick.altAction1Input) && !IsKeyPickUp)
        {
            IsKeyPickUp = true;
            print("IsKeyPickUp is true");
        }

        float pickUpDelay = .5f;

        Invoke("SetKeyPickUpFalse", pickUpDelay);
    }

    private void SetKeyPickUpFalse()
    {
        IsKeyPickUp = false;
        print("IsKeyPickUp is false");
    }

    private void DepleteStamina(float cost)
    {
        if (staminaCool < stamina)     // use zero for depletion in stamina pass           .... matter of inverting everything       ... use this where needed after revision
        {
            staminaCool += cost;               
        }
    }

        private bool IsAction1Input()
    {
        // (joystick button 1 || h key, ... ||   ... virtual pick up..) 

        if (((Input.GetKeyDown(playerScript.joystick.action1Input) || Input.GetKeyDown(playerScript.joystick.altAction1Input)) || (playerScript.joystick.number == 1 && pickUpButton && pickUpButton.pushed) || (IsTapBall())))
        
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void CheckCatchCool()
    {
        if (!catchReady)
        {
            //  t_catchF = Time.realtimeSinceStartup;
            catchCoolDown -= Time.deltaTime;

            if (catchCoolDown <= 0)
            {
                catchReady = true;
            }
        }
    }

    private void CheckTouchInput()
    {
        if (mode == "Touch" || (mode == "Virtual Joystick" && !isVirtualJoystickButtonsPressed()))
        {
            if (Input.touchCount >= 2)
            {
                touch1 = Input.GetTouch(1);


            }
        }
    }

    private void CheckHasBallAnim()
    {
        if (!ballGrabbed)
        {                                                                               

            if (animator.GetBool("hasBall") == true)
                animator.SetBool("hasBall", false);
        }
        else
        {
            if (animator.GetBool("hasBall") == false)
                animator.SetBool("hasBall", true);
        }
    }

    private void PickUpActivate()
    {
        if (!ballGrabbed)
        {
            nearestBall = GetNearestBall();
            if (nearestBall)
            {
            if (ObjectIsInGrabDistance(nearestBall) && nearestBall.GetComponent<Ball>().grounded)
            {
                    nearestBall.GetComponent<Ball>().PickUpActivate(playerScript.color);
            }
            }
        }
        
    }

    private void HitFX(Ball ball, float hitPauseDuration, float hitPausePreDelay)
    {
        GameObject hitFX = levelManager.HitSmall;
        if (ball.chargeAlpha > .25f) hitFX = levelManager.HitMed;
        if (ball.chargeAlpha == 1f) hitFX = levelManager.HitMax;
        GameObject hfx = Instantiate(hitFX, ball.transform.position, Quaternion.identity);
        levelManager.CamShake(1f, transform);

        levelManager.SetHitPauseDuration(hitPauseDuration);
        Invoke("DoHitPause", hitPausePreDelay);
         
    }

    public void DropBall()
    {
        print("Dropping the ball");

      //  Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
      //  if (levelManager.IsInGameBounds(cockBackPos))       //      isnt vaalide if we'reusing this on restart
        {
            throwCharge = 0;
            isCharging = false;
            ballGrabbed = false;
            ballCaught = false;
            ball.GetComponent<Ball>().grabbed = false;
            ball.GetComponent<SphereCollider>().enabled = true;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<SpriteRenderer>().enabled = true;
            if (animator)
            {
                animator.SetBool("hasBall", false);
            }
        }
       
    }

    private bool isVirtualJoystickButtonsPressed()
    {
        return ((throwButton.Pressed || throwButton.pushed) || (superButton.Pressed || superButton.pushed) || (pickUpButton.Pressed || pickUpButton.pushed));
    }

    public void SetTapThrowReadyToFalse()
    {
        isTapThrowReady = false;
    }

    public void SetTapThrowReadyToTrue()
    {
        isTapThrowReady = true;
    }

    private void ResetTouch1TapCount()
    {
        touch1.tapCount = 0;
    }

    private bool IsTapBall()
    {
        
        bool returnMe = (playerIsTouched && (touch1.phase == UnityEngine.TouchPhase.Ended && touch1Phase_prev != UnityEngine.TouchPhase.Ended) && !isTapThrowReady && Input.touchCount >= 2);
        if (returnMe)
        {
            return returnMe;
        }
        else
        {
            if (Input.touchCount >= 2 && !ballGrabbed)
            {
                if ((touch1.phase == UnityEngine.TouchPhase.Stationary && touch1Phase_prev != UnityEngine.TouchPhase.Stationary))
                {
                    return true;
                }
                else
                {
                //  print("touch1Phase = " + touch1.phase);
                  //  print("touch1PhasePrev = " + touch1Phase_prev);
                    //print("isTap Throw Ready =" + isTapThrowReady);
                }
                
            }
          
            return returnMe;
        }
         
    }

    private Transform GetNearestOpp(Vector3 touch1Pos)
    {

        Transform nearestTargetedOpp = null;
        float nearest = 1000000f;

        List<GameObject> players = new List<GameObject>();
        players.AddRange(levelManager.tm1.players);
        players.AddRange(levelManager.tm2.players);

        if (GetComponentInParent<Player>().team == 1)
        {

            foreach (GameObject player in players)
            {
                if (player.GetComponent<Player>().team == 2 && player.GetComponent<Player>().isOut == false)
                {
                    Vector3 diff = player.transform.GetChild(0).transform.position - touch1Pos;
                    // Vector3 comp = rigidbody.velocity - diff;
                    if (Vector3.Magnitude(diff) < nearest)
                    {
                        nearestTargetedOpp = player.transform.GetChild(0);
                        nearest = diff.magnitude;
                    }

                }

            }
        }
        else
        {
            if (GetComponentInParent<Player>().team == 2)
            {
                foreach (GameObject player in players)
                {
                    if (player.GetComponent<Player>().team == 1 && player.GetComponent<Player>().isOut == false)
                    {
                        Vector3 diff = player.transform.GetChild(0).transform.position - touch1Pos;
                        // Vector3 comp = rigidbody.velocity - diff;
                        if (Vector3.Magnitude(diff) < nearest)
                        {
                            nearestTargetedOpp = player.transform.GetChild(0);
                            nearest = diff.magnitude;
                        }

                    }

                }
            }
        }

        return nearestTargetedOpp;
    }

    private Vector2 GetTouchSwipeVel(int touch_i)
    {
        touch_i--;

        if (Input.touchCount >= touch_i)
        {
            return Input.GetTouch(touch_i-1).deltaPosition;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private float GetTouchSwipeSpeed(int touch_i)
    {
        touch_i--;

        if (Input.touchCount>= touch_i)
        {
            return Vector2.SqrMagnitude(Input.GetTouch(touch_i-1).deltaPosition);
        }
       else
        {
            return 0f;
        }
    }

    
    

    private void ResetThrowAnimations()
    {

            animator.ResetTrigger("Release");
            animator.ResetTrigger("Charge");
    }

    private void ResetChargeAnimations()
    {

        animator.ResetTrigger("Release");
        animator.ResetTrigger("Charge");
    }

    private void Throw()   // button throw
    {
     //   print("button throw");

        Vector3 throwStandVec = Vector3.zero;
        Vector3 throwMovVec = Vector3.zero;
        bool hasSeekVec = false;

        /*
        if (GameManager.mode == "Basic" && GameManager.gameMode == "Solo")
        {
           hasSeekVec = false;
        }

        if ((throwDirection.x > 0 && playerScript.team == 1) || (throwDirection.x < 0 && playerScript.team == 2))           // need to face opp for seek vec assisance
        {
            hasSeekVec = false;
        }
        */


        if (gameObject.GetComponentInParent<Player>().team == 1)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 1);
        }
        if (gameObject.GetComponentInParent<Player>().team == 2)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 2);
        }

        Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * (collider.bounds.size.magnitude + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

        if (levelManager.IsInGameBounds(cockBackPos) && !isDodging) 
        {
            if (BallIsInGrabDistance(GetTargetedOpp().gameObject, 1f) && GetTargetedOpp()){
                cockBackPos = (cockBackPos + GetTargetedOpp().position)/2;      // tag pos  
               // print("Tag cockback");
            }

         //   ball.transform.position = cockBackPos;


            if (rigidbody.velocity.magnitude <= standingThrowThresh)
            {
                standingThrowPower = playerScript.standingThrowPower;
       
                if (hasThrowMag)
                {
                    Vector3 seekVec = new Vector3(1.0f, 0.0f, 0.0f);
                    Transform nearestOpp = GetTargetedOpp();

                    if (nearestOpp && throwDirection.x > 0 )
                    {
                        seekVec = nearestOpp.transform.position - ball.transform.position;
                        seekVec = seekVec.normalized;
                    }

                    throww = seekVec * (standingThrowPower + throwCharge);
                    print("Standing Throw . mag");

                }
                else
                {


                    Vector3 velVec = rigidbody.velocity;
                    float xClamped = velVec.x;

                    if (velVec.x > -1.0f && velVec.x < 1.0f)
                    {
                        xClamped = throwDirection.x;
                    }


                    velVec = new Vector3(xClamped, velVec.y, velVec.z);
                    //  Vector3 velNorm = Vector3.Normalize(rigidbody.velocity);
                    //  print("veNorm = " + velNorm);
                    print("velVec = " + velVec);
                    throww = new Vector3( velVec.x * (standingThrowPower + throwCharge), 5f, (velVec.z) * (standingThrowPower + throwCharge));
                   print("Standing Throw . no mag");
                }
            }

            else                   //moving throw
            {

                Transform nearestOpp = GetTargetedOpp();

                if (hasThrowMag)
                {
                    Vector3 seekVec = Vector3.one;
                
                    if (nearestOpp) {

                      seekVec = nearestOpp.transform.position - ball.transform.position;
                      seekVec = new Vector3(Mathf.Clamp(seekVec.x, -maxSeekVec, maxSeekVec), seekVec.y, Mathf.Clamp(seekVec.z, -maxSeekVec, maxSeekVec));
                    }

                 
                    throww = rigidbody.velocity * throwPower;
                    throww = new Vector3(throww.x, 2.5f, throww.z);


                       // if (GameManager.mode == "Basic" && GameManager.gameMode == "Solo")       
                    {
                      //  print("moving Throw  . mag");
                        throww = GetThrowAid(throww, seekVec);
                        throww = new Vector3((throwPower + throwCharge) * rigidbody.velocity.x, 5f, rigidbody.velocity.z* (throwPower + throwCharge));

                    }

                }

                else
                {
                   print("moving Throw  . no mag");
                    Vector3 movingThrowVec = new Vector3((throwPower + throwCharge) * rigidbody.velocity.x, 5f, rigidbody.velocity.z * (throwPower + throwCharge));
                    throww = movingThrowVec;

                }
            }

            if (animator)
            {
                float throwMag = Vector3.Magnitude(throww);
                float mack3ThrowSpeedThresh = 3500f;

                float throwAnimSpeed = Mathf.Clamp(throwMag/mack3ThrowSpeedThresh, 1.25f, 1.75f);
              //  print("throwAnimSpeed " + throwMag / mack3ThrowSpeedThresh);

                animator.SetFloat("ThrowSpeed",throwAnimSpeed );

                if (isJumping)
                {
                    animator.SetTrigger("Air Throw");
                }
                else
                {
                    animator.SetTrigger("Release");       // <-   speed scale this
                }
            }


            Transform targetedOpp = GetTargetedOpp();
            float renderLength = GetRenderLength();
            print("throww = " + throww);
          //  print("targetedOpp = " + targetedOpp);
         //   print("hasThrowMag = " + hasThrowMag);
            // print("throw Magnetism = "+throwMagnetism);
             ball.GetComponent<Ball>().Throw(throww, playerScript.color, hasSeekVec, throwMagnetism, targetedOpp, renderLength, ChargePowerAlpha);

            playerScript.playThrowSound();
            playerScript.playThrowSound();
            levelManager.AddThrow(ball, parent);
            ballGrabbed = false;
            ballCaught = false;
            throwCharge = 0;
            isCharging = false;
            chargeVel = Vector3.zero;



            if (animator)
            {
                animator.SetBool("hasBall", false);
            }
        }
    }

    private Vector3 GetThrowAid(Vector3 throww,Vector3 seekVec)
    {
        Vector3 returnMe = (throww + seekVec) / 2;

         for (int i =0; i< levelManager.level; i++)
            {
                returnMe = (returnMe + throww) / 2;
            }

        return returnMe;

    }


    private float GetRenderLength()
    {
        float clipLength = .0125f;
        foreach (AnimationClip ac in animator.runtimeAnimatorController.animationClips)
        {
            if (ac.name == "Mack.Ball.2.Throw" || ac.name == "King.2.Throw" || ac.name == "Nina.2.Throw"){
             //   clipLength = ac.length/10;    // *arbitary nums
            }
        }
        return clipLength;

    }

    private void Throw(Vector3 throww, String type, float mag)
    {
        if (playerScript.team == 1)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 1);
        }

        if (playerScript.team == 2)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 2);
        }

        Vector3 cockBackPos = new Vector3(transform.position.x + throwDirection.x * (collider.bounds.size.magnitude + handSize.x), transform.position.y + 1, transform.position.z);

        //ball.transform.position = cockBackPos;
       //print("cockBackPos = " + cockBackPos);



        if (animator)
        {

            float throwMag = Vector3.Magnitude(throww);
            float throwSpeedThresh = 300f;

            float throwAnimSpeed = Mathf.Clamp(throwMag / throwSpeedThresh, 2f, 3f);
            animator.SetFloat("ThrowSpeed", throwAnimSpeed);
            animator.SetTrigger("Charge");
        }

        float magnetism = mag;

        if (superPackage || type == "Super")
        {
            if (superPackage.GetComponent<SuperBall>().type == 1)
            {
                magnetism = superPackage.GetComponent<SuperBall>().superMagnetism;
            }
            else
            {
                if (superPackage.GetComponent<SuperBall>().type == 2)
                {
                    magnetism = superPackage.GetComponent<SuperTechBall>().seekMagnetism;
                }
                   
            }
        }

        Transform targetedOpp = GetTargetedOpp();
        float renderLength = GetRenderLength();

        ball.GetComponent<Ball>().Throw(throww, playerScript.color, true, magnetism,targetedOpp,renderLength, ChargePowerAlpha);
        levelManager.AddThrow(ball, parent);
        ballGrabbed = false;
        ballCaught = false;
        throwPower = gameObject.GetComponentInParent<Player>().GetThrowPower0();

        if (animator)
        {
            animator.SetBool("hasBall", false);
        }
    }

    private Transform GetTargetedOpp()
    {

        Transform nearestTargetedOpp = null;
        float nearest = 1000000f;
        if (GetComponentInParent<Player>().team == 1)
        {
           
            foreach (GameObject player in levelManager.tm2.players)
            {
                if ( player.GetComponent<Player>().isOut == false)
                {
                    Vector3 diff = player.transform.GetChild(0).position - playerConfigObject.transform.position;
                   // Vector3 comp = rigidbody.velocity - diff;
                    if (Vector3.Magnitude(diff)< nearest)
                    {
                        nearestTargetedOpp = player.transform.GetChild(0);
                        nearest = diff.magnitude;
                    }

                }
               
            }
        }
        else
        {
            if (GetComponentInParent<Player>().team == 2)
            {
                foreach (GameObject player in levelManager.tm1.players)
                {
                    if  (player.GetComponent<Player>().isOut == false)
                    {
                        Vector3 diff = player.transform.GetChild(0).transform.position - playerConfigObject.transform.position;
                       // Vector3 comp = rigidbody.velocity - diff;
                        if (Vector3.Magnitude(diff) < nearest)
                        {
                            nearestTargetedOpp = player.transform.GetChild(0);
                            nearest = diff.magnitude;
                        }

                    }

                }
            }
        }
       
        return nearestTargetedOpp;
    }

    void SuperInput()
    {
        //if mode is basic or advanced
      //  if (GameManager.mode == "Basic")
        {
            if (superCoolDown > 0)
            {

                t_sF = Time.realtimeSinceStartup;
                superCoolDown -= Time.deltaTime;

                float superTime = 0f;

                if (playerScript.super.GetComponent<SuperScript>().type == 1 || playerScript.super.GetComponent<SuperScript>().type == 2)
                {

                    if (superPackage)
                    {
                        superTime = superPackage.GetComponent<SuperBall>().superTime;
                    }
                }
                else
                {
                    if (playerScript.super.GetComponent<SuperScript>().type == 3)
                    {
                        superTime = playerScript.super.GetComponent<SuperSpeed>().superTime;
                    }
                }

                if (t_sF - t_s0 >= superTime && isSupering)
                {
                    SuperDeactivate();
                }
            }

        if ((Input.GetButtonDown(playerScript.joystick.superInput) || Input.GetKeyDown(playerScript.joystick.altSuper1Input) || (playerScript.joystick.number == 1 && superButton && superButton.Pressed)) && superCoolDown <= 0 && ballGrabbed)   // super activate
                {
                SuperActivate();
            }
        }
        
    }

    

    private void SuperDeactivate()
    {
        isSupering = false;
        animator.SetBool("Supering", false);


        if (playerScript.super.GetComponent<SuperScript>().type == 1 || playerScript.super.GetComponent<SuperScript>().type == 2)
        {
            foreach (Transform t in ballSupered.transform.GetComponentInChildren<Transform>()) //assumes there is only one ball supered at a time
            {
                ballSupered.GetComponent<Ball>().Normalize();
                if (t.gameObject.tag == "SuperBall")
                    Destroy(t.gameObject);
            }

        }
        else
        {
            if (playerScript.super.GetComponent<SuperScript>().type == 3)     //Nina
            {
                xSpeed = playerScript.GetComponent<Player>().xspeed;
                zSpeed = playerScript.GetComponent<Player>().zspeed;
            }
        }
    }

    private void SuperActivate()
    {
        t_s0 = Time.realtimeSinceStartup;
        isSupering = true;
        superCoolDown = transform.parent.gameObject.GetComponent<Player>().power;
      
        int superType = playerScript.super.GetComponent<SuperScript>().type;

        float throwMag = 0;

        if (superType == 1 || superType == 2)
        {

            ballSupered = ball;
            superPackage = Instantiate(playerScript.super);
            ballSupered.GetComponent<Ball>().SuperInit(superPackage);
            superPackage.transform.parent = ballSupered.transform;
            superPackage.transform.localPosition = Vector3.zero;

            if (superType == 2)
            {
                if (throwDirection.x == 1)
                {
                    superPackage.transform.rotation = new Quaternion(0f, 180f, 0f,0f);
                }
                else
                {
                    superPackage.transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
                }

            }

           

            

          //  if (GameManager.mode == "Basic")
            {
                if (superType == 1) // Mack (Ball Expansion)
                {
                    Transform nearestOpp = GetTargetedOpp();
                    Vector3 seekVec = nearestOpp.transform.position - ball.transform.position;

                    print("ball = " + ball.transform.position);
                    print("nearestOpp transfrom = " + nearestOpp.transform.position);
                    print("seekVec = " + seekVec);
                    throwMag = superPackage.GetComponent<SuperBall>().superMagnetism;
                    // throww = seekVec * throwPower * superPackage.GetComponent<SuperBall>().throwPowerMult;
                    throww = seekVec.normalized * 10000;
                  //  throww = new Vector3(throww.x, 5f, throww.z);
                }

                if (superType == 2)  // King (Super Tech Ball) 
                {

                throww = new Vector3(throwPower * rigidbody.velocity.x, 4f, throwPower * rigidbody.velocity.z);

                }

                Throw(throww, "Super", throwMag);
            }
           
        }
        else
        {
            if (superType == 3)  // Nina 
            {
                float superTime = playerScript.super.GetComponent<SuperSpeed>().superTime;
                float superBoost = playerScript.super.GetComponent<SuperSpeed>().speedBoost;
                SpeedBoost(superTime, superBoost);
            }
        }

    }

    void BlockInput()
    {

        if (staminaCool < playerScript.stamina - 40 && isBlocking)                   // important feature to revitalize and take into account which involves tag and blocking
        {
            isBlocking = false;
            ball.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetButtonDown(playerScript.joystick.blockInput) && staminaCool <= 0 && ballGrabbed)
        {
            isBlocking = true;
            staminaCool = playerScript.stamina;
            ball.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (isBlocking)
        {
            float nuBallX = transform.position.x + throwDirection.x * handSize.x * 1.25f;
            float nuBallY = transform.position.y;
            //left handed or right handed
            float nuBallZ = transform.position.z;
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.transform.position = new Vector3(nuBallX, nuBallY, nuBallZ);
        }
    }

    private bool ThrownByOpp(GameObject ball, int team)
    {
        if (team == 2)
        {
            if (ball.GetComponent<Ball>().thrownBy1 && gameObject.GetComponentInParent<Player>().team == 2)
            {
                ball.GetComponent<Ball>().thrownBy1 = false;
                return true;
            }

        }
        if (team == 1)
        {
            if (ball.GetComponent<Ball>().thrownBy2 && gameObject.GetComponentInParent<Player>().team == 1)
            {
                ball.GetComponent<Ball>().thrownBy2 = false;
                return true;
            }

        }
        return false;
    }

    public bool ObjectIsInGrabDistance(GameObject nearest)
    {
        if (playerConfigObject.transform.position.x + grabRadius > nearest.transform.position.x &&
                 playerConfigObject.transform.position.x - grabRadius < nearest.transform.position.x)
        {
            if (playerConfigObject.transform.position.y + grabRadius > nearest.transform.position.y &&
                playerConfigObject.transform.position.y - grabRadius < nearest.transform.position.y)
            {
                if (playerConfigObject.transform.position.z + grabRadius > nearest.transform.position.z &&
                    playerConfigObject.transform.position.z - grabRadius < nearest.transform.position.z)
                {
                    float angle = 180f;
                  //  if (Vector3.Angle(playerConfigObject.transform.forward, nearest.transform.position - playerConfigObject.transform.position) < angle)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool BallIsInGrabDistance(GameObject nearest, float v)
    {
        if (playerConfigObject.transform.position.x + grabRadius*v > nearest.transform.position.x &&
                   playerConfigObject.transform.position.x - grabRadius * v < nearest.transform.position.x)
        {
            if (playerConfigObject.transform.position.y + grabRadius * v > nearest.transform.position.y &&
                playerConfigObject.transform.position.y - grabRadius * v < nearest.transform.position.y)
            {
                if (playerConfigObject.transform.position.z + grabRadius*v > nearest.transform.position.z &&
                    playerConfigObject.transform.position.z - grabRadius*v < nearest.transform.position.z)
                {
                    if ((IsAboutToCollideWBall(nearest) && nearest.GetComponent<Ball>().thrown == false) && !inBallPause && ballPauseReady)
                    {
                        inBallPause = true;
                        float ballPauseTime = .5f;                                // gr
                        Invoke("TurnInBallPauseFalse", ballPauseTime);        
                        ballPauseReady = false;
                    }

                    float angle = 180f;
                    if (Vector3.Angle(playerConfigObject.transform.forward, nearest.transform.position - playerConfigObject.transform.position) < angle)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void TurnInBallPauseFalse()
    {
        inBallPause = false;
        accelerationTime = .25f;
        Invoke("SetBallPauseReadyTrue", 1f);
    }

    private void SetBallPauseReadyTrue()
    {
        accelerationTime = .85f;
        ballPauseReady = true;
    }

    private bool IsAboutToCollideWBall(GameObject Ball)
    {
        float thresh = 6.0f;
       // print("Ball distance mag check = " + Vector3.Magnitude(playerConfigObject.transform.position - nearestBall.transform.position));

      if (Vector3.Magnitude(playerConfigObject.transform.position - nearestBall.transform.position) < thresh && IsFacingObj(ball))
        {
            return true;
        }
      else
        {
            return false;
        }
    }

    private GameObject GetNearestBall()                       // nearest legal
    {
        GameObject nearestBall = null;
        Vector3 smallest = new Vector3(10000f, 10000f, 10000f);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (Vector3.Magnitude(playerConfigObject.transform.position - ball.transform.position) < smallest.magnitude)
            {
                if (ball.GetComponent<Ball>().isSupering == false && !ball.GetComponent<Ball>().grabbed)
                {
                    smallest = playerConfigObject.transform.position - ball.transform.position;
                    nearestBall = ball;
                }
            }
        }
        return nearestBall;
    }

    void OnCollisionEnter(Collision collision) {           

        if (gameObject.GetComponent<Controller3D>().enabled == true) {
            if (collision.gameObject.tag == "Ball") {

                var ball = collision.gameObject.GetComponent<Ball>();

                    if (ball.CheckPlayerHit(playerScript.team)) {

                        TriggerHeadHit();

                        ballContact = true;

                        t_contact0 = Time.realtimeSinceStartup;

                        ballHit = collision.gameObject;
                        ballHit.GetComponent<Ball>().contact = true;
                        float ballVelocity = Mathf.Clamp(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude / 9, 3, 6);

                        ParticleSystem ball_hit_ps = collision.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
                        ball_hit_ps.GetComponent<Renderer>().sortingOrder = 3;
                        ball_hit_ps.startSize = ballVelocity;

                        if (ball.isSupering)
                        {
                            TriggerKnockBack(collision.gameObject.GetComponent<Rigidbody>().velocity);
                            ParticleSystem.MainModule sup_main_ps = collision.gameObject.GetComponentInChildren<ParticleSystem>().main;
                            sup_main_ps.startSize = 4;
                            sup_main_ps.simulationSpeed = 20f;
                            sup_main_ps.startSizeX = 10f;
                            sup_main_ps.startSizeMultiplier = 10f;
                        }

                        if (!(collision.gameObject.GetComponent<TrailRenderer>().startWidth == 2f)) {
                            if (collision.gameObject.GetComponent<TrailRenderer>().enabled == false) {
                                collision.gameObject.GetComponent<TrailRenderer>().enabled = true;
                            } else {
                                collision.gameObject.GetComponent<TrailRenderer>().startWidth = 2f;
                            }
                        }

                        float hitPauseDuration = ballVelocity / 25f;                 // * arbitrays nums
                        float hitPausePreDelay = .125f;

                        HitFX(ball, hitPauseDuration, hitPausePreDelay);

                        levelManager.AddHit(collision.gameObject, parent);
                        levelManager.HitDisplay(gameObject, collision.gameObject);
                        float shakeIntensity = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                        levelManager.CamShake(shakeIntensity,transform);
                        print("~!CONTACT!~ 2");
                    }

                    else
                    {
                        rigidbody.velocity = Vector3.zero;
                        Vector3 diff = (transform.position - collision.transform.position) * pushVal;
                        Vector3 nuPos = new Vector3(transform.position.x+diff.x, transform.position.y-.025f, transform.position.z +diff.z);
                        rigidbody.MovePosition(nuPos);
                    }
            }


            // TODO doesnt make smoothe as intended 
            if (collision.gameObject.tag == "Wall") {
                wallCollision = true;
                rigidbody.AddForce(-velocity.x, 0, -velocity.z);
            }


            if (collision.gameObject.tag == "Playing Level")
            {
                onGround = true;
                if (isJumping)
                {
                    isJumping = false;
                    if (animator) {
                        animator.SetBool("Jumping", false);
                    }
                }
            }

            if (collision.gameObject.tag == "Player Sprite")
            {
              rigidbody.velocity = Vector3.zero;
                Vector3 pushBack = (transform.position - collision.transform.position);
                rigidbody.AddForce(pushBack * pushVal);
                print("Ouch!");

            }


        }
    }

    private void PostFX(string type)
    {
        levelManager.PostFX(type);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Playing Level") { 
            onGround = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Playing Level")
        {
            onGround = true;
        }
    }


    private void TriggerKnockBack(Vector3 ballVelocity)
    {
        rigidbody.AddExplosionForce(ballVelocity.magnitude, ballVelocity, ballVelocity.magnitude / 10);
        knockedOutTime = 3f;
        t_knock0 = Time.realtimeSinceStartup;
        isKnockedOut = true;
        // animator.SetTrigger("Knock Out");
        animator.SetTrigger("Head Hit");


    }

    public bool InBounds(){
		inBounds = true;
		if (gameObject.GetComponentInParent<Player>().team ==1) {
			if ( collider.bounds.min.x < levelManager.stage.baseLineLeft) {
				playerConfigObject.transform.position = new Vector3 (levelManager.stage.baseLineLeft + collider.bounds.extents.x + 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
				rigidbody.velocity = new Vector3 (0f, rigidbody.velocity.y, rigidbody.velocity.z);
				inBounds = false;
				print ("Out of Bounds 1");

			}
			if ( collider.bounds.max.x > levelManager.stage.halfCourtLine) {
                playerConfigObject.transform.position = new Vector3 (levelManager.stage.halfCourtLine - collider.bounds.extents.x- 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
				rigidbody.velocity = new Vector3 (0f, rigidbody.velocity.y, rigidbody.velocity.z);
				inBounds = false;
				print ("Out of Bounds 1");

			}
		}

		if (gameObject.GetComponentInParent<Player>().team ==2) {
			if ( collider.bounds.min.x < levelManager.stage.halfCourtLine) {
                playerConfigObject.transform.position = new Vector3 (levelManager.stage.halfCourtLine + collider.bounds.extents.x+ 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
				rigidbody.velocity = new Vector3 (0f, rigidbody.velocity.y, rigidbody.velocity.z);
				inBounds = false;
				print ("Out of Bounds 2");

			}
			if ( collider.bounds.max.x > levelManager.stage.baseLineRight) {
                playerConfigObject.transform.position = new Vector3 (levelManager.stage.baseLineRight - collider.bounds.extents.x- 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
				rigidbody.velocity = new Vector3 (0f, rigidbody.velocity.y, rigidbody.velocity.z);
				inBounds = false;
				print ("Out of Bounds 2");

			}
		}
			
			
		return inBounds;
	}



	private void SetDodgingF (){                       // ?
		isDodging = false;
        playerScript.ToggleActivateDodge ();
	}
    internal void TriggerHeadHit()
    {
        if (animator)
        {
            animator.SetTrigger("Head Hit");
        }
    }

    internal void ResetTouch1PhasePrev()
    {
        touch1Phase_prev = UnityEngine.TouchPhase.Canceled;
    }

    public void FaceOpp()
    {
        bool isFacingRight = !spriteRenderer.flipX;

        if (playerScript.team == 1 && !isFacingRight)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else
        {
            if (playerScript.team == 2 && isFacingRight)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            }
         }

    }

