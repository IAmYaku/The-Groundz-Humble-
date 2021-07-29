using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{


    GameObject LogFeed;

    public LevelManager levelManager;

    GlobalConfiguration globalConfiguration;

    public GameObject GMFX;

   public static int ageThresh;

    public PlayerInputManager playerInputManager;



//    public GameObject camera;
/*
    public Nina NinaScript;
    public Nina Nina2Script;
    public Mack MackScript;
    public King KingScript;
    public Mack Mack2Script;
    public King King2Script;

    public GameObject P1Icon;
    public GameObject P2Icon;
    public GameObject P3Icon;
    public GameObject P4Icon;
    public GameObject P1StaminaBar;
    public GameObject P1PowerBar;
    public GameObject P2StaminaBar;
    public GameObject P2PowerBar;
    public GameObject P3StaminaBar;
    public GameObject P3PowerBar;
    public GameObject P4StaminaBar;
    public GameObject P4PowerBar;


    public string[] joysticks;
    public GameObject ball;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player3Prefab;
    public GameObject Player4Prefab;



   // public GameRules gameRules;


    private bool start = false;
    private bool ready;
   // public bool isPlaying;
    public int round;
    private float countDown;
    private float rsgNum = 3f;
    private bool gameOver = false;
    public int ballCount = 1;
    public static int playerCount = 4;
    public static float level;
    public static int agethresh = 6;
    public GameObject[] Players;
    //public GameObject[] Balls;
    public List<GameObject> Team1;
    public List<GameObject> Team2;

    /*
    public Dictionary<GameObject, GameObject> throws = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, HashSet<GameObject>> hits = new Dictionary<GameObject, HashSet<GameObject>>();

    public HitPause hitPause;
    private float hitPauseDuration;

    public Dictionary<GameObject, GameObject> catches = new Dictionary<GameObject, GameObject>();

    
    public float timer = 0f;
    private float t0;
    private float tF;

    public int team1Points;
    public int team2Points;
    public bool team1Wins;
    public bool team2Wins;
    public bool team1Scored;
    public bool team2Scored;

    public Text team1Score_Text;
    public Text team2Score_Text;
    public Text timer_Text;


    public GameObject timerTextObject;
    public GameObject team1ScoreObject;
    public GameObject team2ScoreObject;
    public GameObject hitFX;
    public GameObject catchFX;
    public GameObject outFX;
    public GameObject outFX2;
    public GameObject GoFX;
    public GameObject winFX;
    private GameObject winDis;
    public GameObject OneUpFX;

    public AudioSource audienceAudioSource;
    public AudioSource refAudioSource;
    public AudioClip cheer_Sound;
    public AudioClip DAMN_Sound;
    public float damMeter = 0f;
    public AudioClip[] audience_Sounds;
    public AudioClip[] out_Sounds;
    public AudioClip[] catch_Sounds;
    public AudioClip whistle_Sound;

    public bool isCelebrating;

    public List<Dictionary<int[], String>> outLog = new List<Dictionary<int[], String>>();
   // public static Dictionary<int, String> playerTypes = new Dictionary<int, String>();

  //  public static String gameMode = "Solo"; // Solo or Multiplayer
    public static String mode = "Basic";  // Basic or Advanced
    public static String view = "Keyboard"; // Keyboard or Virtual Joystick

    float throwMag = 2;
    float maxseekVec = 25f;
    float difficultyScaler = 3f;
    float throwDecScalar = 0.25f;
    float grabMag = 10f;

    public GameObject VirtualControllor;

    public GameObject CanvasGeneralGame;

    public GameObject[] powerBalls;
    private GameObject speedBall;

    */

    


    void Awake() {

        if (!levelManager)
        {
            levelManager = GetComponent<LevelManager>();
        }

        if (!globalConfiguration)
        {
            globalConfiguration = GetComponent<GlobalConfiguration>();
        }

        if (!GMFX)
        {
            GMFX = GameObject.Find("GM FX");
        }

        if (!playerInputManager)
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }
        /*
        if (!adManager)
        {
          if(GameObject.Find("Ad Manager"))
            {
               // adManager = GameObject.Find("Ad Manager").GetComponent<AdManager>();
            }
        }
           /*
   
     
        joysticks = Input.GetJoystickNames();
        joysticks = GetActualJoysticks(joysticks);

        if (gameMode == "Multiplayer")
        {

            Players = new GameObject[playerCount];

            if (!GameObject.Find("Player 1"))
            {
                Player1 = Instantiate(Player1Prefab, Player1Prefab.transform.position, Player1Prefab.transform.rotation);
            
                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }

                Players[0] = Player1;
            }
            else
            {
                Player1 = GameObject.Find("Player 1");

                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }
                else
                {
                    SetPlayerType(Player1, "Nina");
                }

                Players[0] = Player1;
            }

            if (!GameObject.Find("Player 2"))
            {
                Player2 = Instantiate(Player2Prefab, Player2Prefab.transform.position, Player2Prefab.transform.rotation);
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }
            else
            {
                Player2 = GameObject.Find("Player 2");
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }

            if (!GameObject.Find("Player 3"))
            {
                Player3 = Instantiate(Player3Prefab, Player3Prefab.transform.position, Player3Prefab.transform.rotation);
                Player3.GetComponent<Player>().childPos0 = Player3.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 3)
                {
                    SetPlayerType(Player3, playerTypes[3]);
                }
                else
                {
                    SetPlayerType(Player3, GetAvailablePlayerType(3));
                }

                Players[2] = Player3;
            }

            else
            {
                Player3 = GameObject.Find("Player 3");
                Player3.GetComponent<Player>().childPos0 = Player3.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 3)
                {
                    SetPlayerType(Player3, GetAvailablePlayerType(3));
                }

                Players[2] = Player3;

            }
            if (!GameObject.Find("Player 4"))
            {
                Player4 = Instantiate(Player4Prefab, Player4Prefab.transform.position, Player4Prefab.transform.rotation);
                Player4.GetComponent<Player>().childPos0 = Player4.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 4)
                {
                    SetPlayerType(Player4, playerTypes[4]);
                }
                else
                {
                    SetPlayerType(Player4, GetAvailablePlayerType(4));
                }

                Players[3] = Player4;
            }
            else
            {
                Player4 = GameObject.Find("Player 4");
                Player4.GetComponent<Player>().childPos0 = Player4.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 4)
                {
                    SetPlayerType(Player4, playerTypes[4]);
                }


                Players[3] = Player4;
            }


           

        }

        if (gameMode == "Solo")
        {

            Players = new GameObject[playerCount];

            if (!GameObject.Find("Player 1"))
            {
                Player1 = Instantiate(Player1Prefab, Player1Prefab.transform.position, Player1Prefab.transform.rotation);
                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }
                Players[0] = Player1;
            }
            else
            {
                Player1 = GameObject.Find("Player 1");
                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }
                else
                {
                    SetPlayerType(Player1, "Nina");
                }

                Players[0] = Player1;
            }

            if (!GameObject.Find("Player 2"))
            {
                Player2 = Instantiate(Player2Prefab, Player2Prefab.transform.position, Player2Prefab.transform.rotation);
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }
            else
            {
                Player2 = GameObject.Find("Player 2");
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }

            P3Icon.SetActive(false);
            P4Icon.SetActive(false);

            P1Icon.GetComponent<Image>().sprite = Player1.GetComponent<Player>().playerIconImage;
            P2Icon.GetComponent<Image>().sprite = Player2.GetComponent<Player>().playerIconImage;
            P1StaminaBar = Player1.GetComponent<Player>().staminaBarObject;
            P1StaminaBar.SetActive(true);
            P1PowerBar = Player1.GetComponent<Player>().powerBarObject;
            P1PowerBar.SetActive(true);
            P2StaminaBar = Player2.GetComponent<Player>().staminaBarObject;
            P2StaminaBar.SetActive(true);
            P2PowerBar = Player2.GetComponent<Player>().powerBarObject;
            P2PowerBar.SetActive(true);


        }

        if (playerCount == 3)
        {

            //unneccasary or better coding capacity vvv

            Players = new GameObject[playerCount];

            if (!GameObject.Find("Player 1"))
            {
                Player1 = Instantiate(Player1Prefab, Player1Prefab.transform.position, Player1Prefab.transform.rotation);
                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }
                Players[0] = Player1;
            }
            else
            {
                Player1 = GameObject.Find("Player 1");
                Player1.GetComponent<Player>().childPos0 = Player1.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 1)
                {
                    SetPlayerType(Player1, playerTypes[1]);
                }
                else
                {
                    SetPlayerType(Player1, "Nina");
                }

                Players[0] = Player1;
            }

            if (!GameObject.Find("Player 2"))
            {
                Player2 = Instantiate(Player2Prefab, Player2Prefab.transform.position, Player2Prefab.transform.rotation);
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }
            else
            {
                Player2 = GameObject.Find("Player 2");
                Player2.GetComponent<Player>().childPos0 = Player2.transform.GetChild(0).transform.position;
                if (playerTypes.Keys.Count >= 2)
                {
                    SetPlayerType(Player2, playerTypes[2]);
                }
                else
                {
                    SetPlayerType(Player2, GetAvailablePlayerType(2));
                }

                Players[1] = Player2;
            }

            if (!GameObject.Find("Player 3"))
            {
                Player3 = Instantiate(Player3Prefab, Player3Prefab.transform.position, Player3Prefab.transform.rotation);
                Player3.GetComponent<Player>().childPos0 = Player3.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 3)
                {
                    SetPlayerType(Player3, playerTypes[3]);
                }
                else
                {
                    SetPlayerType(Player3, GetAvailablePlayerType(3));
                }

                Players[2] = Player3;
            }

            else
            {
                Player3 = GameObject.Find("Player 3");
                Player3.GetComponent<Player>().childPos0 = Player3.transform.GetChild(0).transform.position;

                if (playerTypes.Keys.Count >= 3)
                {
                    SetPlayerType(Player3, GetAvailablePlayerType(3));
                }

                Players[2] = Player3;

            }

            P1Icon.GetComponent<Image>().sprite = Player1.GetComponent<Player>().playerIconImage;
            P2Icon.GetComponent<Image>().sprite = Player2.GetComponent<Player>().playerIconImage;
            P3Icon.GetComponent<Image>().sprite = Player3.GetComponent<Player>().playerIconImage;
            P4Icon.SetActive(false);

            if (Player1.GetComponent<Player>().staminaBarObject)
            {
                P1StaminaBar = Player1.GetComponent<Player>().staminaBarObject;
            }
            P1StaminaBar.SetActive(true);
            P1PowerBar = Player1.GetComponent<Player>().powerBarObject;
            P1PowerBar.SetActive(true);
            P2StaminaBar = Player2.GetComponent<Player>().staminaBarObject;
            P2StaminaBar.SetActive(true);
            P2PowerBar = Player2.GetComponent<Player>().powerBarObject;
            P2PowerBar.SetActive(true);
            P3StaminaBar = Player3.GetComponent<Player>().staminaBarObject;
            P3StaminaBar.SetActive(true);
            P3PowerBar = Player3.GetComponent<Player>().powerBarObject;
            P3PowerBar.SetActive(true);

        }

        if (mode == "")
        {
            mode = "Basic";
        }
        if (mode == "Basic")
        {
            SetMode(mode);
        }
        if (mode == "Advanced")
        {
            SetMode(mode);
        }

        SetView(view);
        */
    }
    /*
    internal void SetStart(bool v)
    {
        start = v;
    }

 

    private string[] GetActualJoysticks(string[] stix)
    {
        int j = 0;
        string[] returnMe;
        List<string> addMe = new List<string>();

        for (int i=0; i < joysticks.Length; i++)
        {
            if (stix[i] != "")
            {
                j++;
                addMe.Add(stix[i]);
            }
        }
        returnMe = new string[j];

        for (int k=0; k<addMe.Count;k++)
        {
            returnMe[k] = addMe[k];
        }

        return returnMe;
    }


    internal GameObject[] GetWalls()
    {
        // return gameRules.GetWalls();
        return null;
    }



   

    internal void PauseGame()
    {
        if (CanvasGeneralGame.GetComponent<PauseMenuScript>().GameIsPaused)
        {
           // CanvasGeneralGame.GetComponent<PauseMenuScript>().Resume();
        }
        else
        {
            CanvasGeneralGame.GetComponent<PauseMenuScript>().Pause();
        }
    }

    internal void EndGameMenu()
    {

        
       // adManager.LoadsSuperAwesomeVideo();
        //adManager.PlaySuperAwesomeVideo();
        CanvasGeneralGame.GetComponent<EndGameMenuScript>().Pause();
        print("gameOver = " + gameOver);

    }

    public void SetViewSetting(string mode)           // deprecated
    {
        RectTransform FixedJoystick = VirtualControllor.transform.GetChild(1).gameObject.GetComponent<RectTransform>();

        FixedJoystick.localScale = new Vector3(2f, 2f, 2f);
        FixedJoystick.localPosition = new Vector3(-660f, -680f, 0f);

        RectTransform ThrowUI = VirtualControllor.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        RectTransform PickUpUI = VirtualControllor.transform.GetChild(3).gameObject.GetComponent<RectTransform>();
        RectTransform DodgeUI = VirtualControllor.transform.GetChild(4).gameObject.GetComponent<RectTransform>();
        RectTransform SuperUI = VirtualControllor.transform.GetChild(5).gameObject.GetComponent<RectTransform>();

        ThrowUI.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        PickUpUI.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        DodgeUI.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        SuperUI.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        RectTransform ThrowButton = VirtualControllor.transform.GetChild(6).gameObject.GetComponent<RectTransform>();
        RectTransform PickUpButton = VirtualControllor.transform.GetChild(7).gameObject.GetComponent<RectTransform>();
        RectTransform DodgeButton = VirtualControllor.transform.GetChild(8).gameObject.GetComponent<RectTransform>();
        RectTransform SuperButton = VirtualControllor.transform.GetChild(9).gameObject.GetComponent<RectTransform>();

        ThrowButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        PickUpButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        DodgeButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        SuperButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        RectTransform PauseUI = VirtualControllor.transform.GetChild(10).gameObject.GetComponent<RectTransform>();
        RectTransform PauseButton = VirtualControllor.transform.GetChild(11).gameObject.GetComponent<RectTransform>();

        PauseUI.localScale = new Vector3(1f, 1f, 1f);
        PauseUI.localPosition = new Vector3(25f, -820f, 0f);
        PauseButton.localScale = new Vector3(1f, 1f, 1f);
        PauseButton.localPosition = new Vector3(25, -820, 1f);

        if (mode == "Basic")
        {
            if (view == "Virtual Joystick")
            {
                ThrowUI.localPosition = new Vector3(839f, -686f, 0.0f);
                ThrowButton.anchoredPosition = new Vector3(829.5f, -699.6f, 0.0f);

                PickUpUI.localPosition = new Vector3(495f, -764f, 0.0f);
                PickUpButton.localPosition = new Vector3(485, -807.2f, 0.0f);

                DodgeUI.gameObject.SetActive(false);
                DodgeButton.gameObject.SetActive(false);

                SuperUI.localPosition = new Vector3(602f, -564f, 0.0f);
                SuperButton.localPosition = new Vector3(598f, -569, 0.0f);
            }
        }

        if (mode == "Advanced")
        {
            if (view == "Virtual Joystick")
            {
             ThrowUI.localPosition = new Vector3(500.0f, -640.0f, 0.0f);
             ThrowButton.localPosition = new Vector3(490.5f, -653.9f, 0.0f);

             PickUpUI.localPosition = new Vector3(650f, -800f, 0.0f);
             PickUpButton.localPosition = new Vector3(640f, -847f, 0.0f);

             DodgeUI.gameObject.SetActive(true);
             DodgeButton.gameObject.SetActive(true);

             DodgeUI.localPosition = new Vector3(800f, -640f, 0.0f);
             DodgeButton.localPosition = new Vector3(790f, -654f, 0.0f);

            SuperUI.localPosition = new Vector3(600f, -525f, 0.0f);
            SuperButton.localPosition = new Vector3(596.1f, -528.7f, 0.0f);

            }

            MyJoystick.mode = mode;
            Controller3D.hasThrowMag = false;
            Controller3D.hasGrabMag = false;
        }
    }

  
    public void SetView(string view)        // deprecated
    {

        RectTransform FixedJoystick = VirtualControllor.transform.GetChild(1).gameObject.GetComponent<RectTransform>();

        RectTransform ThrowUI = VirtualControllor.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        RectTransform PickUpUI = VirtualControllor.transform.GetChild(3).gameObject.GetComponent<RectTransform>();
        RectTransform DodgeUI = VirtualControllor.transform.GetChild(4).gameObject.GetComponent<RectTransform>();
        RectTransform SuperUI = VirtualControllor.transform.GetChild(5).gameObject.GetComponent<RectTransform>();

        RectTransform ThrowButton = VirtualControllor.transform.GetChild(6).gameObject.GetComponent<RectTransform>();
        RectTransform PickUpButton = VirtualControllor.transform.GetChild(7).gameObject.GetComponent<RectTransform>();
        RectTransform DodgeButton = VirtualControllor.transform.GetChild(8).gameObject.GetComponent<RectTransform>();
        RectTransform SuperButton = VirtualControllor.transform.GetChild(9).gameObject.GetComponent<RectTransform>();

        if (view == "Keyboard")
        {

            FixedJoystick.gameObject.SetActive(false);

            ThrowUI.gameObject.SetActive(false);
            ThrowButton.gameObject.SetActive(false);

            PickUpUI.gameObject.SetActive(false);
            PickUpButton.gameObject.SetActive(false);

            DodgeUI.gameObject.SetActive(false);
            DodgeButton.gameObject.SetActive(false);

            SuperUI.gameObject.SetActive(false);
            SuperButton.gameObject.SetActive(false);

        }

        if (view == "Virtual Joystick")
        {

            FixedJoystick.gameObject.SetActive(true);
            Controller3D.vJoystick = FixedJoystick.GetComponent<FixedJoystick>();

            ThrowUI.gameObject.SetActive(true);
            ThrowButton.gameObject.SetActive(true);
            Controller3D.throwButton = ThrowButton.gameObject.GetComponent<ThrowButton>();

            PickUpUI.gameObject.SetActive(true);
            PickUpButton.gameObject.SetActive(true);
            Controller3D.pickUpButton = PickUpButton.gameObject.GetComponent<PickUpButton>();

            DodgeUI.gameObject.SetActive(true);
            DodgeButton.gameObject.SetActive(true);
            Controller3D.dodgeButton = DodgeButton.gameObject.GetComponent<DodgeButton>();

            SuperUI.gameObject.SetActive(true);
            SuperButton.gameObject.SetActive(true);
            Controller3D.superButton = SuperButton.gameObject.GetComponent<SuperButton>();

            SetViewSetting("Basic");

        }
    }

    void Start()
    {

     //   if (!gameRules)
        {
          //  gameRules = gameObject.GetComponent<GameRules>();
        }
        if (!LogFeed)
        {
          //  LogFeed = GameObject.Find("Log Feed");
        }
        if (!GoFX)
        {
        //    GoFX = GameObject.Find("GoFX");
        }

        //instantiate balls

        /*
        for (int i = 0; i < ballCount-1; i++)
        {
            float offSet = 15f;
            float nuZ = (gameRules.BackPlane.transform.position.z+(gameRules.FrontPlane.transform.position.z - gameRules.BackPlane.transform.position.z)/2 - 10 * i) + offSet;
			Instantiate (ball, new Vector3 (gameRules.halfCourtLine + UnityEngine.Random.Range(-2f,2f), 10f, nuZ), ball.transform.rotation);
           
            
        }

        Balls = GameObject.FindGameObjectsWithTag("Ball");
		foreach (GameObject ball in Balls) {
			ball.GetComponent<Ball> ().pos0 = ball.transform.position;
		}

        */

		//get players 

        /*
		int joystickCount = joysticks.Length;
    
            print("joystck count = " + joystickCount);


        if (joystickCount == 1){
            PlayerInit(1);
        }
		
		if (joystickCount == 2) {
            PlayerInit(1);
            PlayerInit(2);
        }
		if (joystickCount == 3) {
            PlayerInit(1);
            PlayerInit(2);
            PlayerInit(3);
        }
		if (joystickCount == 4) {
            PlayerInit(1);
            PlayerInit(2);
            PlayerInit(3);
            PlayerInit(4);
        }

        int j = 1;

        foreach (GameObject player in Players)
        { 
            if (player.GetComponent<Player>().team == 1)
            {
                Team1.Add(player);
				print ("adding one");
            }

            else
            {
                Team2.Add(player);
				print ("adding two");
            }
			if (player.GetComponent<Player> ().hasJoystick == false) {
				player.GetComponent<Player> ().enableAI ();
			}
        }

		timer_Text = timerTextObject.GetComponent<Text> ();
		team1Score_Text = team1ScoreObject.GetComponent<Text> ();
		team2Score_Text = team2ScoreObject.GetComponent<Text> ();

        print("Player Count  = " + playerCount);
        start = true;
        ready = true;

        
    }

    

    private void PlayerInit(int v)    // sets control player
    {
        if (v == 1)
        {
            print("Initializng  controller 1");

            Player1.GetComponent<Player>().enableController(1, joysticks[0]);

            GameObject aura = Player1.transform.GetChild(0).transform.GetChild(0).gameObject;
            aura.SetActive(true);
            Player1.GetComponent<Player>().playerAura = aura;
            ParticleSystem.MainModule auraMain = aura.GetComponent<ParticleSystem>().main;
            auraMain.startColor = Player1.GetComponent<Player>().color;
            ParticleSystem.MainModule aura0Main = aura.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            aura0Main.startColor = Player1.GetComponent<Player>().color;
            ParticleSystem.MainModule aura1Main = aura.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
            aura1Main.startColor = Player1.GetComponent<Player>().color;
        }
        if (v == 2)
        {
            print("Initializng controller 2");

            Player2.GetComponent<Player>().enableController(2, joysticks[1]);

            GameObject aura = Player2.transform.GetChild(0).transform.GetChild(0).gameObject;
            aura.SetActive(true);
            Player2.GetComponent<Player>().playerAura = aura;
            ParticleSystem.MainModule auraMain = aura.GetComponent<ParticleSystem>().main;
            auraMain.startColor = Player2.GetComponent<Player>().color;
            ParticleSystem.MainModule aura0Main = aura.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            aura0Main.startColor = Player2.GetComponent<Player>().color;
            ParticleSystem.MainModule aura1Main = aura.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
            aura1Main.startColor = Player2.GetComponent<Player>().color;
        }

        if (v == 3)
        {
            print("Initializng controller 3");

            Player3.GetComponent<Player>().enableController(3, joysticks[2]);

            GameObject aura = Player3.transform.GetChild(0).transform.GetChild(0).gameObject;
            aura.SetActive(true);
            Player3.GetComponent<Player>().playerAura = aura;
            ParticleSystem.MainModule auraMain = aura.GetComponent<ParticleSystem>().main;
            auraMain.startColor = Player3.GetComponent<Player>().color;
            ParticleSystem.MainModule aura0Main = aura.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            aura0Main.startColor = Player3.GetComponent<Player>().color;
            ParticleSystem.MainModule aura1Main = aura.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
            aura1Main.startColor = Player3.GetComponent<Player>().color;
        }
    

        if (v == 4)
        {

            print("Initializng controller 4");

            Player4.GetComponent<Player>().enableController(4, joysticks[3]);

            GameObject aura = Player4.transform.GetChild(0).transform.GetChild(0).gameObject;
            aura.SetActive(true);
            Player4.GetComponent<Player>().playerAura = aura;
            ParticleSystem.MainModule auraMain = aura.GetComponent<ParticleSystem>().main;
            auraMain.startColor = Player4.GetComponent<Player>().color;
            ParticleSystem.MainModule aura0Main = aura.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            aura0Main.startColor = Player4.GetComponent<Player>().color;
            ParticleSystem.MainModule aura1Main = aura.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
            aura1Main.startColor = Player4.GetComponent<Player>().color;
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
		if (ready && !isPlaying) {
			rsgNum = (rsgNum - Time.deltaTime);
            timer_Text.text = ((int)(rsgNum)).ToString();
            if (rsgNum <= 0) {
				isPlaying = true;
                ready = false;
                Instantiate(GoFX);
				PlayWhistle ();
			} 
		}

		if (start) {
      
            timer += Time.deltaTime;
			timer_Text.text = ((int)timer).ToString ();
			team1Score_Text.text = ((int)team1Points).ToString ();
			team2Score_Text.text = ((int)team2Points).ToString ();
			if (!GameOver ()) {
				Referee ();
				UpdateDamMeter ();
                countDown = 3f;

            } else {

                ready = false;
                Invoke("SetIsPlayingFalse",.5f);

				if (team1Wins == false && team2Wins == false)   {

                    countDown =( countDown-Time.deltaTime);
                    if (team1Scored)
                    {
                        camera.GetComponent<CamController>().ZoomToSide(1);
                        if (!isCelebrating)
                        {
                            if (Player1)
                            {
                                if (countDown <= 2f)
                                {
                                    Invoke("Player1WinAnimation", .125f);
                                    isCelebrating = true;
                                }
                            }

                            if (Player4)
                            {
                                if (countDown <= 2f)
                                {
                                    Invoke("Player4WinAnimation", .125f);
                                    isCelebrating = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (team2Scored)
                        {
                            camera.GetComponent<CamController>().ZoomToSide(2);
                            if (!isCelebrating)
                            {
                                if (Player2)
                                {
                                    if (countDown <= 2f)
                                    {
                                        Invoke("Player2WinAnimation", .125f);
                                        isCelebrating = true;
                                    }
                                }
                                if (Player3)
                                {
                                    if (countDown <= 2f)
                                    {
                                        Invoke("Player3WinAnimation", .125f);
                                        isCelebrating = true;
                                    }
                                }
                            }
                        }
                    }

                   

                    if (countDown <= 0)
                    {
                       Invoke("GameRestart",.125f);
                        
                    }
                }
                else {
                    if (gameOver)
                    {
                        start = false;
                        Invoke("EndGameMenu", 3f);
                    }
				}

			}
		}
        
    }
        
    /*
    private void SetIsPlayingFalse()
    {
      //  isPlaying = false;
    }

    private void Player1WinAnimation()
    {
        Player1.GetComponent<Player>().PlayWinAnimation();
    }

    private void Player2WinAnimation()
    {
        Player2.GetComponent<Player>().PlayWinAnimation();
    }

    private void Player3WinAnimation()
    {
        Player3.GetComponent<Player>().PlayWinAnimation();
    }

    private void Player4WinAnimation()
    {
        Player4.GetComponent<Player>().PlayWinAnimation();
    }

    private void RollCredits()
    {
        SceneManager.LoadScene("CreditsRolling");
    }

    internal void AddCatch(GameObject ball, GameObject parent)
    {
      //  catches.Add(ball, parent);
    }

    private void Referee()
    {
	//	CheckHits();
	//	CheckTeamHasPlayer ();     // most likely have to switch and update pi indexes


    }
    /*
    private bool GameOver()
    {
        if (TimeIsOut())
        {
            return true;
        }
			
        int i = 0;
        int j = 0;



        foreach (GameObject player in Team1)
        {
            if (player.GetComponent<Player>().isOut)
            {
                i++;
            }
        }
   
        foreach (GameObject player in Team2)
        {
            if (player.GetComponent<Player>().isOut)
            {
                j++;
            }
        }

		if (i == Team1.Count) {

            if (!team2Scored)
            {
                team2Points++;
                team2Scored = true;
                float rand = UnityEngine.Random.Range(-10.0f, 10.0f);
                WinDisplay(new Vector3(10 + rand / 5, 20, rand/5));
                PlayCheer();
                PlayWhistle();
                print("~!!! Team 2 WiNS !!!~");
            }
			if (team2Points > 5) {
				team2Wins = true;
			}
            gameOver = true;
            return true;
        }
		if (j== Team2.Count){

            if (!team1Scored)
            {
                team1Points++;
                team1Scored = true;
                float rand = UnityEngine.Random.Range(-10.0f, 10.0f);
                WinDisplay(new Vector3(-10 + rand / 5, 20, rand/5));
                PlayCheer();
                PlayWhistle();
                print("~!!! Team 1 WiNS !!!~");
            }
			if (team1Points > 5) {
                gameOver = true;
				team1Wins = true;
			}
			return true;
		} 
            return false;
        }

    */

    internal void ClearContacts(GameObject ball)
    {
        /*
        if (hits.ContainsKey(ball)) { 
        foreach (GameObject player in hits[ball])
        {
                if (player.GetComponent<Player>().hasAI)
                {
                    player.transform.GetChild(0).gameObject.GetComponent<AI>().ballContact = false;
                    player.transform.GetChild(0).gameObject.GetComponent<AI>().ballHit = null;
                }
                else
                {
                    player.transform.GetChild(0).gameObject.GetComponent<Controller3D>().ballContact = false;
                    player.transform.GetChild(0).gameObject.GetComponent<Controller3D>().ballHit = null;

                    ParticleSystem ps = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule ring_ps = ps.main;
                    ParticleSystem ps_2 = player.transform.GetChild(0).gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule spikes_ps = ps_2.main;
                    spikes_ps.simulationSpeed = 1;
                    ring_ps.simulationSpeed = 1;
                }
            }
        }
        */
    }

    private bool TimeIsOut()
    {
        return false;
    }

	public void LastThrowerOut(GameObject ball)
    {
        /*
        throws[ball].GetComponent<Player> ().isOut = true;
        String outType = "c";
		Remove(throws [ball], outType,ball);
        ball.GetComponent<Ball>().DeactivateThrow();
		PlayCatches ();
        */
    }
	public void AddThrow(GameObject ball, GameObject player)
	{
        /*
		if (!throws.ContainsKey (ball))  {
			throws.Add (ball, player);
		}
        */
	}

	public void AddHit(GameObject ball, GameObject player)
	{
		// TODO Chain hits
		// i think a new world of logic presents itself when a ball hits multiplayers or multiple balls hit a player
        /*
		if (!hits.ContainsKey (ball)) {
			HashSet <GameObject> players = new HashSet<GameObject> ();
			players.Add (player);
			hits.Add (ball, players);
		} else {
			hits [ball].Add (player);
		}
        */
	}
    /*
	public void GetAnotherPlayer( int team)
	{
		bool somebodyOut = false;
		if (team == 1) {
			foreach (GameObject player in Team1) {
				if (player.GetComponent<Player> ().isOut) {
					somebodyOut = true;
				}
			}
			if (somebodyOut == true) {
				BringPlayerIn (1);
			}
		}
		if (team == 2) {
			foreach (GameObject player in Team2) {
				if (player.GetComponent<Player> ().isOut) {
					somebodyOut = true;
				}
			}
			if (somebodyOut == true) {
				BringPlayerIn (2);
			}
		}
	}



	public void Remove(GameObject player, String outType, GameObject ball){
        

        if (outType == "c")
            {
                int throwerNumber = player.GetComponent<Player>().number;
              int catcherNumber = catches[ball].GetComponent<Player>().number;
                int[] array = new int[2];
                 array[0] = catcherNumber;
                array[1] = throwerNumber;
                Dictionary<int[], String> dict = new Dictionary<int[], string>();
                dict.Add(array, outType);
                outLog.Add(dict);
                LogOuts();
                RemoveCatch(ball);
            }
            else
                if (outType == "h") {
            //    int throwerNumber = throws[ball].GetComponent<Player>().number;
                int outNumber = player.GetComponent<Player>().number;
                int[] array = new int[2];
                array[0] = throwerNumber;
                array[1] = outNumber;
                Dictionary<int[], String> dict = new Dictionary<int[], string>();
                dict.Add(array, outType);
                outLog.Add(dict);
                LogOuts();
            }

            if (player.GetComponentInChildren<Controller3D>().enabled)
            {
                if (player.GetComponentInChildren<Controller3D>().ballGrabbed)
                {
                    player.GetComponentInChildren<Controller3D>().DropBall();
                }
            player.GetComponentInChildren<Controller3D>().SetTouch0FXActivate(false);
            player.GetComponentInChildren<Controller3D>().isKnockedOut = false;
            player.GetComponentInChildren<Controller3D>().ballContact = false;
            player.GetComponentInChildren<Controller3D>().enabled = false;
            ParticleSystem ps = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ring_ps = ps.main;
            ParticleSystem ps_2 = player.transform.GetChild(0).gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule spikes_ps = ps_2.main;
            spikes_ps.simulationSpeed = 1;
            ring_ps.simulationSpeed = 1;
        }
            else
            {
                if (player.GetComponentInChildren<AI>().enabled)
                {
                    if (player.GetComponentInChildren<AI>().ballGrabbed)
                    {
                        player.GetComponentInChildren<AI>().DropBall();
                    }
            player.GetComponentInChildren<AI>().isKnockedOut = false;
            player.GetComponentInChildren<AI>().ballContact = false;
            player.GetComponentInChildren<AI>().enabled = false;
            player.GetComponentInChildren<NavMeshAgent>().enabled = false;

                }
            }
        player.GetComponent<Player>().PlayOutSound();
        player.GetComponent<Player>().DeRender();
            

        }




    public void BringPlayerIn(int team){
		int index =0;
		if (team == 1) {
			foreach (GameObject player in Team1) {
				if (player.GetComponent<Player>().isOut) {
					index = Team1.IndexOf (player);
					break;
				}
			}
			if (Team1[index].GetComponent<Player> ().hasAI) {
				Team1[index].GetComponentInChildren<AI> ().enabled = true;
                Team1[index].GetComponentInChildren<NavMeshAgent>().enabled = true;
                Team1[index].GetComponentInChildren<Rigidbody>().isKinematic = true;
            } else {
				Team1[index].GetComponentInChildren<Controller3D> ().enabled = true;
                Team1[index].GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();
                Team1[index].GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
                Team1[index].GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            Team1[index].transform.GetChild(1).gameObject.SetActive(true);
            Team1[index].transform.GetChild (0).transform.position = Team1 [index].GetComponent<Player> ().childPos0; 
			Team1[index].GetComponentInChildren<SpriteRenderer> ().enabled = true;
			Team1[index].GetComponentInChildren<CapsuleCollider> ().enabled = true;
			Team1[index].GetComponentInChildren<SphereCollider> ().enabled = true;
			Team1[index].GetComponentInChildren<Rigidbody> ().useGravity = true;
			Team1[index].GetComponent<Player> ().isOut = false;
			print (" bringing 1 in");
		}
		if (team == 2) {
			foreach (GameObject player in Team2) {
				if (player.GetComponent<Player>().isOut) {
					index = Team2.IndexOf (player);
					break;
				}
			}
			if (Team2[index].GetComponent<Player> ().hasAI) {
				Team2[index].GetComponentInChildren<AI> ().enabled = true;
                Team2[index].GetComponentInChildren<NavMeshAgent>().enabled = true;
                Team2[index].GetComponentInChildren<Rigidbody>().isKinematic = true;
            } else {
				Team2 [index].GetComponentInChildren<Controller3D> ().enabled = true;
                Team2[index].GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();
                Team2[index].GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
                Team2[index].GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            Team2[index].transform.GetChild(1).gameObject.SetActive(true);
            Team2[index].transform.GetChild (0).transform.position = Team2[index].GetComponent<Player> ().childPos0;
			Team2[index].GetComponentInChildren<SpriteRenderer> ().enabled = true;
			Team2[index].GetComponentInChildren<CapsuleCollider> ().enabled = true;
			Team2[index].GetComponentInChildren<SphereCollider> ().enabled = true;
			Team2[index].GetComponentInChildren<Rigidbody> ().useGravity = true;
			Team2[index].GetComponent<Player> ().isOut = false;
			print (" bringing 2 in");
		}
	}

    internal void PostFX(string type)
    {
        if (type == "Player1Hit")
        {
            
        }
    }

    private void CheckHits(){

	
		if (hits.Count > 0) {
			List <GameObject> toRemove = new List<GameObject>();
			foreach (GameObject ball in hits.Keys)
            {
				if (ball.GetComponent<Ball> ().grounded) {
					foreach (GameObject player in hits[ball]) {
						player.GetComponent<Player> ().isOut = true;
						OutDisplayX2 (player.transform.GetChild (0).transform,ball);

                        String outType = "h";
						Remove(player,outType,ball);
                        PlayOuts ();

					}
					if (hits.ContainsKey (ball)) {
						toRemove.Add (ball);
					}
				}
			}
			foreach (GameObject ball in toRemove) {
					hits.Remove (ball);
                     RemoveThrow(ball);
			}
		}
	}

	public void RemoveThrow(GameObject ball){
        if (throws.ContainsKey(ball))  {

                throws.Remove(ball);

        }
	}

    public void RemoveHit(GameObject ball){
		if (hits.ContainsKey (ball)) {
			hits.Remove (ball);

		}
	}

    private void RemoveCatch(GameObject ball)
    {
        catches.Remove(ball);
    }

    public void GameRestart() {

        round++;
		ready = true;
		rsgNum = 3;
		timer = 0;
        isCelebrating = false;
        GetComponent<Camera>().GetComponent<CamController>().Normal();
        hits.Clear();
        throws.Clear();

        IncreaseLevelDifficulty();
        
		foreach (GameObject player in Players) {

            player.transform.GetChild(1).gameObject.SetActive(true);
            player.transform.GetChild(0).transform.position = player.GetComponent<Player>().childPos0;
            player.GetComponent<Player> ().isOut = false;
			player.GetComponentInChildren<SpriteRenderer> ().enabled = true;
            player.GetComponentInChildren<CapsuleCollider> ().enabled = true;
			player.GetComponentInChildren<SphereCollider> ().enabled = true;
            player.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            player.GetComponentInChildren<Rigidbody> ().useGravity = true;

            if (player.GetComponent<Player>().hasAI)
            {
                if (player.GetComponentInChildren<AI>().ballGrabbed)
                {
                    player.GetComponentInChildren<AI>().DropBall();
                }

                player.GetComponentInChildren<AI>().enabled = true;
                player.GetComponentInChildren<NavMeshAgent>().enabled = true;
                player.GetComponentInChildren<Rigidbody>().isKinematic = true;
           //     player.GetComponentInChildren<AI>().animator.runtimeAnimatorController = player.GetComponentInChildren<AI>().play;  
                player.GetComponentInChildren<AI>().FaceOpp();
            }
            else
            {
                if (player.GetComponentInChildren<Controller3D>().ballGrabbed == true)
                {
                    player.GetComponentInChildren<Controller3D>().DropBall();
                }

                player.GetComponentInChildren<Rigidbody>().isKinematic = false;
                player.GetComponentInChildren<Controller3D>().enabled = true;
                
                                                                                                        // if mode == virtual joystick
                player.GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();                                     
                player.GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
                player.GetComponentInChildren<Controller3D>().SetTouch0FXActivate(false);

             //   player.GetComponentInChildren<Controller3D>().animator.runtimeAnimatorController = player.GetComponentInChildren<Controller3D>().play;
                player.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                player.GetComponentInChildren<Controller3D>().FaceOpp();
            }
            
        }

      //  foreach (GameObject ball in Balls)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            ball.transform.position = ball.GetComponent<Ball>().pos0;
            ball.GetComponent<Ball>().DeactivateThrow();
            ball.transform.GetChild(2).gameObject.SetActive(false);
        }
        team1Scored = false;
        team2Scored = false;

        if (round == 1)
        {
        // InstantiateSpeedBall();
        }
        PlayWhistle();
    }

    private void IncreaseLevelDifficulty()   //  one sided, not modular
    {
        level++;

        if (team1Scored)   
        {
            DecreaseThrowMag(difficultyScaler);

        foreach (GameObject player in Players)
        {
                if (player.GetComponentInChildren<AI>())
                {
                    if (team1Scored && player.GetComponent<Player>().team == 2 && joysticks.Length <= 1)
                    {
                        IncreaseAIIntensity(player.GetComponentInChildren<AI>(), difficultyScaler);
                    }
                }
            }
        }
    }


    public void GameRestart(string reset)
    {

        round = 1; ;
        ready = true;
        rsgNum = 3;
        timer = 0;
        isCelebrating = false;
        GetComponent<Camera>().GetComponent<CamController>().Normal();
        hits.Clear();
        throws.Clear();

        foreach (GameObject player in Players)
        {
            if (player.GetComponentInChildren<Controller3D>().ballGrabbed == true)
            {
                player.GetComponentInChildren<Controller3D>().DropBall();
            }
            else
            {
                if (player.GetComponentInChildren<AI>())
                {
                    if (player.GetComponentInChildren<AI>().ballGrabbed)
                    {
                        player.GetComponentInChildren<AI>().DropBall();
                    }
                }
            }
            player.transform.GetChild(1).gameObject.SetActive(true);
            player.transform.GetChild(0).transform.position = player.GetComponent<Player>().childPos0;
            player.GetComponent<Player>().isOut = false;
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            player.GetComponentInChildren<CapsuleCollider>().enabled = true;
            player.GetComponentInChildren<SphereCollider>().enabled = true;
            player.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            player.GetComponentInChildren<Rigidbody>().useGravity = true;
            if (player.GetComponent<Player>().hasAI)
            {

                player.GetComponentInChildren<AI>().enabled = true;
                player.GetComponentInChildren<NavMeshAgent>().enabled = true;
                player.GetComponentInChildren<AI>().ResetLevel();
                player.GetComponentInChildren<Rigidbody>().isKinematic = true;
               // player.GetComponentInChildren<Animator>().runtimeAnimatorController = player.GetComponentInChildren<AI>().play;

                if (team1Scored && player.GetComponent<Player>().team == 2 && joysticks.Length <= 1)
                {
                    IncreaseAIIntensity(player.GetComponentInChildren<AI>(), difficultyScaler);
                }
              
            }
            else
            {
             //   player.GetComponentInChildren<Animator>().runtimeAnimatorController = player.GetComponentInChildren<Controller3D>().play;
              //  player.GetComponentInChildren<Controller3D>().animator.runtimeAnimatorController = player.GetComponentInChildren<Controller3D>().play;
                player.GetComponentInChildren<Rigidbody>().isKinematic = false;
                player.GetComponentInChildren<Controller3D>().enabled = true;
                Controller3D.throwMagnetism = throwMag;
                player.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            }

        }
       // foreach (GameObject ball in Balls)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            ball.transform.position = ball.GetComponent<Ball>().pos0;
            ball.GetComponent<Ball>().DeactivateThrow();
            ball.transform.GetChild(2).gameObject.SetActive(false);
        }
        team1Scored = false;
        team2Scored = false;

        if (round == 1)
        {
            // InstantiateSpeedBall();
        }

        PlayWhistle();
    }

    public void GameReset()
    {
        gameOver = false;
        ready = true;
       // isPlaying = false;
        team1Wins = false;
        team2Wins = false;
        team1Points = 0;
        team2Points = 0;
        team1Scored = false;
        team2Scored = false;
        level = 0;
        GameRestart("reset");
    }

    private void InstantiateSpeedBall()
    {
        GameObject speedBallType = powerBalls[0];
        speedBall =Instantiate(speedBallType);
        Vector3 force0 = speedBall.GetComponent<SpeedBall>().vel0;
        Invoke("AddForce",1);
        
    }

    private void AddForce()
    {
        Vector3 force0 = speedBall.GetComponent<SpeedBall>().vel0;
        speedBall.GetComponent<Rigidbody>().AddForce(force0);
    }

    private void DecreaseThrowMag(float difficultyScaler)
    {

        if (mode == "Basic" && (Controller3D.throwMagnetism - throwDecScalar) >= 0.0)
        {
            Controller3D.throwMagnetism -= throwDecScalar;
        }
        
    }


    private void IncreaseAIIntensity(AI ai, float n)
    {
        ai.LevelIncrease(n);
        
    }

    public void Pause (){
		Time.timeScale = 0;
		Invoke ("Resume", 3);

	}
	private void Resume(){
		start = true;
		Time.timeScale = 1;
	}

	public void ExecuteOuts(){
		foreach (GameObject player in Team1)
		{
			if (player.GetComponent<Player>().isOut == true)
			{
				Remove(player,"",null);
				//  print("~!!! OUT !!!~");
			}
		}
		foreach (GameObject player in Team2)
		{
			if (player.GetComponent<Player>().isOut == true)
			{
				Remove(player,"",null);
				//   print("~!!! OUT !!!~");
			}
		}
	}

	private void CheckTeamHasPlayer( )           // def  must revamp, especially when we dont consider 
    {                                                // most likely have to switch and update pi indexes
        foreach (GameObject player in Team1)
		{
			if (player.GetComponent<Player>().isOut && player.GetComponent<Player>().hasJoystick)
			{
				int joystickNumber = player.GetComponent<Player> ().joystick.number;
				foreach (GameObject other in Team1)
				{
					if (other.GetComponent<Player> ().hasAI && other.GetComponent<Player> ().isOut == false) {
                        if (joysticks.Length >= 1) {
                            other.GetComponent<Player>().enableController(joystickNumber, joysticks[joystickNumber - 1]);
                        }
                        else
                        {
                            other.GetComponent<Player>().enableController(joystickNumber, "");
                        }
						
						other.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive(true);
                        player.GetComponent<Player>().hasAI = true;
                        player.GetComponent<Player>().hasJoystick = false;
                        break;
					}
				}
			}
		}
		foreach (GameObject player in Team2)
		{
			if (player.GetComponent<Player>().isOut && player.GetComponent<Player>().hasJoystick)
			{
				int joystickNumber =  player.GetComponent<Player> ().joystick.number;
				foreach (GameObject other in Team2)
				{
					if (other.GetComponent<Player> ().hasAI && other.GetComponent<Player> ().isOut == false) {
						other.GetComponent<Player> ().enableController (joystickNumber, joysticks[joystickNumber - 1]);
                        Destroy(other.transform.GetChild(0).transform.GetChild(0).gameObject);
                        GameObject aura = Instantiate(player.transform.GetChild(0).transform.GetChild(0).gameObject,other.transform.GetChild(0));
                        aura.SetActive(true);
                        other.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive(true);
                        other.GetComponent<Player>().playerAura = aura;                                             // I think  we only need color
                        other.GetComponent<Player>().color = aura.GetComponent<ParticleSystem>().startColor;
                        player.GetComponent<Player>().hasAI = true;
                        player.GetComponent<Player>().hasJoystick = false;
                        break;
					}
				}
			}
		}
	}

	public void HitDisplay( GameObject hittee, GameObject ball) { 
		if (hitFX != null) {
            GameObject hitter = throws[ball].transform.GetChild(0).gameObject;
            Vector3 terPosition = hitter.transform.position;
            Color terColor = hitter.GetComponentInParent<Player>().color;

            Vector3 teePosition = hittee.transform.position;
            Color teeColor = hittee.GetComponentInParent<Player>().color;

			GameObject hfx = Instantiate (hitFX, teePosition, hitFX.transform.rotation);
            ParticleSystem ps = hfx.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psMain = ps.main;
            ParticleSystem.ColorOverLifetimeModule pscol = ps.colorOverLifetime;

            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(teeColor, 0.0f), new GradientColorKey(terColor, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(.5f, 0.0f) });

            pscol.color = grad;
           

            float ballVelocity =(ball.GetComponent<Rigidbody>().velocity.magnitude);
            float bv4s = Mathf.Clamp(ballVelocity / 30, 1.5f, 4);
            hfx.transform.localScale = new Vector3 (bv4s, bv4s, bv4s);
            psMain.simulationSpeed = Mathf.Clamp(20 / (ballVelocity / 40), 15, 25);
        }

	}

    public void CatchDisplay(Vector3 position){
		if (catchFX != null) {
			Instantiate (catchFX, position, catchFX.transform.rotation);
		}
	}

    internal void CatchDisplay(Vector3 position, float velocity)
    {
        if (catchFX != null)
        {
            GameObject catchFXObject1 = Instantiate(catchFX, position, catchFX.transform.rotation);
            ParticleSystem.MainModule catchPsMain1 = catchFXObject1.GetComponent<ParticleSystem>().main;
            ParticleSystem.VelocityOverLifetimeModule catchPsVelMod = catchFXObject1.GetComponent<ParticleSystem>().velocityOverLifetime;
            catchPsMain1.startSize = Mathf.Clamp((velocity /40f), 1f, 2f);
            catchPsVelMod.orbitalZMultiplier = 1 + velocity;

            GameObject catchFXObject2 = catchFXObject1.transform.GetChild(0).gameObject;
            ParticleSystem.MainModule catchPsMain2 = catchFXObject2.GetComponent<ParticleSystem>().main;
            catchPsMain2.startLifetime = Mathf.Clamp((velocity / 50f), 1f, 2.5f);
        }
    }

    internal void OutDisplay(GameObject gameObject)
    {
        Transform playerT = gameObject.transform;
        Color color = playerT.gameObject.GetComponentInParent<Player>().color;
        ParticleSystem psMain = outFX.GetComponent<ParticleSystem>();
        // psMain.startColor = color;
        ParticleSystem.ColorOverLifetimeModule pscolm = psMain.colorOverLifetime;
        pscolm.color = new ParticleSystem.MinMaxGradient(Color.gray, color);
        Instantiate(outFX, playerT.position, outFX.transform.rotation);
    }

    public void OutDisplayX2( Transform controllerT, GameObject ball) {
        Color color = controllerT.gameObject.GetComponentInParent<Player>().color;

        if (outFX != null && outFX2!= null) {
            Transform t = controllerT;
            outFX2.transform.position = ball.transform.position;
            outFX2.GetComponent<ParticleSeek>().force = Vector3.Distance(controllerT.position, ball.transform.position) * 10;
            outFX2.GetComponent<ParticleSeek>().target = t;
            outFX2.GetComponent<ParticleSeek>().target.position = t.position;
           ParticleSystem.MainModule psMain2 = outFX2.GetComponent<ParticleSystem>().main;
            psMain2.startColor = ball.GetComponent<TrailRenderer>().startColor;
            Instantiate(outFX2, ball.transform.position, outFX2.transform.rotation);

            ParticleSystem psMain = outFX.GetComponent<ParticleSystem>();
           // psMain.startColor = color;
            ParticleSystem.ColorOverLifetimeModule pscolm = psMain.colorOverLifetime;
            pscolm.color = new ParticleSystem.MinMaxGradient(Color.gray, color);
            Instantiate (outFX, controllerT.position, outFX.transform.rotation);
          
		}

	}

	public void WinDisplay( Vector3 position){
        GameObject oneUp = Instantiate(OneUpFX);
        oneUp.transform.position = new Vector3(position.x* 2f, position.y - 7f, position.z);
        if (!winDis)
		winDis = Instantiate (winFX);
        winDis.SetActive(true);
		winDis.transform.position = position;
			Invoke ("DestroyWinFX", 3);
	}

	public void DestroyWinFX()	{
		Destroy (winDis);
	}


	public void PlayCheer()	{
		if (!audienceAudioSource.isPlaying) {
            int ran = (int)(UnityEngine.Random.Range(0f, (float)(audience_Sounds.Length)));
            audienceAudioSource.clip = audience_Sounds[ran];
            audienceAudioSource.volume = .85f;
			audienceAudioSource.priority = 200;
			audienceAudioSource.Play ();
		}
	}
	public void PlayDamn()	{

		audienceAudioSource.clip = DAMN_Sound;
		audienceAudioSource.priority = 200;
		audienceAudioSource.volume = 0.75f;
		audienceAudioSource.pitch = 1f + damMeter;
		audienceAudioSource.Play ();
		if (damMeter< 1f) {
			damMeter += .2f;
        }
        
        
    }

	public void PlayOuts()	{
		int ran = (int)(UnityEngine.Random.Range (0f, (float)(out_Sounds.Length)));
		refAudioSource.clip = out_Sounds[ran];
		refAudioSource.priority = 120;
		refAudioSource.volume = 0.75f;
		refAudioSource.Play ();
	}

	public void PlayCatches(){
		int ran = (int)(UnityEngine.Random.Range (0f, (float)(catch_Sounds.Length)));
		refAudioSource.clip = catch_Sounds[ran];
		refAudioSource.priority = 120;
		refAudioSource.volume = 0.75f;
		refAudioSource.Play ();
	}

	public void PlayWhistle(){
		if (!refAudioSource.isPlaying) {
			refAudioSource.volume = 0.25f;
			refAudioSource.priority = 150;
			refAudioSource.clip = whistle_Sound;
			refAudioSource.Play ();
		}
	}

	private void UpdateDamMeter(){
		if (damMeter >= .2f) {
			damMeter -= 0.002f;
			audienceAudioSource.pitch = 1f + damMeter;
		}
	}

   public void CamShake(float intensity, Transform playerT)
    {
        GetComponent<Camera>().GetComponent<CamController>().TrigCamShake(intensity,playerT);
    }

    internal void CamGlitch(float ballVelocity)
    {
        GetComponent<Camera>().GetComponent<CamController>().ActivateGlitch(ballVelocity);
    }
    public String ToggleMode()
    {

       if (mode == "Advanced")
        {
            SetViewSetting("Basic");
            mode = "Basic";
            return "Advanced";
        }
        if (mode == "Basic")
        {
            SetViewSetting("Advanced");
            mode = "Advanced";
            return "Basic";
        }

        return "";
    }
    public String ToggleViewNMode()
    {

        if(view == "")
        {
            view = "KeyBoard";
        }

        if (view == "Keyboard")
        {
            view = "Virtual Joystick";
            SetView("Virtual Joystick");           
            Controller3D.mode = view;
            return "Keyboard";
        }
        if (view == "Virtual Joystick")
        {
            view = "Keyboard";
            SetView("Keyboard");
            Controller3D.mode = view;
            return "Virtual Joystick";
        }

        return "";
    }


    public void HitPause(float duration)
    {
        hitPause.Freeze(duration);
    }

    internal void HitPause()
    {
        hitPause.Freeze(hitPauseDuration);
    }

    internal void SetHitPauseDuration(float hitPauseDur)
    {
        hitPauseDuration = hitPauseDur;
    }

    private void LogOuts()
    {
        int outer = 0;
        int scorer = 0;
        string type = "";
        foreach (var group in outLog[outLog.Count - 1])
        {
            scorer = group.Key[0];
            outer = group.Key[1];
            type = (group.Value);
        }

        LogFeed.GetComponent<GameLog>().AddToFeed(scorer, outer, type);
    }
    */
}
