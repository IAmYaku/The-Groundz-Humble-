using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class TeamSelect : MonoBehaviour
{
    GameManager gameManager;

    public GameObject controllersRootObject;

    public PlayerModuleManager playerModuleManager;

    public GameObject controllerPrefab;

    List<GameObject> controllers;

    public List<GameObject> modules_temp_objs;


    int team1Count = 1;
    int team2Count = 1;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public bool isLoading;



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

    int readys;
    int readyCount = 0;

    public virtual void Start()
    {
        if (GlobalConfiguration.Instance.gameMode != GlobalConfiguration.GameMode.arcade)
        {
            GlobalConfiguration.Instance.SetGameMode("arcade");
            print("Setting game mode = " + GlobalConfiguration.Instance.gameMode);
        }

      //  print("Game mode == " + GlobalConfiguration.Instance.gameMode);

        GlobalConfiguration.Instance.GetJoysticks();

        controllers = new List<GameObject>();
        //modules_temp_objs = new List<GameObject>();

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

        readyCount = Mathf.Clamp(GlobalConfiguration.Instance.GetDeviceCount(), 0, 4);     //  4 or maxPlayerCount

        if (readyCount > 0)
        {
            for (int i = 0; i < readyCount; i++)
            {
                EnableModule(i, true);                                             //ones already enabled btw
            }
        }


    }



    public void EnableModule(int i, bool hasController)
    {
        modules_temp_objs[i].SetActive(true);

        if (hasController)
        {
            if (i == 0)
            {
                GameObject keyboardObject = modules_temp_objs[i].transform.GetChild(4).gameObject;
                keyboardObject.SetActive(false);
            }

            GameObject controllerObject = modules_temp_objs[i].transform.GetChild(3).gameObject;
            controllerObject.SetActive(true);
            Image controllerImage = controllerObject.GetComponent<Image>();
            controllerImage.color = GlobalConfiguration.Instance.stickColorGuide[i];

        }
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

            GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(false);
            tF = Time.realtimeSinceStartup;
            deltaT = tF - t0;
            if (deltaT >= 3)
            {
                // sprint(" Ready... Going to next  scene");
                SceneManager.LoadScene("StageSelect");
            }
        }
    }

    public void SetTeam1Count(string input)
    {
        int count = 1;
        try
        {
            count = Int32.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{input}'");
        }

        if (count == 1 || count == 2 || count == 3 || count == 4)                                // get GlobalConfig.maxPlayerCount
        {
            team1Count = count;
            GlobalConfiguration.Instance.SetTeamCount(1, team1Count);
        }
        else
        {
            team1Count = 1;
            GlobalConfiguration.Instance.SetTeamCount(1, team1Count);
        }

    }

    public void SetTeam2Count(string input)
    {
        int count = 1;
        try
        {
            count = Int32.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{input}'");
        }

        if (count == 1 || count == 2 || count == 3 || count == 4)             // get GlobalConfig.maxPlayerCount
        {
            team2Count = count;
            GlobalConfiguration.Instance.SetTeamCount(2, team2Count);
        }
        else
        {
            team2Count = 1;
            GlobalConfiguration.Instance.SetTeamCount(2, team2Count);
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
        module1.team = team;
    }

    public void Player2TeamSelect(int team)
    {
        module2.team = team;
    }

    public void Player3TeamSelect(int team)
    {
        module3.team = team;
    }

    public void Player4TeamSelect(int team)
    {
        module4.team = team;
    }




    internal void InstantiateControllerObject(int playerIndex, Color color)
    {

        print("Instating Controller Object :" + playerIndex);
        Vector3 pos = playerModuleManager.team1Module1.transform.position;

        GameObject controller = Instantiate(controllerPrefab, pos, Quaternion.identity);

        controller.transform.parent = controllersRootObject.transform;
        controller.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        controller.GetComponent<Image>().color = color;

        controllers.Add(controller);

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


    public void PlayerReady(int playerIndex)
    {
        readys++;


        //   print("Player Readdy!!!");
        if ((readyCount == readys || readyCount == 0) && !ready)
        {
            GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(false);       // needed because controllerObject instantiates when p1Object instantiates for what is only keyboard commmand...   and technically we're not tho lol

            ModuleDataToPlayers();

            int p1team = modules[0].team;

            GlobalConfiguration.Instance.PopulateAI(p1team);
            GlobalConfiguration.Instance.SetDefaultJoin(false);
            ready = true;

            Invoke("TriggerUIAnimation", .5f);
            t0 = Time.realtimeSinceStartup;
        }

    }

    public void PlayerReadyArcade(int playerIndex, string charSelected)
    {

        //   print("Player Readdy!!!");

        LevelManager lm = GlobalConfiguration.Instance.gameManager.levelManager;

        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(false);       // needed because controllerObject instantiates when p1Object instantiates for what is only keyboard commmand...   and technically we're not tho lol

        readyCount = Mathf.Clamp(GlobalConfiguration.gamepadStarts, 0,1);
        ModuleDataToPlayers();

        string firstOppChar = GetOppPlayerType(charSelected);
        print("firstOppChar " + firstOppChar);
        lm.AddOppsFaced(firstOppChar);
        lm.SetCurrentOpp(firstOppChar);
        GlobalConfiguration.Instance.PopulateArcadeAI(2, firstOppChar); // initial


        GlobalConfiguration.Instance.SetDefaultJoin(false);

        string arcadeSceneName = lm.GetArcadeSceneName();
        int arcadeSceneIndex = lm.GetArcadeSceneIndex();

        if (!Stage.loadedFromStage)
        {
            LoadGameLevel(arcadeSceneName);
        }




    }

    private void ModuleDataToPlayers()
    {

        LevelManager lm = GlobalConfiguration.Instance.gameManager.levelManager;

        TeamManager tm1 = lm.tm1;
        TeamManager tm2 = lm.tm2;

        if (readyCount == 0)
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


        for (int i = 0; i < readyCount; i++)
        {
            //   print("readyCount > 0");

            GameObject pObject = GlobalConfiguration.Instance.GetPlayerAtIndex(i);                 // already instantiated on player join   only valid before ai pop

            int team = modules[i].team;
            int charCount = 0;

            if (team == 1)
            {
                charCount = tm1.GetCharCount(modules[i].characterName);
            }

            if (team == 2)
            {
                charCount = tm2.GetCharCount(modules[i].characterName);
            }

            GlobalConfiguration.Instance.SetPlayerType(pObject, modules[0].characterName, charCount);

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

    public void SetReadyCount(int x)                // device count changed
    {
        readyCount = x;
    }

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

    public void LoadGameLevel(string sceneName)
    {
        if (!isLoading)
        {
            DestroyThemeMusic();
            StartCoroutine(LoadAsynchronously(sceneName));
            isLoading = true;
        }

    }

    public void LoadGameLevel(int sceneIndex)
    {
        if (!isLoading)
        {
            DestroyThemeMusic();
            SceneManager.LoadSceneAsync(sceneIndex);
             isLoading = true;
        }

    }

    IEnumerator LoadAsynchronously(string sceneName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";

            yield return null;
        }

    }

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.Instance.TurnThemeMusic(false);

    }
}