using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class RevampTeamSelect : MonoBehaviour
{
    public GameObject multiplayerEventSystemMainGamepad;

    public ModuleManager moduleManager;

    int ai1Count = 0;
    int ai2Count = 1;

    public class PlayerModule
    {
        public int number = 0;
        public string characterName = "Mack";
        public int team = 1;
        public int playerIndex = 1;
        public string controlType = "gamepad";   // keyboard, gamepad
                                                 // public bool isTaken;

        public PlayerModule(int x)
        {
            number = x;
        }
    }

    List<PlayerModule> modules;
    PlayerModule module1;
    PlayerModule module2;
    PlayerModule module3;
    PlayerModule module4;


    public Animator uiAnimator;
    public static int PlayerNum;
    protected bool ready;
    private float t0;
    private float tF;
    private float deltaT;

    // public ParticleSystem MackPS;
    //  public ParticleSystem KingPS;
    // public ParticleSystem NinaPS;

    private bool isNinaSelected;
    private bool isKingSlecetd;
    private bool isMackSelected;

    // public Animator MackAnimator;
    // public Animator KingAnimator;
    //  public Animator NinaAnimator;

    int selected;
    int selectCount; // obsolete

    public static int starts = 0;


    public virtual void Start()
    {
        //do checks... references, if doesnt have gm

        GlobalConfiguration.Instance.GetJoysticks();

       GlobalConfiguration.Instance.SetRevampTeamSelect(this);

       GlobalConfiguration.Instance.SetIsAtRevampTeamSelect(true);
        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(false);

        if (GlobalConfiguration.Instance.gameMode != GlobalConfiguration.GameMode.multiplayer)   //if loaded from quickCharSel stage
        {
            GlobalConfiguration.Instance.SetGameMode("multiplayer");
            print("Setting game mode = " + GlobalConfiguration.Instance.gameMode);
        }



        modules = new List<PlayerModule>();
        module1 = new PlayerModule(1);
        module2 = new PlayerModule(2);
        module3 = new PlayerModule(3);
        module4 = new PlayerModule(4);



        modules.Add(module1);
        modules.Add(module2);
        modules.Add(module3);
        modules.Add(module4);

        // CheckCharLocks();

        starts = 0;


    }



    public void EnableModule(int i)
    {
        moduleManager.EnableModule(i);
    }



    public void CheckCharLocks()
    {
        int i = 0;

        foreach (PlayerModule currentModule in modules)
        {
            bool MackIsLocked = GlobalConfiguration.Instance.GetCharIsLocked("Mack");
            bool KingIsLocked = GlobalConfiguration.Instance.GetCharIsLocked("King");
            bool NinaIsLocked = GlobalConfiguration.Instance.GetCharIsLocked("Nina");

            if (MackIsLocked)
            {
                //   currentModule.DisableChar("Mack");
            }

            if (KingIsLocked)
            {
                //  currentModule.DisableChar("King");
            }

            if (NinaIsLocked)
            {
                //   currentModule.DisableChar("Nina");
            }

            i++;
        }
    }

    public virtual void Update()
    {

        if (ready)
        {


            tF = Time.realtimeSinceStartup;
            deltaT = tF - t0;
            if (deltaT >= 3)
            {
                // sprint(" Ready... Going to next  scene");
                
            }
        }
    }

    public void SetAITeam1Count(int count)
    {
        

        if (count == 0 ||count == 1 || count == 2 || count == 3 || count == 4)                                // get GlobalConfig.maxPlayerCount
        {
            ai1Count = count;
      //      GlobalConfiguration.instance.SetTeamInitAICount(1, ai1Count);    // will add once created
        }
        else
        {
            ai1Count  = 1;
            //   GlobalConfiguration.instance.SetTeamInitAICount(1, ai1Count); // will add once created
        }
    }

    public void SetAITeam2Count(int count)
    {
      
        if (count == 0 || count == 1 || count == 2 || count == 3 || count == 4) {        
            ai2Count = count;
        //   GlobalConfiguration.instance.SetTeamInitAICount(1, ai2Count);  // will add once created
    
        }
        else
        {
            ai2Count = 1;
            // GlobalConfiguration.instance.SetTeamInitAICount(2, ai2Count);  // will add once created
        }
    }

    public void SetModule1CharacterType(string name)
    {
        // if name is legal ... == i.e Mack.name

        module1.characterName = name;
    }

    public void SetModule2CharacterType(string name)
    {
        // if name is legal ... == i.e Mack.name

        module2.characterName = name;
    }
    public void SetModule3CharacterType(string name)
    {
        // if name is legal ... == i.e Mack.name

        module3.characterName = name;
    }
    public void SetModule4CharacterType(string name)
    {

        // if name is legal ... == i.e Mack.name

        module4.characterName = name;
    }

    public void Player1TeamSelect(int team)
    {
        module1.team = team + 1;
    }

    public void Player2TeamSelect(int team)
    {
        module2.team = team + 1;
    }

    public void Player3TeamSelect(int team)
    {
        module3.team = team + 1;
    }

    public void Player4TeamSelect(int team)
    {
        module4.team = team + 1;
    }



    public void SetTeam1Modules(int team, int x)
    {
        /*
        if (team == 1)
        {
            if (team1Modules.Count == 0)    // init
            {
                for (int i = 0; i < x; i++)
                {
                 //   team1Modules.Add(new PlayerModule());
                }
            }
            else
            {
                if (team1Modules.Count > x)
                {
                    while (team1Modules.Count > x)
                    {
                        team1Modules.RemoveAt(team1Modules.Count - 1);
                    }
                }

                if (team1Modules.Count < x)
                {
                    while (team1Modules.Count < x)
                    {
                       // team1Modules.Add(new PlayerModule());
                    }
                }
            }
        }

        if (team == 2)
        {
            if (team2Modules.Count == 0)    // init
            {
                for (int i = 0; i < x; i++)
                {
                 //   team2Modules.Add(new PlayerModule());
                }
            }
            else
            {
                if (team2Modules.Count > x)
                {
                    while (team2Modules.Count > x)
                    {
                        team2Modules.RemoveAt(team2Modules.Count - 1);
                    }
                }

                if (team2Modules.Count < x)
                {
                    while (team2Modules.Count < x)
                    {
                     //   team2Modules.Add(new PlayerModule());
                    }
                }
            }
        }

        */
        //update ui
    }

    internal GameObject GetMultiplayerEventSystem(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                return moduleManager.multiplayerEventSystem1Gamepad;

            case 2:
                return moduleManager.multiplayerEventSystem2Gamepad;

            case 3:
                return moduleManager.multiplayerEventSystem3Gamepad;

            case 4:
                return moduleManager.multiplayerEventSystem4Gamepad;

        }

        return null;
    }

    internal void AddStart()
    {
        starts++;
    }

    public void PlayerReady(int playerIndex)
    {
        /*
        readys++;


        //   print("Player Readdy!!!");
        if ((readyCount == readys || readyCount == 0) && !ready)
        {
            GlobalConfiguration.instance.SetIsAtRevampTeamSelect(false);       // needed because controllerObject instantiates when p1Object instantiates for what is only keyboard commmand...   and technically we're not tho lol

            ModuleDataToPlayers();

            int p1team = modules[0].team;

            GlobalConfiguration.instance.PopulateAI(p1team);
            GlobalConfiguration.instance.SetDefaultJoin(false);
            ready = true;

            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }
        */
    }


    private void ModuleDataToPlayers()
    {

        LevelManager lm = GlobalConfiguration.Instance.gameManager.levelManager;
        TeamManager tm1 = lm.tm1;
        TeamManager tm2 = lm.tm2;

        int gamepadCount = Gamepad.all.Count;

        if (starts == 0)
        {
            //   print("readyCount = 0")

            GameObject player1 = GlobalConfiguration.Instance.CreatePlayer1();

            int team = modules[0].team;
            int charCount = 0;

            if (team == 1)
            {
                charCount = tm1.GetCharCount(modules[0].characterName);
            }

            if (team == 2)
            {
                charCount = tm2.GetCharCount(modules[0].characterName);
            }

            GlobalConfiguration.Instance.SetPlayerType(player1, modules[0].characterName, charCount);

            Player pScript = player1.GetComponent<Player>();
            pScript.enableController(-1);
            pScript.team = modules[0].team;

            pScript.SetOnStandby(true);

            GlobalConfiguration.Instance.AddPlayerToTeamManager(player1, pScript.team, true);

            pScript.SetColor(GlobalConfiguration.Instance.GetPlayerColor(1, pScript));

            player1.name = "Player " + "(" + pScript.type + ") " + pScript.number;
        }

        else

        for (int i = 0; i < starts; i++)
        {
            //   print("readyCount > 0");

            GameObject pObject = GlobalConfiguration.Instance.GetPlayerAtIndex(i);                 // already instantiated on player join   only valid before ai pop

            int team = modules[i].team;
            int charCount = 0;

                print("i = " + i);
                print("team = " + team);

                if (team == 1)
            {
                charCount = tm1.GetCharCount(modules[i].characterName);
            }

            if (team == 2)
            {
                charCount = tm2.GetCharCount(modules[i].characterName);
            }

                GlobalConfiguration.Instance.SetPlayerType(pObject, modules[i].characterName, charCount);

            Player pScript = pObject.GetComponent<Player>();
            pScript.enableController(i);
            pScript.team = modules[i].team;

            pScript.SetOnStandby(true);

            GlobalConfiguration.Instance.AddPlayerToTeamManager(pObject, pScript.team, true);

            pScript.SetColor(GlobalConfiguration.Instance.GetPlayerColor(i + 1, pScript));

            pObject.name = "Player " + "(" + pScript.type + ") " + pScript.number;

        }
    }

    private void MackSelect()
    {
        if (!ready)
        {
            //  if (!isMackSelected)
            {
                print("Mack");
                selected++;



                //   ParticleSystem.MainModule main = MackPS.main;
                //  main.simulationSpeed = 2f;

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

    private void NinaSelect()
    {
        if (!ready)
        {
            if (!isNinaSelected)
            {
                print("Nina");
                selected++;
                // isNinaSelected = true;
                // MackAnimator.SetFloat("Selected", 1f);

                //var main = NinaPS.main;
                // main.simulationSpeed = 8f;
            }
        }
        if (selectCount <= selected)
        {
            ready = true;
            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }


    }

    private void KingSelect()
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
                //  ParticleSystem.MainModule main = KingPS.main;
                // main.simulationSpeed = 6f;
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

    private string[] GetActualJoysticks(string[] stix)   // deprecated
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
    /*
    public void SetReadyCount(int x)                // device count changed
    {
        readyCount = x;
    }
    */

    private string GetOppPlayerType(string charName)

    {

        if (charName == Mack.name)
        {

            return King.name;
        }

        if (charName == King.name)
        {
            return Mack.name;
        }

        if (charName == Nina.name)
        {
            return GetRandomCharacterType();
        }

        return "Mack";
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

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.Instance.TurnThemeMusic(false);

    }


    public void ReadyButton()
    {

        print("Ready button");
        //check modules
        ModuleDataToPlayers();

        //foreach tm foreach aiCount - > create AI
        int p1team = modules[0].team;

        GlobalConfiguration.Instance.PopulateAIRevamp(p1team,ai1Count,ai2Count);
        GlobalConfiguration.Instance.SetDefaultJoin(false);


        GlobalConfiguration.Instance.SetIsAtRevampTeamSelect(false);
        GlobalConfiguration.Instance.ResetGamepadStarts();
        SceneManager.LoadScene("StageSelect");
    }

    public void BackButton()
    {
        GlobalConfiguration.Instance.Reset();
        SceneManager.LoadScene("GamemodeMenu");
    }

    }






