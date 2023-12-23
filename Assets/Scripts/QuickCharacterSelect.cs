using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem.UI;

public class QuickCharacterSelect : MonoBehaviour
{

    public TextMeshProUGUI characterSelectText;
    public TextMeshProUGUI pressStartText;

    bool gamepadStartPressed;

    public GameObject multiplayerEventSystem;

    public GameObject keyboardEventSystem;

    public GameObject mackContainerObject;


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

    PlayerModule module1;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public static int starts = 0;

    public bool isLoading;


    bool ready;


    public  void Start()
    {
        print("------- QuickCharacterSelect -------");


        if (GlobalConfiguration.Instance.gameMode != GlobalConfiguration.GameMode.arcade)    //if loaded from quickCharSel stage
        {
            GlobalConfiguration.Instance.SetGameMode("arcade");
            print("Setting game mode = " + GlobalConfiguration.Instance.gameMode);
        }

        //  print("Game mode == " + GlobalConfiguration.Instance.gameMode);

        GlobalConfiguration.Instance.GetJoysticks();


        module1 = new PlayerModule(1);
  

        // CheckCharLocks();

        GlobalConfiguration.Instance.SetQuickCharacterSelect(this);
        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(true);
        GlobalConfiguration.Instance.SetIsAtRevampTeamSelect(false);

        starts = 0;

    }

    public  QuickCharacterSelect()   // If not called from start
    {
        print(" Init QuickCharacterSelect");


        if (GlobalConfiguration.Instance.gameMode != GlobalConfiguration.GameMode.test)    //if loaded from quickCharSel stage
        {
            GlobalConfiguration.Instance.SetGameMode("arcade");
            print("Setting game mode = " + GlobalConfiguration.Instance.gameMode);
        }

        //  print("Game mode == " + GlobalConfiguration.Instance.gameMode);

        GlobalConfiguration.Instance.GetJoysticks();


        module1 = new PlayerModule(1);


        // CheckCharLocks();

        GlobalConfiguration.Instance.SetQuickCharacterSelect(this);
        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(true);
        GlobalConfiguration.Instance.SetIsAtRevampTeamSelect(false);

        starts = 0;
    }


    public void Update() 
    {
        Gamepad g = Gamepad.current;
        if (null !=g)        // gamepad
        {

            if (!gamepadStartPressed)
            {
                if (pressStartText.gameObject.activeSelf == false)
                {
                    pressStartText.gameObject.SetActive(true);
                   // characterSelectText.gameObject.SetActive(false);

                }
            }
        }
        else                        // keyboard
        {
            if (pressStartText.gameObject.activeSelf != false)
            {
                pressStartText.gameObject.SetActive(false);
               // characterSelectText.gameObject.SetActive(true);
            }
        }
    }


    public void PickMack()
    {
        if (!isLoading)
        {
        SetModule1CharacterType("Mack");

        print("PlayerReady Mack");
        PlayerReadyArcade(1,"Mack");
        }
    }

    public void PickKing()
    {
        if (!isLoading)
        {
            SetModule1CharacterType("King");

            print("PlayerReady King");
            PlayerReadyArcade(1, "King");
        }
    }

    public void LoadGameLevel(string sceneName)
    {
        multiplayerEventSystem.SetActive(false);
        keyboardEventSystem.SetActive(false);

        if (!isLoading)
        {
            DestroyThemeMusic();
            StartCoroutine(LoadAsynchronously(sceneName));
            isLoading = true;
        }

    }

    public void SetModule1CharacterType(string name)
    {
        // if name is legal ... == i.e Mack.name

        module1.characterName = name;
    }

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.Instance.TurnThemeMusic(false);

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

    internal void SetStartText(bool v)  // poor naming
    {

        if (!v)
        {
            gamepadStartPressed = true;
            pressStartText.gameObject.SetActive(false);
            characterSelectText.gameObject.SetActive(true);
        }
    }
    public void BackButton()
    {
        GlobalConfiguration.Instance.Reset();
        SceneManager.LoadScene("GamemodeMenu");
    }

    internal EventSystem GetEventSystem()
    {
        throw new NotImplementedException();
    }

    internal void SetFirstSelectedToMack()
    {
        multiplayerEventSystem.SetActive(true);
        multiplayerEventSystem.GetComponent<MultiplayerEventSystem>().firstSelectedGameObject = mackContainerObject;
    }

    public void PlayerReadyArcade(int playerIndex, string charSelected)
    {

        //   print("Player Readdy!!!");

        LevelManager lm = GlobalConfiguration.Instance.gameManager.levelManager;

        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(false);       // needed because controllerObject instantiates when p1Object instantiates for what is only keyboard commmand...   and technically we're not tho lol

        ModuleDataToPlayer();

        string firstOppChar = GetOppPlayerType(charSelected);
        print("firstOppChar " + firstOppChar);
        lm.AddOppsFaced(firstOppChar);
        lm.SetCurrentOpp(firstOppChar);
        GlobalConfiguration.Instance.PopulateArcadeAI(2, firstOppChar); // initial


        GlobalConfiguration.Instance.SetDefaultJoin(false);

        string arcadeSceneName = lm.GetArcadeSceneName();
        int arcadeSceneIndex = lm.GetArcadeSceneIndex();

        GlobalConfiguration.Instance.ResetGamepadStarts();

        if (!Stage.loadedFromStage)
        {
            LoadGameLevel(arcadeSceneName);
        }




    }

    private void ModuleDataToPlayer()
    {

        LevelManager lm = GlobalConfiguration.Instance.gameManager.levelManager;

        TeamManager tm1 = lm.tm1;
        TeamManager tm2 = lm.tm2;

        GameObject player1 = null ;

            if (starts == 0)
        {
             player1 = GlobalConfiguration.Instance.CreatePlayer1();
        }

            else
        {
      
                player1 = GlobalConfiguration.Instance.GetPlayerAtIndex(0);

        }
           
          

            int team = module1.team;
            int charCount = 0;

            if (team == 1)
            {
                charCount = tm1.GetCharCount(module1.characterName);
            }

            if (team == 2)
            {
                charCount = tm2.GetCharCount(module1.characterName);
            }

            GlobalConfiguration.Instance.SetPlayerType(player1, module1.characterName, charCount);

            Player pScript = player1.GetComponent<Player>();
            pScript.enableController(-1);
            pScript.team = module1.team;

            pScript.SetOnStandby(true);

            GlobalConfiguration.Instance.AddPlayerToTeamManager(player1, pScript.team, true);

            pScript.SetColor(GlobalConfiguration.Instance.GetPlayerColor(1, pScript));

            player1.name = "Player " + "(" + pScript.type + ") " + pScript.number;


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


}
