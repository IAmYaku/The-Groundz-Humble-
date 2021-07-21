using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeGate : MonoBehaviour
{
    public GameObject ageGate;
    public bool isOldEnough;
    private int ageLimit;
    public Button yesButton;
    public Button noButton;
    public Animator ageThreshAnim;

    public AdManager adManager;

    void Start()
    {
        if (!ageGate)
        {
            ageGate = gameObject;
        }

        if (!ageThreshAnim)
        {
            ageThreshAnim = gameObject.GetComponent<Animator>();
        }


        ageLimit = GameManager.ageThresh;


        LoadAdTest();
    }

    private void LoadAdTest()
    {
        adManager.LoadsSuperAwesomeVideo();
        adManager.PlaySuperAwesomeVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsOldEnough(bool v)
    {

        if (v == true)
        {
            if (ageThreshAnim)
            {
                isOldEnough = true;
                ageThreshAnim.SetBool("IsOver", true);
                Invoke("DestroySelf", 1f);
            }
        }
        else
        {
            Application.Quit();
        }
           
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
