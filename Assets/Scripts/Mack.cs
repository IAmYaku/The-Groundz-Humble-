﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mack : Character
{

    static public string name = "Mack";
    public static bool isLocked;

    public static float maxSpeed = 45f;
    public static float xSpeed = 240f;
    public  static float zSpeed = 260f;
    public static float acceleration = 1;

    public static float dodgeSpeed = 1600f;
    public static float jumpSpeed = 10.0f;
    public static float throwPower0 = 100f;
    public static float standingThrowPower = 200;
    public static float maxThrowPower = 600f;
    public static float maxStandingThrowPower = 1800f;
    public static float superCoolDown = 30.0f;
    public static float grabRadius = 8f;
    public static Vector3 handSize = new Vector3(3f, 2f, 0f);
    public static float catchLagTime = .5f;
    public static int stamina =150;
    public static float staminaCoolRate = 1.0f;
    public static float toughness =2;



    public static int power = 30;
    public static int mass = 1000;

    public static Material out_mat;
    public static Material default_mat;
    public static Material super_mat;

    public static Sprite playerIconImage;
    public static GameObject staminaBarObject;
    public static GameObject powerBarObject;
    public static GameObject super;
    public static Color color;
    public static Color tintColor;

    public static Animator animator;
    public static RuntimeAnimatorController win;
    public static RuntimeAnimatorController play;

    public static AudioClip dodgeSound;
    public static AudioClip outSound;
    public static AudioClip footsteps;
    public static AudioClip[] throwSounds;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
