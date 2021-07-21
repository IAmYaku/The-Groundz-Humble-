using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCourtLine : MonoBehaviour
{
    // Start is called before the first frame update
   public ParticleSystem halfCOurtParticleSystem;
    ParticleSystem.Particle[] m_Particles;
    bool isBoosted;
    float boostTime;
     
    void Start()
    {
        if (!halfCOurtParticleSystem)
        {
            halfCOurtParticleSystem = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoosted)
        {
            if (boostTime > 0)
            {
                boostTime -= Time.deltaTime;
                BoostParticleSystem(boostTime);
            }
            else
            {
                isBoosted = false;
                NormalParticleSystem();
            }

        }
    }

    private void BoostParticleSystem(float boostTime)
    {
        int numParticlesAlive = halfCOurtParticleSystem.main.maxParticles;
        m_Particles = new ParticleSystem.Particle[numParticlesAlive];
        halfCOurtParticleSystem.GetParticles(m_Particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].startSize = Mathf.Clamp(m_Particles[i].startSize *2f, .25f,.5f);
            m_Particles[i].color = new Color(m_Particles[i].color.r, m_Particles[i].color.g, m_Particles[i].color.b, m_Particles[i].color.a + boostTime);

        }
        halfCOurtParticleSystem.SetParticles(m_Particles, numParticlesAlive);
    }

    private void NormalParticleSystem()
    {
        int numParticlesAlive = halfCOurtParticleSystem.main.maxParticles;
        m_Particles = new ParticleSystem.Particle[numParticlesAlive];
        halfCOurtParticleSystem.GetParticles(m_Particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].startSize = .25f;
            m_Particles[i].color = new Color(m_Particles[i].color.r, m_Particles[i].color.g, m_Particles[i].color.b, .1f);
        }
        halfCOurtParticleSystem.SetParticles(m_Particles, numParticlesAlive);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player Sprite" && other.transform.GetComponentInParent<Player>().hasJoystick)
        {
            SetBoostParticleSystem();
        }
    }



    private void SetBoostParticleSystem()
    {
        isBoosted = true;
        boostTime = 1f;


    }
}
