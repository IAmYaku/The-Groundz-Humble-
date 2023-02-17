using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Controller3D : MonoBehaviour
{


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

    private ChargeVelInput chargeVelInput = new ChargeVelInput();

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
    private Vector3 velVec;
    private Vector3 vel0;

    float accelerationRate = .85f;
    bool isSlowingDown;

    Vector3 chargeVel;

    public float throwPower = 200f;
    float standingThrowThresh = 20f;

    public float maxThrowPower = 240f;
    public float maxStandingThrowPower = 1600;

    float throwCharge;
    bool isCharging;
    float chargeTime = 0.0f;

    public Vector3 handSize = new Vector3(3f, 3f, 3f);
    public float grabRadius = 5f;
    public static float grabHelpMultiplier = 1f; // gr or diff
    Vector3 cockBackPos;

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
    public static bool hasSeekVec = false;
    public static float throwMagnetism = 0f;
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
    private float pushVal = .265f;

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
    bool dodgeThrowDelay;
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
    void Start()
    {

         GameObject gameManagerObject = GameObject.Find("GameManager");

        if (gameManagerObject)
        {
            levelManager = gameManagerObject.GetComponent<LevelManager>();
        }

        else
        {
            gameManagerObject = GameObject.Find("GameManager(Clone)");

            if (gameManagerObject)
            {
                levelManager = gameManagerObject.GetComponent<LevelManager>();
            }
        }


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

       // cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * (collider.bounds.size.magnitude + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
      //  Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

    }

    internal void DisablePlayerInputControls()
    {
        playerInput.DeactivateInput();
    }
    internal void EnablePlayerInputControls()
    {
        playerInput.ActivateInput();
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

      //  swipeInput.SetMoves(SwipeInput.bufferSize);
       // vJSwipeInput.SetMoves(SwipeInput.bufferSize);

        //  touchFX = playerScript.playerConfigObject.transform.GetChild(2).gameObject.GetComponent<TouchFX>();


    }




    void Update()
    {


        if (levelManager.isPlaying && !playerScript.isOut)
        {
            CheckKeyInput();
            CheckVelocity();
            CheckStamina();
            GrabInput();
            CheckSuperCool();
            HandleContact();
            // HandlePerks(perkDur,1f);


        }
        else
        {
            rigidbody.velocity = Vector3.zero; 
        }
    }

    private void CheckKeyInput()
    {
        int playerIndex = playerScript.GetJoystick().number;

        if (playerIndex == -1)
        {
            CheckKeyMove();
            CheckKeyGrab();
            CheckKeyDodge();
            CheckKeySuper();

        }
    }

    private void CheckKeySuper()
    {

        int superType = playerScript.super.GetComponent<SuperScript>().type;

        CheckSuperCool();

        if (CheckSuperInputDown() && superCoolDown <= 0 && ballGrabbed && !isDodging)   // super activate
        {
            SuperActivate();
        }

        if (CheckSuperInput() && ballGrabbed && isSupering)                   // super charge   ... iffy when conisdering if isSupering finishes before SuperAutoRelease
        {
            SuperCharge(superType);
        }

        if (CheckSuperInputRelease() && ballGrabbed && isSupering)                        // superRelease
        {
            SuperRelease(chargeVel.magnitude, superType);
        }
    }

    private void CheckKeyDodge()
    {
        if ((Input.GetKeyDown(playerScript.joystick.altDodge1Input)))
        {
            Dodge();
        }
    }


    private void CheckKeyGrab()
    {
        if (Input.GetKeyDown(playerScript.joystick.altAction1Input))    
        {
            CheckGrab();
        }


        //charge 

        if (ballGrabbed && Input.GetKeyDown(playerScript.joystick.altAction1Input) && !IsKeyPickUp)
        {

            if (!isCharging)
            {
                chargeVel = rigidbody.velocity;
                chargeVelInput.Input(chargeVel.x, chargeVel.z);
                isCharging = true;

                float glide = chargeVel.magnitude/100f;
                accelerationRate = Mathf.Clamp(glide, 0.000001f, 1.0f);
                print("accelerationRate = " + accelerationRate);


            }

        }

        else
        {
            //release

            if (Input.GetKeyUp(playerScript.joystick.altAction1Input) && !IsKeyPickUp && ballGrabbed)
            {
                Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

                if (levelManager.IsInGameBounds(cockBackPos))
                {
                    if (!dodgeThrowDelay)
                    {

                        float mackThrowDelay = .1f;
                        //  Invoke("Throw", mackThrowDelay);
                        Throw();
                    }
                    else
                    {
                        DodgeThrow();
                    }
                }


                if (animator)
                {
                    float mackThrowDelay = .1f;
                    animator.SetBool("hasBall", false);
                    Invoke("ResetThrowAnimations", .05125f);    //arbs
                }

                float throwSlowDownfactor = .00025f;
                float stallTime = .1f;
                SlowDownByVelocity(throwSlowDownfactor, stallTime);

            }
        }
        
    }

    private void CheckKeyMove()
    {
        {

            Vector3 keyMuv = Vector2.zero;

            float keyLeftVal = 0f;
            float keyRightVal = 0f;
            float keyUpVal = 0f;
            float keyDownVal = 0f;


            Vector2 joyMuv = Vector2.zero;

            joyMuv = new Vector2(Input.GetAxis(playerScript.joystick.horzInput), Input.GetAxis(playerScript.joystick.vertInput));
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

            move.x = keyMuv.x;
            move.z = keyMuv.z;

           // print("Keyboard Move: " + move);
        }
    }

    void CheckVelocity()
    {
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

                        float slowDownThresh = 10f;
                        float velMag = rigidbody.velocity.magnitude;
                        // print("VelMag = " + velMag);

                        if (!ballGrabbed && IsInGrabDistance(nearestBall, "ball") && (velMag > slowDownThresh))
                        {
                            if (!nearestBall.GetComponent<Ball>().thrown && IsFacingObj(nearestBall))
                            {
                                //  print("Slowing down for ball");
                                float grabHelpSlowDownfactor = .000125f + velMag / 1000f;     // should be game level dependent
                                float stallTime = .1f;    
                                  //print("grabHelpSlowDownFactor = "+ grabHelpSlowDownfactor);
                                  SlowDownByVelocity(grabHelpSlowDownfactor,stallTime);

                            }
                        }

                        // Slow down for Player Collision

                        if (PlayerNear() && rigidbody.velocity.magnitude > slowDownThresh)     // can impeded on tag idea
                        {
                            //   if (GetComponent<Raycast>().isOnline)
                            {
                                //     print("Slowing down for player");
                                float playerSlowDownfactor = .00005f;
                                float stallTime = .01f;
                                SlowDownByVelocity(playerSlowDownfactor, stallTime);

                            }
                        }

                        //Slow Down for Wall

                        if (IsWallNear())
                        {
                            float wallSlowDownfactor = .00005f;
                            float stallTime = .01f;
                            SlowDownByVelocity(wallSlowDownfactor,stallTime);
                        }

                        if (!inBallPause && !isDodging)
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

    private void CheckStamina()
    {
        if (rigidbody.velocity.magnitude < 3f)             // *arb = moveThresh
        {

            if (staminaCool > 0.0f)      // should invert ... i.e - cost, as opposed to + cost
            {
                staminaCool -= staminaCoolRate;        //  *Should go time dependent
            }
        }
    }

    public void MoveInput(CallbackContext context)
    {

        if (!GlobalConfiguration.isAtRevampTeamSelect && !GlobalConfiguration.isAtQuickCharacterSelect) { 
        if (levelManager.isPlaying && !playerScript.isOut)
        {
            Vector2 ctxValue = context.ReadValue<Vector2>();

            move = new Vector3(ctxValue.x, 0f, ctxValue.y);

            playerConfigObject.transform.localEulerAngles = new Vector3(playerConfigObject.transform.localEulerAngles.x, 0f, playerConfigObject.transform.localEulerAngles.z);

                //print("JoyInput = " + ctxValue);
            }
        }
    }

    //handle Dodge/Jump Input
    public void DodgeInput(CallbackContext context)
    {
        if (levelManager.isPlaying && !playerScript.isOut)
        {
            if (context.performed)
            {
                Dodge();
            }
        }
    }

    private void Dodge()
    {
        float dodgeStaminaThresh = stamina / 2f;

        {
            if (InBounds() && staminaCool <= dodgeStaminaThresh)
            {
                //Dodge
                if (IsInDodgeRange() && !isDodging)
                {
                    if (Mathf.Abs(move.z) > Mathf.Abs(move.x))
                    {
                        print("Dodge!");
                        isDodging = true;
                        dodgeThrowDelay = true;

                        if (animator)
                        {
                            animator.SetTrigger("Dodge");
                        }

                        staminaCool += staminaDodgeCost;
                        rigidbody.AddForce(new Vector3(0f, jumpSpeed / 4f, Mathf.Sign(rigidbody.velocity.z) * dodgeSpeed) * 75, ForceMode.Impulse);  //*arb
                        playerScript.PlayDodgeSound();

                        float dodgeCool = .5f + (rigidbody.velocity.magnitude / 1000f);
                        float dodgeThrowDelayCool = 1f + (rigidbody.velocity.magnitude / 1000f);
                        Invoke("SetDodgingF", dodgeCool);
                        Invoke("SetDodgeThrowDelayF", dodgeThrowDelayCool);


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
                        rigidbody.AddForce(new Vector3(0f, jumpSpeed / 4f, GetDodgeDirection() * 100.0f * dodgeSpeed), ForceMode.Impulse);  //*arb
                        playerScript.PlayDodgeSound();

                        float dodgeCool = .5f + (rigidbody.velocity.magnitude / 1000f);
                        float dodgeThrowDelayCool = 1f + (rigidbody.velocity.magnitude / 1000f);
                        Invoke("SetDodgingF", dodgeCool);
                        Invoke("SetDodgeThrowDelayF", dodgeThrowDelayCool);
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
            acceleration = Mathf.Clamp((Mathf.Log(accelLogCount, accelerationCurve) + logOffset) * logMult, 1f, accelerationCap);
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
        float moveThresh = .2f;

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


    public void SlowDownByVelocity( float decelerationRate, float decelerationTime)
    {
        if (!isSlowingDown)
        {
            isSlowingDown = true;
            accelerationRate = decelerationRate;
            Invoke("NormalAccelerationRate", decelerationTime);
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

    private float ReMap(float value, float from1, float to1, float from2, float to2)
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
            bool oCIsOut = other.GetComponent<Player>().isOut;

            if (otherNum != number)
            {
                if (ObjectIsInGrabDistance(otherConfig) && !oCIsOut)
                {
                   // print("Player Near");
                    return true;
                }
            }

        }
        return false;
    }

    private void ApplyVelocity(Vector3 move)
    {
              float maxKoJVel = 4f;

            //float xVelocity = move.x * Time.deltaTime * (Mathf.Pow(xSpeed, 1f + joyInput.muvXceleration * acceleration));
            // float zVelocity = move.z * Time.deltaTime * (Mathf.Pow(zSpeed, 1f + joyInput.muvXceleration * acceleration));     

            //   float xVelocity = Mathf.Clamp(move.x * Time.deltaTime * xSpeed * xCelerate, -maxKoJVel, maxKoJVel);
            //    float zVelocity = Mathf.Clamp(move.z * Time.deltaTime * zSpeed * zCelerate, -maxKoJVel, maxKoJVel);

            // float xCelerate = (Mathf.Pow(1.85f, 1.5f + joyInput.muvXceleration * acceleration));
            // float zCelerate = (Mathf.Pow(1.85f, 1.5f + joyInput.muvXceleration * acceleration));

            float muvXcel_x = Mathf.Abs(joyInput.GetMuvDelta().x);
            float muvXcel_z = Mathf.Abs(joyInput.GetMuvDelta().y);
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

        float timeMultiplier = 300f;

        float xVelocity = move.x * frameMult * xSpeed * xMultiplier * acceleration * Time.deltaTime  * timeMultiplier;
        float zVelocity = move.z * frameMult * zSpeed * zMultiplier * acceleration * Time.deltaTime * timeMultiplier;



            // rigidbody.velocity =  new Vector3(xVelocity,rigidbody.velocity.y, zVelocity);
            //  print("xVeclocity = " + xVelocity);
            //  print("zVeclocity = " + zVelocity);

        // rigidbody.AddForce(move.x * xCelerate , 0 , move.z * zCelerate, ForceMode.VelocityChange);

        if (isSlowingDown || isCharging)
            {
                if (isCharging)
                {

                velVec = new Vector3(xVelocity, 0f, zVelocity) / (1 + chargeTime);

                if (vel0.magnitude == 0)
                {
                    vel0 = chargeVel;
                }

                 //   Vector3 followThroughVec = (velVec + vel0) / (1+ (chargeTime * 100f));
                Vector3 followThroughVec = (velVec + vel0)/2;

                Vector3 chargeVelVec = Vector3.Lerp(followThroughVec, Vector3.zero, accelerationRate);

                    rigidbody.velocity = chargeVelVec;

                    vel0 = rigidbody.velocity;

                print("velVec = " +velVec);
                print("chargeVel = " + chargeVel);
                print("followThroughVec = " + followThroughVec);
                print("chargeVelVec = " + chargeVelVec);

            }
                else
                {
                    rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, accelerationRate);

                }
            }

            else
            {
                velVec = new Vector3(xVelocity, 0f, zVelocity);
                rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, velVec, accelerationRate);
            }          

        // clamp velcocity to max velocity
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            //rigidbody.velocity /= 1.5f;
            // print("rigidbody.velocity.mag = " + rigidbody.velocity.magnitude);
        }

    }

    public void GrabInput(CallbackContext context)
    {
        if (levelManager.isPlaying && !playerScript.isOut )
        {
            if (context.performed)
            {
                CheckGrab();
            }

        }
    }

    void CheckGrab()
    {
        if (!ballGrabbed)                                       // ~ pick up /catch              
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
                            levelManager.CatchDisplay(playerScript.color, playerConfigObject.transform.position, (Vector3.Magnitude(rigidbody.velocity) + Vector3.Magnitude(velocityCaught))/2f);
                            ballComp.DeactivateThrow();



                            float hitPauseDuration = velocityCaught.magnitude / 100f;
                            float hitPausePreDelay = .25f;

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
            float action1SlowDownfactor = Mathf.Clamp(.0001f - (rigidbody.velocity.magnitude) / 10000f, .0001f, 100f);     //split to ready pickUp catch and character attribute specific
            float stallTime = .1f;
            SlowDownByVelocity(action1SlowDownfactor,stallTime);
   
        }

        else
        {
            // drop
            int playerIndex = playerScript.GetJoystick().number;

            if (ballGrabbed && playerIndex != -1 && catchReady)
            {
                
                DropBall();

                catchReady = false;
                catchCoolDown = catchLagTime;

                float dropSlowDownfactor = .0005f;
                float stallTime = .1f;
                SlowDownByVelocity(dropSlowDownfactor, stallTime);

                
            }
        }
    }

    public void ThrowInput(CallbackContext context)
    {
        if (levelManager.isPlaying && !playerScript.isOut)
        {
            //charge 

            if (ballGrabbed && context.started)
            {
                if (!isCharging)
                {
                    chargeVel = rigidbody.velocity;
                    isCharging = true;

                    float glide = .01f - chargeVel.magnitude / 100000f;     //arbs
                    accelerationRate = Mathf.Clamp(glide, 0.000001f, 1.0f);  //arbs
                }

            }

            //release

            if (context.canceled && ballGrabbed)
            {
                Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

                if (levelManager.IsInGameBounds(cockBackPos))
                {
                    if (!dodgeThrowDelay)
                    {

                        float mackThrowDelay = .1f;
                        //  Invoke("Throw", mackThrowDelay);
                        Throw();
                    }
                    else
                    {
                        DodgeThrow();
                    }
                }


                if (animator)
                {
                    float mackThrowDelay = .1f;
                    animator.SetBool("hasBall", false);
                    Invoke("ResetThrowAnimations", .05125f);    //arbs
                }

                float throwSlowDownfactor = .25f;
                float stallTime = .1f;
                //SlowDownByVelocity(throwSlowDownfactor,stallTime);

            }
        }
    }

    void GrabInput()
    {
        // A ~ Pick up/Drop, X~ Throw Mechanic


        PickUpActivate();

        CheckHasBallAnim();

        CheckCatchCool();

        CheckCharge();

        MoveBall();
    }

    private void CheckCharge()
    {
        float chargeRate = 1000;  // character dependent??
        float chargeCost = .25f;

        if (ballGrabbed && isCharging)
        {

            throwCharge += (chargeRate * Time.deltaTime) + chargeVel.magnitude /100f;
            chargeTime += Time.deltaTime;
            // throwCharge = Mathf.Clamp(throwCharge, 0f, maxStandingThrowPower - standingThrowPower);

            chargeVelInput.Input(rigidbody.velocity.x, rigidbody.velocity.z);

            DepleteStamina(chargeCost);

            animator.SetTrigger("Charge");
            // Invoke("ResetChargeAnimations", .05125f);    //arbs

        }
    }

    private void MoveBall()
    {
        if (ballGrabbed && !isBlocking)
        {
            Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
            
            if (levelManager.IsInGameBounds(cockBackPos))
            {
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

    }

    private bool CheckReleaseInput()
    {
        if ((Input.GetButtonUp(playerScript.joystick.rTriggerInput) || (Input.GetKeyUp(playerScript.joystick.altAction1Input) && !IsKeyPickUp) || (playerScript.joystick.number == 1 && throwButton && throwButton.Pressed) || (Input.touchCount >= 2 && isTapThrowReady && playerIsTouched && !isVirtualJoystickButtonsPressed())))
        {
            return true;
        }

        return false;
    }

    private void CheckStandingAutoRelease(float chargeVel)
    {

        float standingThrowPower = playerScript.standingThrowPower;
        float chargeCost = .25f;

        Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

        if ((standingThrowPower + throwCharge) >= maxStandingThrowPower && chargeVel <= standingThrowThresh)
        {
            if (levelManager.IsInGameBounds(cockBackPos))
            {
                if (!dodgeThrowDelay)
                {
                    print("Charge throw maxed @ " + throwCharge);
                    /*
                    chargeThrowSlowDownfactor = .0125f;
                    float stallTime = .25f;
                    SlowDownByVelocity(chargeThrowSlowDownfactor, stallTime);

                    */

                    {
                        float mackThrowDelay = .1f;
                        Throw();
                    }
                }
                else
                {
                    DodgeThrow();
                }

                if (animator)
                {
                    float mackThrowDelay = .1f;
                    animator.SetBool("hasBall", false);
                    Invoke("ResetThrowAnimations", .05125f);    //arbs
                }

                float throwSlowDownfactor = .25f;
                float stallTime = .25f;
                //   SlowDownByVelocity(throwSlowDownfactor, stallTime);



                DepleteStamina(chargeCost);
            }
        }
    }

    private void CheckMovingAutoRelease(float chargevel)
    {
        float chargeCost = .25f;

        Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

        if ((throwPower + throwCharge) >= maxThrowPower && chargevel > standingThrowThresh)
        {
            if (levelManager.IsInGameBounds(cockBackPos))
            {
                if (!dodgeThrowDelay)
                {
                    print("Charge throw maxed @ " + throwCharge);
                    /*
                    chargeThrowSlowDownfactor = .0125f;
                    float stallTime = .25f;
                    SlowDownByVelocity(chargeThrowSlowDownfactor,stallTime);

                    */

                    {
                        float mackThrowDelay = .1f;
                        //  Invoke("Throw", mackThrowDelay);
                        Throw();
                    }
                }

                else
                {
                    DodgeThrow();
                }

                if (animator)
                {
                    float mackThrowDelay = .1f;
                    animator.SetBool("hasBall", false);
                    Invoke("ResetThrowAnimations", .05125f);    //arbs
                }

                float throwSlowDownfactor = .25f;
                float stallTime = .25f;
                //   SlowDownByVelocity(throwSlowDownfactor,stallTime);



                DepleteStamina(chargeCost);
            }
        }
    }

    private void ResetCatchTrigger()
    {
        animator.ResetTrigger("Catch");
    }

    public void NormalAccelerationRate()
    {
        accelerationRate = .85f;
        isSlowingDown = false;
    }

    public void SetAccelerationRate(float x)
    {
        accelerationRate = x;
    }

    private void CheckKeyPickUp()
    {
        if (Input.GetKeyDown(playerScript.joystick.altAction1Input) && !IsKeyPickUp)
        {
            IsKeyPickUp = true;
           // print("IsKeyPickUp is true");
        }

        float pickUpDelay = .5f;

        Invoke("SetKeyPickUpFalse", pickUpDelay);
    }

    private void SetKeyPickUpFalse()
    {
        IsKeyPickUp = false;
        //print("IsKeyPickUp is false");
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
        /*
        if (((Input.GetKeyDown(playerScript.joystick.action1Input) || Input.GetKeyDown(playerScript.joystick.altAction1Input)) || (playerScript.joystick.number == 1 && pickUpButton && pickUpButton.pushed) || (IsTapBall())))

        {
            return true;
        }

        else
        {
            return false;
        }
        */
        return false;
    }

    private void CheckCatchCool()
    {
        if (!catchReady)
        {
            //  t_catchF = Time.realtimeSinceStartup;
            catchCoolDown -= Time.deltaTime;

            if (catchCoolDown <= 0) // && hasEnoughStamina
            {
                catchReady = true;
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



    public void DropBall()
    {
        print("Dropping the ball");

          Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
         
        if (levelManager.IsInGameBounds(cockBackPos))       //      isnt valid if we'reusing this on restart
        {
            throwCharge = 0;
            isCharging = false;
            ballGrabbed = false;
            ballCaught = false;
            ball.GetComponent<Ball>().grabbed = false;
            ball.GetComponent<SphereCollider>().enabled = true;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<SpriteRenderer>().enabled = true;
            ball.transform.GetChild(3).gameObject.SetActive(true);

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
            return Input.GetTouch(touch_i - 1).deltaPosition;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private float GetTouchSwipeSpeed(int touch_i)
    {
        touch_i--;

        if (Input.touchCount >= touch_i)
        {
            return Vector2.SqrMagnitude(Input.GetTouch(touch_i - 1).deltaPosition);
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

    private void Throw()   // throw
    {
        //   print("button throw");


        Vector3 throwStandVec = Vector3.zero;
        Vector3 throwMovVec = Vector3.zero;

        Vector3 cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
       
        
        Transform targetedOpp = null;
        Vector3 throwAidVec = new Vector3(1.0f, 1.0f, 1.0f);



        //Tag
        if (IsInGrabDistance(GetTargetedOpp().gameObject, "ball") && GetTargetedOpp())  
        {
            cockBackPos=  new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
            cockBackPos = (cockBackPos + GetTargetedOpp().position) / 2;      // tag pos  
                                                                              // print("Tag cockback");
        }

           ball.transform.position = cockBackPos;

        if (rigidbody.velocity.magnitude <= standingThrowThresh)
        {
            throwPower = playerScript.standingThrowPower;
        }
        else
        {
            throwPower = playerScript.throwPower0;
        }

        print("throwPower = " + throwPower);

        Vector3 weightedMuvAvVec = new Vector3(chargeVelInput.GetWeightedVelAverage().x, 0f, chargeVelInput.GetWeightedVelAverage().y);

        if (Mathf.Abs(weightedMuvAvVec.magnitude) < 25f)  // Have to check if wasn't moving dyring charge
        {
            weightedMuvAvVec.x = throwDirection.x * throwPower / 100f;
        }

        print("weightedMuvAvVec = " + weightedMuvAvVec);

        if (hasThrowMag)
            {
                Transform nearestOpp = GetTargetedOpp();

                if (nearestOpp && throwDirection.x > 0)
                {
                    Vector3 seekVec = nearestOpp.transform.position - ball.transform.position;
                    seekVec = new Vector3(Mathf.Clamp(seekVec.x, -maxSeekVec, maxSeekVec), seekVec.y, Mathf.Clamp(seekVec.z, -maxSeekVec, maxSeekVec));
                    seekVec = seekVec.normalized;

                    targetedOpp = nearestOpp;

                   // throwAidVec = GetThrowAid(weightedMuvAvVec, seekVec);
            }

            }

       
        print("throwAidVec = " + throwAidVec);

        Vector3 throwVec = new Vector3((throwPower + throwCharge) * throwAidVec.x * weightedMuvAvVec.x, 5f, throwAidVec.z * (throwPower + throwCharge) * weightedMuvAvVec.z);





        if (animator)
        {
            float throwMag = Vector3.Magnitude(throwVec);           //methodize per character
            float mack3ThrowSpeedThresh = 3500f;

            float throwAnimSpeed = Mathf.Clamp(throwMag / mack3ThrowSpeedThresh, 1.25f, 1.75f);
            //  print("throwAnimSpeed " + throwMag / mack3ThrowSpeedThresh);

            animator.SetFloat("ThrowSpeed", throwAnimSpeed);

            if (isJumping)
            {
                animator.SetTrigger("Air Throw");
            }
            else
            {
                animator.SetTrigger("Release");       // <-   speed scale this
            }
        }


        float renderLength = GetRenderLength();


        print("chargeTime = " + chargeTime);
        print("throwCharge = " + throwCharge);
        print("throww = " + throwVec);


        //  print("targetedOpp = " + targetedOpp);
        //   print("hasThrowMag = " + hasThrowMag);
        // print("throw Magnetism = "+throwMagnetism);

        ball.GetComponent<Ball>().Throw(throwVec, playerScript.color, hasSeekVec, throwMagnetism, targetedOpp, renderLength, ChargePowerAlpha);

        //For hit purposes
        if (gameObject.GetComponentInParent<Player>().team == 1)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 1);
        }
        if (gameObject.GetComponentInParent<Player>().team == 2)
        {
            ball.GetComponent<Ball>().SetThrown(gameObject.transform.parent.gameObject, 2);
        }

        playerScript.playThrowSound();
        playerScript.playThrowSound();
        levelManager.AddThrow(ball, parent);
        ballGrabbed = false;
        ballCaught = false;
        throwCharge = 0;
        isCharging = false;
        chargeTime = 0.0f;
        chargeVel = Vector3.zero;
        vel0 = Vector3.zero;
        chargeVelInput.ClearVelocities();
        Invoke("NormalAccelerationRate", .1f);
        

        if (animator)
        {
            animator.SetBool("hasBall", false);
        }
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

        cockBackPos = new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);

        ball.transform.position = cockBackPos;
        //print("cockBackPos = " + cockBackPos);



        if (animator)
        {

            float throwMag = Vector3.Magnitude(throww);
            float throwSpeedThresh = 300f;

            float throwAnimSpeed = Mathf.Clamp(throwMag / throwSpeedThresh, 2f, 3f);
            animator.SetFloat("ThrowSpeed", throwAnimSpeed);

            animator.SetTrigger("Release");
            animator.ResetTrigger("Charge");
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

        ball.GetComponent<Ball>().Throw(throww, playerScript.color, true, magnetism, targetedOpp, renderLength, ChargePowerAlpha);

        levelManager.AddThrow(ball, parent);
        ballGrabbed = false;
        ballCaught = false;
        throwPower = gameObject.GetComponentInParent<Player>().GetThrowPower0();
        throwCharge = 0;
        isCharging = false;
        chargeVel = Vector3.zero; throwCharge = 0;
        isCharging = false;
        chargeVel = Vector3.zero;

        if (animator)
        {
            animator.SetBool("hasBall", false);
        }
    }

    private void DodgeThrow()   // button throw
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


        if (IsInGrabDistance(GetTargetedOpp().gameObject, "ball") && GetTargetedOpp())
        {
           cockBackPos =  new Vector3(playerConfigObject.transform.position.x + throwDirection.x * ((collider.bounds.size.magnitude / 1.5f) + handSize.x), playerConfigObject.transform.position.y + handSize.y, playerConfigObject.transform.position.z + handSize.z);
            cockBackPos = (cockBackPos + GetTargetedOpp().position) / 2;      // tag pos  
                                                                              // print("Tag cockback");
        }

          ball.transform.position = cockBackPos;

        print("Dodge Throw");
        Vector3 dodgeThrowVec = new Vector3((dodgeSpeed * 2f + throwCharge) * move.x, 5f, move.z * (dodgeSpeed + throwCharge) * .5f);
        throww = dodgeThrowVec;




        if (animator)
        {
            float throwMag = Vector3.Magnitude(throww);
            float mack3ThrowSpeedThresh = 3500f;

            float throwAnimSpeed = Mathf.Clamp(throwMag / mack3ThrowSpeedThresh, 1.25f, 1.75f);
            //  print("throwAnimSpeed " + throwMag / mack3ThrowSpeedThresh);

            animator.SetFloat("ThrowSpeed", throwAnimSpeed);

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
        vel0 = Vector3.zero;
        Invoke("NormalAccelerationRate", .1f);


        if (animator)
        {
            animator.SetBool("hasBall", false);
        }
    }

    private Vector3 GetThrowAid(Vector3 throww, Vector3 seekVec)
    {
        Vector3 returnMe = (throww + seekVec) / 2;

        for (int i = 0; i < levelManager.roundLevel; i++)
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
            if (ac.name == "Mack.Ball.2.Throw" || ac.name == "King.2.Throw" || ac.name == "Nina.2.Throw")
            {
                //   clipLength = ac.length/10;    // *arbitary nums
            }
        }
        return clipLength;

    }


    private Transform GetTargetedOpp()
    {

        Transform nearestTargetedOpp = null;
        float nearest = 1000000f;
        if (GetComponentInParent<Player>().team == 1)
        {

            foreach (GameObject player in levelManager.tm2.players)
            {
                if (player.GetComponent<Player>().isOut == false)
                {
                    Vector3 diff = player.transform.GetChild(0).position - playerConfigObject.transform.position;
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
                foreach (GameObject player in levelManager.tm1.players)
                {
                    if (player.GetComponent<Player>().isOut == false)
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



    public void SuperInput(CallbackContext context)
    {
        if (levelManager.isPlaying && !playerScript.isOut)
        {
            //if mode is basic or advanced
            //  if (GameManager.mode == "Basic")



            int superType = playerScript.super.GetComponent<SuperScript>().type;

            if (context.started && superCoolDown <= 0 && ballGrabbed && !isDodging)   // super activate
            {
                SuperActivate();
            }

            if (context.started && ballGrabbed && isSupering)                   // super charge   ... iffy when conisdering if isSupering finishes before SuperAutoRelease
            {
                SuperCharge(superType);
            }

            if (context.canceled && ballGrabbed && isSupering)                        // superRelease
            {
                SuperRelease(chargeVel.magnitude, superType);
            }
        }
    }

    private void CheckSuperCool()
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
    }

    private void SuperCharge(int superType)
    {
        float chargeCost = .25f;

        if (!isCharging)
        {
            chargeVel = rigidbody.velocity;
            chargeVelInput.Input(chargeVel.x, chargeVel.z);
            isCharging = true;

            float glide = .01f - chargeVel.magnitude / 100000f;
            accelerationRate = Mathf.Clamp(glide, 0.000001f, 1.0f);
            velVec = chargeVel;
        }



        float chargeThrowSlowDownfactor = 1f;

        if ((throwCharge) < maxStandingThrowPower)     // 
        {
            print("Super Charge");
            print("Charge @ " + throwCharge);
            float chargeRate = 50;
            throwCharge += chargeRate * Time.deltaTime * 100.0f;
            throwCharge = Mathf.Clamp(throwCharge, 0f, maxStandingThrowPower + 1);

            chargeThrowSlowDownfactor = .025f * Time.deltaTime * 100.0f;
            float stallTime = .1f;
           // SlowDownByVelocity(chargeThrowSlowDownfactor,stallTime);

            accelerationRate = .25f;
            Invoke("NormalAccelerationRate", 1f);

            DepleteStamina(chargeCost);

            //  animator.SetTrigger("Charge");

        }

        else
        {
            SuperRelease(chargeVel.magnitude, superType);
        }


    }

    private bool CheckSuperInput()
    {
        if ((Input.GetButton(playerScript.joystick.superInput) || Input.GetKey(playerScript.joystick.altSuper1Input) || (playerScript.joystick.number == 1 && superButton && superButton.Pressed)))
        {

            return true;
        }

        return false;
    }


    private bool CheckSuperInputDown()
    {
        if (Input.GetKeyDown(playerScript.joystick.altSuper1Input))
        {

            return true;
        }

        return false;
    }

    private bool CheckSuperInputRelease()
    {
        if (Input.GetKeyUp(playerScript.joystick.altSuper1Input))
        {

            return true;

        }

        return false;
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
        superCoolDown = playerScript.power;
        Color playerColor = playerScript.color;

        int superType = playerScript.super.GetComponent<SuperScript>().type;

        float throwMag = 0;

        if (superType == 1 || superType == 2)
        {

            ballSupered = ball;
            superPackage = Instantiate(playerScript.super);
            ballSupered.GetComponent<Ball>().SuperInit(superPackage);
            superPackage.transform.parent = ballSupered.transform;
            superPackage.transform.localPosition = Vector3.zero;
            SetSuperColor(playerColor, superPackage);

            if (superType == 2)
            {
                if (throwDirection.x == 1)
                {
                    superPackage.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                }
                else
                {
                    superPackage.transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
                }

            }

            animator.SetTrigger("Charge");

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

    private void SuperRelease(float magnitude, int superType)
    {
        float throwMag = 0.0f;

        if (superType == 1) // Mack (Ball Expansion)
        {
            Transform nearestOpp = GetTargetedOpp();
            Vector3 seekVec = nearestOpp.transform.position - ball.transform.position;

            print("ball = " + ball.transform.position);
            print("nearestOpp transfrom = " + nearestOpp.transform.position);
            print("seekVec = " + seekVec);

            // throww = seekVec * throwPower * superPackage.GetComponent<SuperBall>().throwPowerMult;
            throww = seekVec.normalized * 5f * throwCharge;
            //  throww = new Vector3(throww.x, 5f, throww.z);
        }

        if (superType == 2)  // King (Super Tech Ball) 
        { 

            throww = new Vector3(throwPower * rigidbody.velocity.x, 4f, throwPower * rigidbody.velocity.z);

        }

        Throw(throww, "Super", throwMag);
    }


    public void SetSuperColor(Color color, GameObject superPackage)
    {
        // 1 SuperBallFX

        int child0Count = superPackage.transform.childCount;

        for (int i =0; i< child0Count; i++)
        {
            GameObject super0Child = superPackage.transform.GetChild(i).gameObject;
            ParticleSystem s0CPS = super0Child.GetComponent<ParticleSystem>();
            var s0CPSmain = s0CPS.main;

            if (i == 1)
            {
                s0CPSmain.startColor = new Color(color.r, color.g, color.b, .2f);
            }
            else
            {
                s0CPSmain.startColor = color;
            }

            int child1Count = super0Child.transform.childCount;

            for (int j = 0; j < child1Count; j++)
            {
                GameObject super1Child = super0Child.transform.GetChild(j).gameObject;
                ParticleSystem s1CPS = super1Child.GetComponent<ParticleSystem>();
                var s1CPSmain = s1CPS.main;

                if ((i==0 && j==0) || (i == 0 && j == 1) || (i==1 && j==2))
                {
                    s1CPSmain.startColor = new Color(color.r, color.g, color.b, .2f);
                }
                else
                {
                    s1CPSmain.startColor = color;
                }

                int child2Count = super1Child.transform.childCount;

                for (int k = 0; k < child2Count; k++)
                {
                    GameObject super2Child = super1Child.transform.GetChild(k).gameObject;
                    ParticleSystem s2CPS = super1Child.GetComponent<ParticleSystem>();
                    var s2CPSmain = s2CPS.main;
                    s2CPSmain.startColor = color;

                }
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

    private bool IsInGrabDistance(GameObject objectToCheck, string type)
    {
        if (playerConfigObject.transform.position.x + grabRadius * grabHelpMultiplier > objectToCheck.transform.position.x &&
                   playerConfigObject.transform.position.x - grabRadius * grabHelpMultiplier < objectToCheck.transform.position.x)
        {
            if (playerConfigObject.transform.position.y + grabRadius * grabHelpMultiplier > objectToCheck.transform.position.y &&
                playerConfigObject.transform.position.y - grabRadius * grabHelpMultiplier < objectToCheck.transform.position.y)
            {
                if (playerConfigObject.transform.position.z + grabRadius * grabHelpMultiplier > objectToCheck.transform.position.z &&
                    playerConfigObject.transform.position.z - grabRadius * grabHelpMultiplier < objectToCheck.transform.position.z)
                {
                    if ( type == "ball")
                    {
                        if ((IsAboutToCollideWBall(objectToCheck) && objectToCheck.GetComponent<Ball>().thrown == false) && !inBallPause && ballPauseReady)
                        {
                            inBallPause = true;
                            float ballPauseTime = .5f;                                // gr
                            Invoke("TurnInBallPauseFalse", ballPauseTime);
                            ballPauseReady = false;
                        }
                    }

                    // Check facing object
                    float angle = 180f;
                    if (Vector3.Angle(playerConfigObject.transform.forward, objectToCheck.transform.position - playerConfigObject.transform.position) < angle)
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
        accelerationRate = .25f;
        Invoke("SetBallPauseReadyTrue", 1f);
    }

    private void SetBallPauseReadyTrue()
    {
        accelerationRate = .85f;
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


    private void PostFX(string type)
    {
        levelManager.PostFX(type);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Playing Level")
        {
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

    public bool InBounds()
    {
        inBounds = true;
        if (gameObject.GetComponentInParent<Player>().team == 1)
        {
            if (collider.bounds.min.x < levelManager.stage.baseLineLeft)
            {
                playerConfigObject.transform.position = new Vector3(levelManager.stage.baseLineLeft + collider.bounds.extents.x + 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
                rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
                inBounds = false;
             //   print("Out of Bounds 1");

            }
            if (collider.bounds.max.x > levelManager.stage.halfCourtLine)
            {
                playerConfigObject.transform.position = new Vector3(levelManager.stage.halfCourtLine - collider.bounds.extents.x - 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
                rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
                inBounds = false;
              //  print("Out of Bounds 1");

            }
        }

        if (gameObject.GetComponentInParent<Player>().team == 2)
        {
            if (collider.bounds.min.x < levelManager.stage.halfCourtLine)
            {
                playerConfigObject.transform.position = new Vector3(levelManager.stage.halfCourtLine + collider.bounds.extents.x + 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
                rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
                inBounds = false;
            //    print("Out of Bounds 2");

            }
            if (collider.bounds.max.x > levelManager.stage.baseLineRight)
            {
                playerConfigObject.transform.position = new Vector3(levelManager.stage.baseLineRight - collider.bounds.extents.x - 0.05f, playerConfigObject.transform.position.y, playerConfigObject.transform.position.z);
                rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
                inBounds = false;
             //   print("Out of Bounds 2");

            }
        }


        return inBounds;
    }



    private void SetDodgingF()
    {
        isDodging = false;
        playerScript.ToggleActivateDodge();
        print("Dodge is false");
    }

    private void SetDodgeThrowDelayF()
    {
        dodgeThrowDelay = false;
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


    private void HandleContact()    // move to player Config
    {
        if (ballContact)
        {
            TriggerHitIndicators();

            float contactSlowDownfactor = .4f;             // should be rekative to ballHit speed
           // SlowDown(contactSlowDownfactor);

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

    private void HandlePerks(float dur, float mult)
    {
        if (hasPerks)
        {

        }
    }

    private void PauseInput(CallbackContext ctx)
    {
       
           // levelManager.PauseGame();
       
    }
}

