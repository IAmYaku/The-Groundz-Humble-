using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSeek : MonoBehaviour {

    public float force = 10f;
    public Transform target;
    public ParticleSystem ps;

    void Start () {
        ps = gameObject.GetComponent<ParticleSystem>();
	}
	

	// Update is called once per frame
	void LateUpdate () {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);
        for (int i=0; i< particles.Length; i++)
        {
            ParticleSystem.Particle p = particles[i];
            Vector3 directionToTarget = (target.position - p.position).normalized;
            Vector3 seekForce = directionToTarget * force * Time.deltaTime;
            p.velocity += seekForce;
         // p.position = new Vector3(p.position.x + Random.value, p.position.y + Random.value, +p.position.z + Random.value);
        //  ParticleSystem.VelocityOverLifetimeModule volm = ps.velocityOverLifetime;
       //    volm.x = seekForce.x;
      //     volm.y = seekForce.y;
     //     volm.z = seekForce.z;
            particles[i] = p;
        }
        ps.SetParticles(particles, particles.Length);
        ps.startLifetime = (target.position - transform.position).magnitude / 10;
	}
}
