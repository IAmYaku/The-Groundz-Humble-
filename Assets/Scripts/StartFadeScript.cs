using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Michsky.UI.Dark;

public class StartFadeScript : MonoBehaviour
{

    public SplashScreenManager splashScreenManager;

    private void Awake()
    {

        if (!splashScreenManager)
        {
            splashScreenManager = gameObject.GetComponent<SplashScreenManager>();
        }

        if (GlobalConfiguration.Instance)
        {
            if (GlobalConfiguration.Instance.gameStarted)
            {
                splashScreenManager.disableSplashScreen = true;


            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
