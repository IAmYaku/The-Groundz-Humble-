using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class GameObjectTTL : MonoBehaviour
{
    public GameObjectTTL()
    {
    }


    public float TimeToLive = 0;
    public Action OnExpired;

    void Update()
    {
        TimeToLive -= Time.deltaTime;

        if (TimeToLive <= 0)
        {
            GameObject.Destroy(this.gameObject);
            if (OnExpired != null)
                OnExpired();
        }
    }

    public static void Apply(GameObject go, float ttl)
    {
        go.AddComponent<GameObjectTTL>().TimeToLive = ttl;
    }

    public static void UnApply(GameObject go)
    {
        var comp = go.GetComponentInChildren<GameObjectTTL>();
        if (comp != null)
        {
            GameObject.Destroy(comp);
        }
    }

    public static void SoftKillParticles(GameObject go, float ttl = 3f)
    {
        foreach (var ps in go.GetComponentsInChildren<ParticleSystem>())
        {
            var pos = ps.transform.position;
            ps.transform.SetParent(null, true);
            ps.transform.position = pos;
            ps.transform.localScale = Vector3.one;
            ps.Stop();
            if (ps.GetComponent<GameObjectTTL>() == null)
            {
                Apply(ps.gameObject, ttl);
            }
        }
    }
}
