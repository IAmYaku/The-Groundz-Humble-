using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour                // * TeamSelect
{ 
    public Animator uiAnimator;
    public static int PlayerNum;
    private bool ready;
    private float t0;
    private float tF;
    private float deltaT;

    public ParticleSystem MackPS;
    public ParticleSystem KingPS;
    public ParticleSystem NinaPS;

    private bool isNinaSelected;
    private bool isKingSlecetd;
    private bool isMackSelected;

    public Animator MackAnimator;
    public Animator KingAnimator;
    public Animator NinaAnimator;

    int selected;
    int selectCount; // obsolete

    int readys;
    int readyCount;


    public void Start()
    {
        readyCount = GlobalConfiguration.Instance.GetDeviceCount();

        selectCount = GlobalConfiguration.Instance.GetDeviceCount();     // obsolete
        // check locks @ global config instance
    }


    private void Update()
    {
        if (ready)
        {
            tF = Time.realtimeSinceStartup;
            deltaT = tF - t0;
            if (deltaT >= 3)
            {

                SceneManager.LoadScene("StageSelect");
            }
        }
    }


     void PlayerReady(string name, int team, int pi)    // obsolete    // pi: 1-4 = players, 0 = null, -1 = ai 
    {
      //  GlobalConfiguration.instance.AddSelectedPlayer(name, team, pi); // "Mack", "King", "Nina"

        if (readyCount <= readys)
        {
            ready = true;
            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }

    }

    


    public void MackSelect()
    {
        if (!ready)
        {
          //  if (!isMackSelected)
            {
                print("Mack");
                selected++;



                ParticleSystem.MainModule main = MackPS.main;
                main.simulationSpeed = 2f;

                // isMackSelected = true;
                //MackAnimator.SetFloat("Selected",1f);

            }
        }
        
        if (selectCount <= selected)
        {
            ready = true;
            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Nina Button"));
        }

    }

    private void TriggerUIAnimation()
    {
        uiAnimator.SetBool("Select", true);
    }

    public void NinaSelect()
    {
        if (!ready)
        {
            if (!isNinaSelected)
            {
                print("Nina");
                selected++;
               // isNinaSelected = true;
                // MackAnimator.SetFloat("Selected", 1f);

                var main = NinaPS.main;
                main.simulationSpeed = 8f;
            }
        }
        if (selectCount <= selected)
        {
            ready = true;
            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }


    }

    public void KingSelect()
    {
        if (!ready)
        {
            if (!isKingSlecetd)
            {
                print("King");
                selected++;
               // isKingSlecetd = true;
                // KingAnimator.SetFloat("Selected", 1f);
              //  int playerNum = GameManager.playerTypes.Count + 1;
             //   GameManager.playerTypes.Add(playerNum, "King");
                ParticleSystem.MainModule main = KingPS.main;
                main.simulationSpeed = 6f;
            }
        }
        if (selectCount <= selected)
        {
            ready = true;
            Invoke("TriggerUIAnimation", .70f);
            t0 = Time.realtimeSinceStartup;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Nina Button"));
        }
    }

    private string[] GetActualJoysticks(string[] stix)
    {
        string[] joysticks = Input.GetJoystickNames();
        int j = 0;
        string[] returnMe;
        List<string> addMe = new List<string>();

        for (int i = 0; i < joysticks.Length; i++)
        {
            if (stix[i] != "")
            {
                j++;
                addMe.Add(stix[i]);
            }
        }
        returnMe = new string[j];

        for (int k = 0; k < addMe.Count; k++)
        {
            returnMe[k] = addMe[k];
        }

        return returnMe;
    }

    private string GetAvailablePlayerType(int number)
    {

        return "";
    }
}
