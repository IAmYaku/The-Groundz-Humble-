using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFX : MonoBehaviour
{
    // Start is called before the first frame update

    ParticleSystem t0ps;
    ParticleSystem t1ps;

    Vector3 t1LockPos;
    bool t1Activate;

    bool isReleased;
    Vector3 seekPos;
    public Color playerColor;
    Vector3 scale0;

    void Start()
    {
        scale0 = transform.localScale;
        t0ps = gameObject.GetComponent<ParticleSystem>();
        t1ps = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

        playerColor = transform.parent.transform.parent.gameObject.GetComponent<Player>().color;

        ParticleSystem.MainModule t1psMain = t1ps.main;
        ParticleSystem.MainModule t0psMain = t0ps.main;

        t0psMain.startColor = playerColor;
        t1psMain.startColor = playerColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReleased)
        {
            if (Vector3.Magnitude(seekPos - transform.position) > 1)
            {
              // transform.position += (seekPos - transform.position) / 10f;
            }
            else
            {
              //  isReleased = false;
              //  Set1Acivate(false);
            }
        }

        if (t1Activate)
        {
            t1ps.transform.position = t1LockPos;
        }
    }

    internal void Set0Acivate(bool v)
    {
            gameObject.SetActive(v);
        

        if (!t0ps)
        {
            t0ps = gameObject.GetComponent<ParticleSystem>();
        }

        t0ps.time = 0;
        t0ps.Play();
    }

    internal void Set1Acivate(bool v)
    {
            transform.GetChild(0).gameObject.SetActive(v);

        if (!t1ps)
        {
            t1ps = gameObject.GetComponent<ParticleSystem>();
        }

        t1ps.time = 0;
        t1ps.Play();

    }
    internal void Touch1Acivate(Vector3 t1pos)
    {
        t1Activate = true;
        if (transform.GetChild(0).gameObject.activeSelf == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        float playerYpos = transform.parent.position.y;
        t1LockPos = new Vector3(t1pos.x, 5f, t1pos.z);
        t1ps.gameObject.transform.position = t1LockPos;
        t1ps.time = 0;
        t1ps.Play();
        

    }

    public void TouchSVelToPSVel(Vector3 vel)
    {
        float velMagNorm = Vector3.Magnitude(vel)/50f;
        transform.localScale = new Vector3(scale0.x*(1+velMagNorm), scale0.y/ (1+ velMagNorm), scale0.z);
    }

    internal void TriggerTapRelease(Vector3 target)
    {
        isReleased = true;
        seekPos = target; 
    }
}
