using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public List<GameObject> players = new List<GameObject>();
    List<GameObject> users = new List<GameObject>();
    List<GameObject> ais = new List<GameObject>();



    public int playerCount =1;  // set in teamSelect

    int aiCount;

    int userCount;

    int outCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    internal void AddObject(GameObject pObject, bool isUser)
    {
        pObject.transform.parent = this.transform;

        if (!players.Contains(pObject)) {
            players.Add(pObject);
        }


        if (isUser)
        {
       
            if(!users.Contains(pObject)) {
                userCount++;
                users.Add(pObject);
            }
       
        }
        else
        {
            if (!ais.Contains(pObject))
            {
                aiCount++;
                ais.Add(pObject);
            }
        }
    }

    internal void SetPlayerCount(int count)
        // parsed at TeamSelect scene only
    {
        playerCount = count;
    }



    internal List<GameObject> PopulateAI(int team)
    {
        List<GameObject> returnMe = new List<GameObject>();

        int x = playerCount - (userCount + aiCount);            // what if we want to replace
        
        for (int i=0; i<x; i++)
        {
            GameObject pObject = GlobalConfiguration.instance.InstantiateAIPrefab();
            Player pScript = pObject.GetComponent<Player>();
            pScript.team = team;
            pScript.hasAI = true;
           // pScript.enableAI();    // cant unlessAtScene

            pScript.SetOnStandby(true);

            GlobalConfiguration.instance.SetPlayerType(pObject, GetAvailablePlayerType());

            AddObject(pObject, false);      // gets unparented then reparented on global config level..

            returnMe.Add(pObject);
        }

        return returnMe;
    }

    private string GetAvailablePlayerType()
    {
        bool mackSelected = false;
        bool kingSelected = false;
        bool ninaSelected = false;

        foreach (GameObject player in GlobalConfiguration.instance.GetPlayers())
        {
            Player playerScript = player.GetComponent<Player>();

            if (playerScript.type == Mack.name)
            {
                mackSelected = true;
            }
            if (playerScript.type == King.name)
            {
                kingSelected = true;
            }
            if (playerScript.type == Nina.name)
            {
                ninaSelected = true;
            }
        }

        if (!ninaSelected && !Nina.isLocked)
        {
            return Nina.name;
        }
        if (!mackSelected && !Mack.isLocked)
        {
            return Mack.name;
        }
        if (!kingSelected && !King.isLocked)
        {
            return King.name;
        }

        else
        {
            return GetRandomCharacterType();
        }
    }

    internal void StandByPlayers(bool v)
    {
       foreach (GameObject player in players)
        {
            Player pScript = player.GetComponent<Player>();
            pScript.SetOnStandby(v);
        }
    }

    private string GetRandomCharacterType()
    {
        string mack = Mack.name;
        string king = King.name;
        string nina = Nina.name;

        float ran = UnityEngine.Random.Range(0.0f, 1.0f);

       /* if (ran < .33 && !Nina.isLocked)
        {
            return nina;
        }
       */
        if (ran > .5f && !King.isLocked)
        {
            return king;
        }

        if (ran <= .5f && !Mack.isLocked)
        {
            return mack;
        }

        return "";
    }

    internal void MoveToSpawnPoints(List<Vector3> spawnPoints)
    {
       for (int i=0; i< playerCount; i++)
        {
            players[i].transform.position = spawnPoints[i];
            players[i].transform.GetChild(0).localPosition = Vector3.zero;
            //NavMeshAgent.Warp(Vector3).
        }
    }

    internal void FaceOpp(int team)
    {
        if (team == 1)
        {
            foreach (GameObject pObject in players)
            {
                SpriteRenderer pSR = pObject.GetComponent<Player>().playerConfigObject.GetComponent<SpriteRenderer>();     // filthy lol
                pSR.flipX = false;
            }
        }

        if (team == 2)
        {
        foreach (GameObject pObject in players)
            {
                SpriteRenderer pSR = pObject.GetComponent<Player>().playerConfigObject.GetComponent<SpriteRenderer>();     // filthy lol
                pSR.flipX = true;

            }
        }
    }

    internal void EnableDropShadows(GameObject playingLevel)
    {
        foreach (GameObject player in players)
        {
            Player pScript = player.GetComponent<Player>();
            DropShadow pDropShadow = pScript.shadow.GetComponent<DropShadow>();
            pDropShadow.SetGroundObject(playingLevel);
            pDropShadow.gameObject.SetActive(true);
        }
    }

    internal void SetRetreatPoints(List<Vector3> spawnPoints)
    {
        for (int i = 0; i < playerCount; i++)
        {
            if (players[i].GetComponent<Player>().hasAI)
            {
                
               // players[i].transform.GetChild(2).GetComponent<AI>().retreatPoint.position = 
            }

        }
    }
}
