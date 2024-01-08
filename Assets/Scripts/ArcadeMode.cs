using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMode : MonoBehaviour
{
    int level;
    LevelManager lm;
    // scenes visited
    List<string> oppsFaced = new List<string>();

    public float diffFactor = .5f;

    bool completed;

    string currentOppName;

    public static float throwMag = 6f;
    public static float seekVec = 100f;

    void Start()
    {
        
    }

     public ArcadeMode(LevelManager x)
    {
        lm = x;
        GameRule gr = lm.GetGameRule();
        gr.ballCount = 3;
        gr.roundsToWin = 5;
        // Controller3D.hasGrabMag = false;
       // Controller3D.grabMag = 10f;
        Controller3D.hasThrowMag = true;
        Controller3D.hasSeekVec = true;
       Controller3D.throwMagnetism = throwMag;
        Controller3D.maxSeekVec = seekVec;
}
    void Update()
    {
        
    }

    internal void levelUp()
    {
        level++;
        GameRule gr = lm.GetGameRule();
        gr.ballCount = Mathf.Clamp(3 + level, 3, 6);
        gr.roundsToWin = Mathf.Clamp(3 + level, 3, 6);
        currentOppName = GetNextOpp();

        if (level == 1)
        {
            float throwMag = Controller3D.throwMagnetism;
            Controller3D.throwMagnetism = throwMag / 2f;
        }

        if (level == 2)
        {
            Controller3D.hasThrowMag = false;

        }

        if (level == 3)
        {
            Controller3D.hasSeekVec = false;
        }

    }

    public string GetScene()
    {
        if (level == 0)
        {
            return "TheGroundzEast";
        }
        if (level == 1)
        {
            return "TheGym";
        }

        if (level == 2)
        {
            return "TheBlock";
        }

        if (level == 3)
        {
            return "TheGroundzWest";
        }

        return "TheGroundzEast";
    }

    internal int GetSceneIndex()
    {
        if (level == 0)
        {
            return 5;
        }
        if (level == 1)
        {
            return 6;
        }

        if (level == 2)
        {
            return 7;
        }

        if (level == 3)
        {
            return 8;
        }

        return 5;
    }

    public void AddOppCharacter(string oppChar)
    {
        oppsFaced.Add(oppChar);
    }

    public void SetCompleted( bool x)
    {
        completed = x;

        if (completed)
        {
            level = 0;
            oppsFaced.Clear();

        }
    }

    public bool GetCompleted()
    {
        if (level+1 >= 4)
        {
            completed = true;
        }

        return completed;
    }

    internal string GetCurrentOppName()
    {
        if (level >= 3)
        {
            currentOppName = GetRandomCharacterType();
        }

        print("currentOpp = " + currentOppName);
        return currentOppName;
    }

    private string GetNextOpp()
    {
        string returnMe ="Mack";
        bool MackFaced = false;
        bool KingFaced = false;
        bool NinaFaced = false;

        foreach (string opp in oppsFaced)
        {
            if (opp == "Mack" || opp == "mack")
            {
                MackFaced = true;
            }
            if (opp == "King" || opp == "king")
            {
                KingFaced = true;
            }
            if (opp == "Nina" || opp == "nina")
            {
                NinaFaced = true;
            }
        }

        if (KingFaced && !MackFaced && !NinaFaced)
        {
            currentOppName = "Nina";   // should start using Nina.name red
            return "Nina"; 
        }

        if (!KingFaced && MackFaced && !NinaFaced)
        {
            currentOppName = "Nina";   // should start using Nina.name red
            return "Nina";
        }
        if (!KingFaced && MackFaced && NinaFaced)
        {
            currentOppName = "King";   // should start using Nina.name red
            return "King";
        }

        if (KingFaced && !MackFaced && NinaFaced)
        {
            currentOppName = "Mack";   // should start using Nina.name red
            return "Mack";
        }
        if (KingFaced && MackFaced && NinaFaced)
        {
            currentOppName = GetRandomCharacterType();   // should start using Nina.name red
            return currentOppName;
        }


        return returnMe;
    }

    public void SetCurrentOpp(string oppName)
    {
        currentOppName = oppName;
    }

    private string GetRandomCharacterType()
    {
        string mack = Mack.name;
        string king = King.name;
        string nina = Nina.name;

        float ran = UnityEngine.Random.Range(0.0f, 1.0f);

        if (ran <= .33)
        {
            return nina;
        }

        if (ran > .33f && ran < .66f)
        {

            return king;
        }

        if (ran >= .66f)
        {
            return mack;
        }

        return "";
    }
}
