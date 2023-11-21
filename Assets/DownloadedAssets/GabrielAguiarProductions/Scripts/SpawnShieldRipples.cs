using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipples : MonoBehaviour
{
    public GameObject shieldRipples;  

    private VisualEffect shieldRipplesVFX;  

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Ball")
        {
            var ripples = Instantiate(shieldRipples, transform) as GameObject;
            shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            shieldRipplesVFX.SetVector3("SphereCenter", co.contacts[0].point);
            print("Block Contact!~");
            Destroy(ripples, 2);
        }
    }
}
