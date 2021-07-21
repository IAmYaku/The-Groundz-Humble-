using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfiguration : MonoBehaviour
{
    GameObject parent;
    LevelManager levelManager;
    GameObject playerAura;
    Player player;
    Controller3D controller3D;
    AI ai;

    static int healthStock = 3;    // Set from gameRule    

    
    public Animator animator;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public Rigidbody rigidbody;
    public SphereCollider headCollider;   // extra points +?, sfx, vfx, etc
    public CapsuleCollider bodyCollider;

    public RuntimeAnimatorController play;
    public RuntimeAnimatorController win;

    public Material out_mat;
    public Material default_mat;

    bool onGround;
    bool isKnowckedOut;
    bool isJumping;

    float pushVal = .2f;   // rename and pull to gr

    float t_contact0;

    bool ballContact;
    GameObject ballHit;

 //  public bool inBallCollision;



    void Start()
    {
        parent = this.transform.parent.gameObject;

        if (!player)
        {
            player = parent.GetComponent<Player>();
        }

        if (!playerAura)
        {

        }

        if (!controller3D)
        {
            controller3D = player.controller3DObject.GetComponent<Controller3D>();
        }

        if (!ai)
        {
            ai = player.controller3DObject.GetComponent<AI>();
        }

        if (!levelManager)
        {
            levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SwitchToWinAnimation()
    {
        throw new NotImplementedException(); 
    }

    public void RemoveContat()
    {
        ballContact = false;
        ballHit = null;

    }

    void OnCollisionEnter(Collision collision)
    {
       

        if (!player.isOut)
        {
            if (collision.gameObject.tag == "Ball")
            {
             //   inBallCollision = true;
              //  print("Ball Collision");
               // print("thrownBy1 = " + collision.gameObject.GetComponent<Ball>().thrownBy1);
              //  print("grounded = " + collision.gameObject.GetComponent<Ball>().grounded);

                if (gameObject.GetComponentInParent<Player>().team == 1)                                                                           // make more module
                {
                    if (collision.gameObject.GetComponent<Ball>().thrownBy2 && collision.gameObject.GetComponent<Ball>().grounded == false)
                    {
                        TriggerHeadHit();
                        ballContact = true;
                        t_contact0 = Time.realtimeSinceStartup;
                        ballHit = collision.gameObject;

                        ballHit.GetComponent<Ball>().contact = true;
                        float ballVelocity = Mathf.Clamp(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude / 9, 3, 6);

                        ParticleSystem ball_hit_ps = collision.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
                        ball_hit_ps.GetComponent<Renderer>().sortingOrder = 3;
                        ball_hit_ps.startSize = ballVelocity;

                        if (collision.gameObject.GetComponent<Ball>().isSupering)
                        {
                            TriggerKnockBack(collision.gameObject.GetComponent<Rigidbody>().velocity);
                            ParticleSystem.MainModule sup_main_ps = collision.gameObject.GetComponentInChildren<ParticleSystem>().main;
                            sup_main_ps.startSize = 4;
                            sup_main_ps.simulationSpeed = 20f;
                            sup_main_ps.startSizeX = 10f;
                            sup_main_ps.startSizeMultiplier = 10f;
                        }

                        if (!(collision.gameObject.GetComponent<TrailRenderer>().startWidth == 2f))
                        {
                            if (collision.gameObject.GetComponent<TrailRenderer>().enabled == false)
                            {
                                collision.gameObject.GetComponent<TrailRenderer>().enabled = true;
                            }
                            else
                            {
                                collision.gameObject.GetComponent<TrailRenderer>().startWidth = 2f;
                            }
                        }

                        float hitPauseDuration = ballVelocity / 25f;                 // * arbitrays nums
                        float hitPausePreDelay = .125f;

                        DelayPause(hitPauseDuration, hitPausePreDelay);

                        levelManager.AddHit(collision.gameObject, parent);

                        levelManager.HitDisplay(gameObject, collision.gameObject);                                             //grs
                        float shakeIntensity = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                        levelManager.CamShake(shakeIntensity, transform);
                        print("~!CONTACT!~ 2");
                    }

                    else
                    {
                        float correctThresh = 1.0f;
                        float velMag = rigidbody.velocity.magnitude;
                        if (velMag > correctThresh)
                        {
                            rigidbody.velocity = Vector3.zero;
                            Vector3 diff = (transform.position - collision.transform.position) * pushVal * (velMag / 100f);
                            Vector3 nuPos = new Vector3(transform.position.x + diff.x, transform.position.y - .025f, transform.position.z + diff.z);
                            rigidbody.MovePosition(nuPos);              // ?? why not Addforce?
                        }
                    }    
                }

                if (gameObject.GetComponentInParent<Player>().team == 2)
                {
                    if (collision.gameObject.GetComponent<Ball>().thrownBy1 && collision.gameObject.GetComponent<Ball>().grounded == false)
                    {
                        TriggerHeadHit();
                        ballContact = true;
                        t_contact0 = Time.realtimeSinceStartup;
                        ballHit = collision.gameObject;
                        ballHit.GetComponent<Ball>().contact = true;

                        float ballVelocity = Mathf.Clamp(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude / 9, 3, 6);

                        ParticleSystem ball_hit_ps = collision.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
                        ball_hit_ps.GetComponent<Renderer>().sortingOrder = 3;
                        ball_hit_ps.startSize = ballVelocity;
                        if (collision.gameObject.GetComponent<Ball>().isSupering)
                        {
                            TriggerKnockBack(collision.gameObject.GetComponent<Rigidbody>().velocity);
                            ParticleSystem.MainModule sup_main_ps = collision.gameObject.GetComponentInChildren<ParticleSystem>().main;
                            sup_main_ps.startSize = 4;
                            sup_main_ps.simulationSpeed = 20f;
                            sup_main_ps.startSizeX = 10f;
                            sup_main_ps.startSizeMultiplier = 10f;
                        }
                        if (!(collision.gameObject.GetComponent<TrailRenderer>().startWidth == 2f))
                        {
                            if (collision.gameObject.GetComponent<TrailRenderer>().enabled == false)
                            {
                                collision.gameObject.GetComponent<TrailRenderer>().enabled = true;
                            }
                            else
                            {
                                collision.gameObject.GetComponent<TrailRenderer>().startWidth = 2f;

                            }
                        }

                        float hitPauseDuration = ballVelocity / 25f;
                        float hitPausePreDelay = .125f;

                        DelayPause(hitPauseDuration, hitPausePreDelay);

                        levelManager.AddHit(collision.gameObject, parent);
                        levelManager.HitDisplay(gameObject, collision.gameObject);
                        levelManager.CamShake(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude, transform);
                        levelManager.PostFX("Player1Hit");
                        print("~!CONTACT!~ 1");
                    }
                    else
                    {
                        float correctThresh = 1.0f;
                        float velMag = rigidbody.velocity.magnitude;
                        if (velMag > correctThresh)
                        {
                             Vector3 diff = (transform.position - collision.transform.position) * pushVal * ( velMag/100f);
                            rigidbody.velocity = Vector3.zero;
                             Vector3 nuPos = new Vector3(transform.position.x + diff.x, transform.position.y - .025f, transform.position.z + diff.z);
                            rigidbody.MovePosition(nuPos);              // ?? why not Addforce?
                        }
                    }
                }
            }


            // TODO doesnt make smoothe as intended 
            if (collision.gameObject.tag == "Wall")
            {
                rigidbody.AddForce(-rigidbody.velocity.x, 0, -rigidbody.velocity.z);
            }


            if (collision.gameObject.tag == "Playing Level")
            {
                onGround = true;
                if (isJumping)
                {
                    isJumping = false;
                    if (animator)
                    {
                        animator.SetBool("Jumping", false);
                    }
                }
            }

            if (collision.gameObject.tag == "Player Sprite")
            {
                /*
                rigidbody.velocity = Vector3.zero;
                Vector3 pushBack = (transform.position - collision.transform.position);
                rigidbody.AddForce(pushBack * pushVal);
                print("Ouch!");

                */

                rigidbody.velocity = Vector3.zero;
                Vector3 diff = (transform.position - collision.transform.position) * pushVal;
                Vector3 nuPos = new Vector3(transform.position.x + diff.x, transform.position.y - .025f, transform.position.z + diff.z);
                rigidbody.MovePosition(nuPos);

            }


        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
           
        }
        else
        {
          //  inBallCollision = false;
        }

    }

    private void TurnInCollisionFalse()
    {
        controller3D.SetAccelerationime(.85f);
      //  inBallCollision = false;

    }

    internal void TriggerHeadHit()
    {
        if (animator)
        {
            animator.SetTrigger("Head Hit");
        }
    }

    private void TriggerKnockBack(Vector3 ballVelocity)                                                                    // important to revitalize
    {
        /*
        rigidbody.AddExplosionForce(ballVelocity.magnitude, ballVelocity, ballVelocity.magnitude / 10);
        knockedOutTime = 3f;
        t_k0 = Time.realtimeSinceStartup;
        isKnockedOut = true;
        animator.SetTrigger("Head Hit");
        // animator.SetTrigger("Knock Out");
        */

    }

    private void DelayPause(float hitPauseDuration, float hitPausePreDelay)
    {
        levelManager.SetHitPauseDuration(hitPauseDuration);
        Invoke("DoHitPause", hitPausePreDelay);

    }


    private void DoHitPause()
    {
        levelManager.HitPause();
    }

    internal void SetOutAnimation(bool v)
    {
        animator.SetBool("isOut", v);
    }
}
