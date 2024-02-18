using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class JSON_Handler : MonoBehaviour
{
    // Controller3D values

    float grabHelpMultiplier;
    float catchHelpMultiplier;
    float moveCurveTimeMultiplier;
    //slowDownForBallMultiplier
    //grabStallMultiplier
    //standingThrowThresh
    //throwChargeMultiplier
    //dodgeMultiplier
    //dodgeStaminaCost
    //sprintStaminaCost
    //blockStaminaCost
    //sprintStaminaCost

    //superRechargeMultiplier
    public class Controller3DSetting
    {
        public float grabHelpMultiplier;
        public float catchHelpMultiplier;
        public float moveCurveTimeMultiplier;

       // public float chargeRate = 1000;  // character dependent??
       // public float chargeCost = .25f;
        //slowDownForBallMultiplier
        //grabStallMultiplier
        //standingThrowThresh
        //throwChargeMultiplier
        //dodgeMultiplier
        //dodgeStaminaCost
        //sprintStaminaCost
        //blockStaminaCost
        //sprintStaminaCost

        //superRechargeMultiplier

    }


    //AI values
    public class AISetting
    {
        // intensity sensitivity
    }

    //ArcadeMode Values
    public class ArcadeModeSetting
    {
        // difficulty multiplier
    }

    public class OtherGamePlaySetting
    {
        float hitPauseMultiplier;
    }

    public class CameraSetting
    {
        
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Save(string type)
    {


        if (type == "Controller3DSetting")
        {
            Controller3DSetting controller3DSetting = new Controller3DSetting
            {
            grabHelpMultiplier = grabHelpMultiplier,
            catchHelpMultiplier = catchHelpMultiplier,
            moveCurveTimeMultiplier = moveCurveTimeMultiplier,
            //slowDownForBallMultiplier
            //grabStallMultiplier
            //standingThrowThresh
            //throwChargeMultiplier
            //dodgeMultiplier
            //dodgeStaminaCost
            //sprintStaminaCost
            //blockStaminaCost
            //sprintStaminaCost

            //superRechargeMultiplier
        };

            string json = JsonUtility.ToJson(controller3DSetting);
            File.WriteAllText(Application.dataPath + "/save.text", json);
        }

    }

    public void Load(string type)
    {


        if (type == "Controller3DSetting")
        {
            if (File.Exists(Application.dataPath + "/save.text"))
            {
                string saveString = File.ReadAllText(Application.dataPath + "/save.text");
                Controller3DSetting controller3DSetting = JsonUtility.FromJson<Controller3DSetting>(saveString);


            }

        }

    }
}
