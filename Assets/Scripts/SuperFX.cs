using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioPeer audioPeer;
    public AudioVisualizer audioVisualizer;
    public Material material_super;
    public Material material_reg;
    public bool power = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioPeer = GetComponent<AudioPeer>();
        audioVisualizer = GetComponent<AudioVisualizer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerOn()
    {
        power = true;
        audioSource.enabled = true;
        audioPeer.enabled = true;
        audioVisualizer.enabled = true;
        
    }

    public void PowerOff()
    {
        power = false;
        audioSource.enabled = false;
        audioPeer.enabled = false;
        audioVisualizer.enabled = false;

    }

    public Material GetSuperMaterial()
    {
        return material_super;
    }

    public void SetSuperMaterial( Material mat)
    {
        mat = material_super;
    }

    public void SetRegMaterial(Material mat)
    {
        mat = material_reg;
    }

    public Material GetRegMaterial()
    {
        return material_reg;
    }

    public void EnableSuperASource()
    {
        audioSource.enabled = true;
    }

    public void DisableSuperASource()
    {
        audioSource.enabled = false;
    }

}
