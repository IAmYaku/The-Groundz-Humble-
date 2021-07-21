using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleManager : MonoBehaviour
{

    public TeamSelect TeamSelect;

    int team1ModuleCount;
    int team2ModuleCount;

    public GameObject team1Module1;          // should check global config for max players first
    public GameObject team1Module2;
    public GameObject team1Module3;
    public GameObject team1Module4;

    public GameObject team2Module1;
    public GameObject team2Module2;
    public GameObject team2Module3;
    public GameObject team2Module4;

    Vector3 team1Module1Pos;          // should check global config for max players first
    Vector3 team1Module2Pos;
    Vector3 team1Module3Pos;
    Vector3 team1Module4Pos;

    Vector3 team2Module1Pos;          
    Vector3 team2Module2Pos;
    Vector3 team2Module3Pos;
    Vector3 team2Module4Pos;






   // public GameObject playerModulePrefab;



    void Start()
    {

        // if not cmc, find cmc
        // if not ts, find ts

        if (team1Module1)
        {
            team1Module1Pos = team1Module1.transform.position ;
        }
        if (team1Module2)
        {
            team1Module2Pos = team1Module2.transform.position ;
        }
        if (team1Module3)
        {
            team1Module3Pos = team1Module3.transform.position;
        }
        if (team1Module4)
        {
            team1Module4Pos =  team1Module4.transform.position ;
        }

        if (team2Module1)
        {
            team2Module1Pos = team2Module1.transform.position;
        }
        if (team2Module2)
        {
            team2Module2Pos = team2Module2.transform.position;
        }
        if (team2Module3)
        {
            team2Module3Pos =  team2Module3.transform.position;
        }
        if (team2Module4)
        {
            team2Module4Pos = team2Module4.transform.position;
        }
    }

    void Update()
    {
        
    }

    public void SetTeam1ModuleCount (int x)
    {
        team1ModuleCount = x;

    }

    public void SetTeam2ModuleCount(int x)
    {
        team2ModuleCount = x;
    }

    public Vector3 GetOpenModuleSpace()
    {
        // for every module if not isTaken
        // get open spot

        return Vector3.zero;
    }
}
