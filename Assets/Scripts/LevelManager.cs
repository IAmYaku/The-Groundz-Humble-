using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    //check if came from main menu basically
    // if not instantiate default
    //instantiate rest of players if needed..  asign appropriate player numbers and object names ... i.e "Player 1,2,3,4" etc.. 
    // get referee from game rule
    // handle spawn 
    // handle UI locations

    GameManager gameManager;

    string gameMode;

    GameRule gameRule;

    public Stage stage;                // make privates
    public CanvasGeneralGame cgg;

    CamController camController;

    bool isAtScene;

    int sceneIndex = 0;

   public TeamManager tm1;
    public TeamManager tm2;

    public List<GameObject> balls = new List<GameObject>();
    List<Vector3> ballSpwanLocations;

    List<GameObject> tm1UIs;
    List<GameObject> tm2UIs;


    GameObject logFeed;

    GameObject timerObj;
    int timer;

    bool start;

    int round =1 ;

    private bool ready;
    public bool isPlaying;
    public bool gameOver;

    bool isCelebrating;
    float celebrationTime = 5f;
    int countDownNum = 3;
    float countDown;

    public GameObject goFX;

    GameObject tm1ScoreObj;
    GameObject tm2ScoreObj;

    public bool team1Wins;
    public bool team2Wins;

    public int team1Points;
    public int team2Points;

    bool team1Scored;
    bool team2Scored;

    public int level;

    int difficultyScaler;
    int throwMag;
    int throwDecScalar;

    public Dictionary<GameObject, GameObject> throws = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, HashSet<GameObject>> hits = new Dictionary<GameObject, HashSet<GameObject>>();
    public Dictionary<GameObject, GameObject> catches = new Dictionary<GameObject, GameObject>();
    public List<Dictionary<int[], string>> outLog = new List<Dictionary<int[], string>>();

    public GameObject hitFX;
    public GameObject outFX;
    public GameObject outFX2;
    public GameObject catchFX;
    public GameObject winFX;                  // should be stage specific
    public GameObject plusOneFX;

    public HitPause hitPause;
    private float hitPauseDuration;

    public AudioSource audienceAudioSource;
    public AudioSource refAudioSource;

    public AudioClip cheer_Sound;
    public AudioClip DAMN_Sound;
    public float damMeter = 0f;
    public AudioClip[] audience_Sounds;
    public AudioClip[] out_Sounds;
    public AudioClip[] catch_Sounds;
    public AudioClip whistle_Sound;

     GameObject[] powerBalls;
     GameObject speedBall;


    void Start()
    {
        // if not gm
        gameManager = GetComponent<GameManager>();


        // if not tms
        tm1 = GlobalConfiguration.instance.team1Object.GetComponent<TeamManager>();
        tm2 = GlobalConfiguration.instance.team2Object.GetComponent<TeamManager>();

        countDown = countDownNum;


        /* P1Icon.GetComponent<Image>().sprite = Player1.GetComponent<Player>().playerIconImage;
         P2Icon.GetComponent<Image>().sprite = Player2.GetComponent<Player>().playerIconImage;
         P3Icon.GetComponent<Image>().sprite = Player3.GetComponent<Player>().playerIconImage;
         P4Icon.GetComponent<Image>().sprite = Player4.GetComponent<Player>().playerIconImage;

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
         P4StaminaBar = Player4.GetComponent<Player>().staminaBarObject;
         P4StaminaBar.SetActive(true);
         P4PowerBar = Player4.GetComponent<Player>().powerBarObject;
         P4PowerBar.SetActive(true);
        */
    }


    void Update()
    {

        if (ready && !isPlaying)
        {
            countDown =  (countDown - Time.deltaTime);

            if (countDown <= 0)
            {
                isPlaying = true;
                InstantiateGoFX();
                PlayWhistle();
            }
        }

        if (start)
        {

            //timer += Time.deltaTime;
            // timer_Text.text = ((int)timer).ToString();
            Text tm1ScoreText = tm1ScoreObj.GetComponent<Text>();
            Text tm2ScoreText = tm2ScoreObj.GetComponent<Text>();

            tm1ScoreText.text = ((int)team1Points).ToString();
            tm2ScoreText.text = ((int)team2Points).ToString();

            if (!GameOver())
            {
                Referee();
              //   UpdateDamMeter();

            }

            else    //gameOver
            {
                
                ready = false;

                 Invoke("SetIsPlayingFalse", 1f);

                if (team1Wins == false && team2Wins == false)
                {

                    celebrationTime = (celebrationTime - Time.deltaTime);

                    if (team1Scored)
                    {
                        camController.ZoomToSide(1);
                        Invoke("ResetTeam1Animators",2f);
                    }
                    else
                    {
                        if (team2Scored)
                        {
                            camController.ZoomToSide(2);
                            Invoke("ResetTeam2Animators",2f);
                            /*
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
                            */
                        }
                    }

                    if (celebrationTime <= 0)
                    {
                       //  Invoke("GameRestart", .5f);
                        GameRestart();

                    }
                }

                else
                {
                    if (gameOver)
                    {
                        start = false;
                        Invoke("EndGameMenu", 3f);
                    }
                }

            }

        }



       
           
    }

    private void ResetTeam1Animators()
    {   
            foreach (GameObject player in tm1.players)
            {
                
            Animator playerAnim = player.transform.GetChild(0).gameObject.GetComponent<PlayerConfiguration>().animator;

            playerAnim.SetBool("Running", false);
            playerAnim.SetBool("hasBall", false);

        }
    }

    private void ResetTeam2Animators()
    {
        foreach (GameObject player in tm2.players)
        {
            Animator playerAnim = player.transform.GetChild(0).gameObject.GetComponent<PlayerConfiguration>().animator;
            playerAnim.SetBool("Running", false);
            playerAnim.SetBool("hasBall", false);

        }
    }


        internal void SetStart(bool v)
    {
        start = v;
    }

    private void Referee()
    {
      	CheckHits();
      //	CheckTeamHasPlayer ();     // most likely have to switch and update pi indexes


    }
    private void CheckHits()
    {


        if (hits.Count > 0)
        {
            List<GameObject> toRemove = new List<GameObject>();
            foreach (GameObject ball in hits.Keys)
            {
                if (ball.GetComponent<Ball>().grounded)
                {
                    foreach (GameObject player in hits[ball])
                    {
                        player.GetComponent<Player>().isOut = true;                          // done twixce in remove... not sure where to put
                        // OutDisplayX2 (player.transform.GetChild (0).transform,ball);

                        String outType = "h";
                        Remove(player, outType, ball);
                        PlayOuts();

                    }
                    if (hits.ContainsKey(ball))
                    {
                        toRemove.Add(ball);
                    }
                }
            }
            foreach (GameObject ball in toRemove)
            {
                hits.Remove(ball);
                RemoveThrow(ball);
            }
        }
    }

    internal List<GameObject> GetPlayers()
    {
        List<GameObject> returnMe = new List<GameObject>();

        returnMe.AddRange(tm1.players);
        returnMe.AddRange(tm2.players);

      //  print("players.Size = " + returnMe.Count);

        return returnMe;
    }
    private void InstantiateGoFX()
    {
        Instantiate(goFX);
    }

    public void LoadLevel()
    {

        SetTeamSpawnLocations(1, stage.GetSpawnLocations(1, tm1.playerCount));
        SetTeamSpawnLocations(2, stage.GetSpawnLocations(2, tm2.playerCount));
        //SetTeamRetreatPoints(1, stage.GetSpawnLocations(1, tm1.playerCount);
        //SetTeamRetreatPoints(1, stage.GetSpawnLocations(1, tm1.playerCount);
        SetPlayerUI(1, tm1.playerCount);
        SetPlayerUI(2, tm2.playerCount);
        SetTeamScoreUI(1);
        SetTeamScoreUI(2);

        SetLogFeed();
        SetTimer();

        InstantiateBalls();

        tm1.EnableDropShadows(stage.playingLevelPlane);
        tm2.EnableDropShadows(stage.playingLevelPlane);

        tm1.StandByPlayers(false);
        tm2.StandByPlayers(false);

        //GetReferee or game rule

        start = true;
        ready = true;                                         // just needed for a few older references.. should strike to see when updating
    }

    internal GameRule CreateGameRule(string v)
    {
        if (v == "basic")
        {
            gameRule = new GR_Basic(gameMode, this);
        }

        if (v == "stock")
        {
            gameRule = new GR_Stock(gameMode, this);
        }

        return gameRule;
    }

    private void SetTeamScoreUI(int team)
    {
       if (team == 1)
        {
            tm1ScoreObj = cgg.GetTeamScoreObj(1);
        }


        if (team == 2)
        {
            tm2ScoreObj = cgg.GetTeamScoreObj(2);
        }
    }

    private void SetLogFeed()
    {
        logFeed = cgg.GetLogFeedObj();
    }

    private void SetTimer()
    {
        timerObj = cgg.GetTimerObj();
    }

    private void SetPlayerUI(int team, int playerCount)
    {
        

        if (team == 1)
        {
            List<GameObject> playerUIs = cgg.GetPlayerUIs(1);

            for (int i=0; i < playerCount; i++)
            {
                playerUIs[i].GetComponent<Image>().sprite = tm1.players[i].GetComponent<Player>().playerIconImage;
                playerUIs[i].SetActive(true);

                GameObject playerPower = playerUIs[i].transform.GetChild(0).gameObject;
                playerPower.SetActive(true);
                tm1.players[i].GetComponent<Player>().powerBarObject = playerPower ;
               

                GameObject playerStamina = playerUIs[i].transform.GetChild(1).gameObject;
                playerStamina.SetActive(true);
                tm1.players[i].GetComponent<Player>().staminaBarObject  =   playerStamina ;
            
            }
        }
        if (team == 2)
        {
            List<GameObject> playerUIs = cgg.GetPlayerUIs(2);

            for (int i = 0; i < playerCount; i++)
            {
                playerUIs[i].GetComponent<Image>().sprite = tm2.players[i].GetComponent<Player>().playerIconImage;
                playerUIs[i].SetActive(true);

                GameObject playerPower = playerUIs[i].transform.GetChild(0).gameObject;
                playerPower.SetActive(true);
                tm2.players[i].GetComponent<Player>().powerBarObject = playerPower;


                GameObject playerStamina = playerUIs[i].transform.GetChild(1).gameObject;
                playerStamina.SetActive(true);
                tm2.players[i].GetComponent<Player>().staminaBarObject = playerStamina;
            }

        }
    }

    private void InstantiateBalls()
    {
        List<Vector3> ballSpwanLocations = stage.GetBallSpawnLocations(gameRule.ballCount);

        for (int i =0; i< gameRule.ballCount; i++)
        {
         //   print("location = " + ballSpwanLocations[i]);
            GameObject ball = GlobalConfiguration.instance.InstantiateBallPrefab(ballSpwanLocations[i]);
            ball.GetComponent<DropShadow>().SetGroundObject(stage.playingLevelPlane);
            balls.Add(ball);

        }
    }

    public void SetStage(Stage x)
    {
        stage = x;
    }

    public void SetUICanvas(CanvasGeneralGame x)
    {
        cgg = x;
    }

    internal void SetCamera(CamController cC)
    {
        camController = cC;
    }

    public void SetMode(string mode)
    {
        /*
       // if (mode == "Basic")
        {
            MyJoystick.mode = mode;
            Controller3D.easyMove = true;
            Controller3D.hasThrowMag = true;
            Controller3D.throwMagnetism = throwMag;
            Controller3D.hasGrabMag = true;
            Controller3D.grabMag = grabMag;
            Controller3D.maxSeekVec = maxseekVec;
        }
        */

    }

    internal void SetIsAtScene(bool v)
    {
        isAtScene = v;


    }

    internal void SetSceneIndex(int x)
    {
        sceneIndex = x;

        print("Scene Index: " + x + " loaded..");
    }

    internal void SetTeamSpawnLocations(int team, List<Vector3> spawnPoints)
    {
        if (team == 1)
        {
            tm1.MoveToSpawnPoints(spawnPoints);
            tm1.FaceOpp(1);
        }
        if (team == 2)
        {
            tm2.MoveToSpawnPoints(spawnPoints);
            tm2.FaceOpp(2);
        }
    }

    internal void SetTeamRetreatPoints(int team, List<Vector3> spawnPoints)
    {
        if (team == 1)
        {
            tm1.SetRetreatPoints(spawnPoints);
        }
        if (team == 2)
        {
            tm2.SetRetreatPoints(spawnPoints);

        }
    }

    internal bool IsInGameBounds(Vector3 cockBackPos)
    {
      return  stage.IsInGameBounds(cockBackPos);
    }

    internal void SetGameMode(string v)
    {
        gameMode = v;
    }

    internal void SetGameRule(string v)
    {
        throw new NotImplementedException();
    }

    public void AddHit(GameObject ball, GameObject player)
    {
        // TODO Chain hits
        // i think a new world of logic presents itself when a ball hits multiplayers or multiple balls hit a player

        print("Adding hit");
        if (!hits.ContainsKey(ball))
        {
            HashSet<GameObject> players = new HashSet<GameObject>();
            players.Add(player);
            hits.Add(ball, players);
        }
        else
        {
            hits[ball].Add(player);
        }
    }

    public void AddThrow(GameObject ball, GameObject player)
    {
        if (!throws.ContainsKey(ball))
        {
            throws.Add(ball, player);
        }
    }


    internal void AddCatch(GameObject ball, GameObject parent)
    {
        catches.Add(ball, parent);
    }

    public void RemoveHit(GameObject ball)
    {
        if (hits.ContainsKey(ball))
        {
            hits.Remove(ball);

        }
    }

    public void RemoveThrow(GameObject ball)
    {
        if (throws.ContainsKey(ball))
        {
            throws.Remove(ball);
        }
    }

    public void LastThrowerOut(GameObject ball)
    {
        throws[ball].GetComponent<Player>().isOut = true;
        String outType = "c";
        Remove(throws[ball], outType, ball);
        ball.GetComponent<Ball>().DeactivateThrow();
        PlayCatches();
    }

    public void Remove(GameObject player, String outType, GameObject ball)
    {
       
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
                if (outType == "h")
        {
            int throwerNumber = throws[ball].GetComponent<Player>().number;
            int outNumber = player.GetComponent<Player>().number;
            int[] array = new int[2];
            array[0] = throwerNumber;
            array[1] = outNumber;
            Dictionary<int[], String> dict = new Dictionary<int[], string>();
            dict.Add(array, outType);
            outLog.Add(dict);
            LogOuts();
        }

        if (player.GetComponent<Player>().hasJoystick)
        {
            if (player.GetComponentInChildren<Controller3D>().ballGrabbed)
            {
                player.GetComponentInChildren<Controller3D>().DropBall();
            }
            player.GetComponentInChildren<Controller3D>().SetTouch0FXActivate(false);
            player.GetComponentInChildren<Controller3D>().isKnockedOut = false;
            player.GetComponentInChildren<Controller3D>().ballContact = false;
            player.GetComponentInChildren<Controller3D>().enabled = false;
            /*
            ParticleSystem ps = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ring_ps = ps.main;
            ParticleSystem ps_2 = player.transform.GetChild(0).gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule spikes_ps = ps_2.main;
            spikes_ps.simulationSpeed = 1;
            ring_ps.simulationSpeed = 1;
            */
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

        player.GetComponent<Player>().isOut = true;
        player.GetComponent<Player>().PlayOutSound();
        player.GetComponent<Player>().DeRender();


    }

    private void RemoveCatch(GameObject ball)
    {
        catches.Remove(ball);
    }

    public void GetAnotherPlayer(int team)           // GR check
    {
        
        bool somebodyOut = false;
        if (team == 1)
        {
            foreach (GameObject player in tm1.players)
            {
                if (player.GetComponent<Player>().isOut)
                {
                    somebodyOut = true;
                }
            }
            if (somebodyOut == true)
            {
                BringPlayerIn(1);
            }
        }
        if (team == 2)
        {
            foreach (GameObject player in tm2.players)
            {
                if (player.GetComponent<Player>().isOut)
                {
                    somebodyOut = true;
                }
            }
            if (somebodyOut == true)
            {
                BringPlayerIn(2);
            }
        }
        
    }

    public void BringPlayerIn(int team)
    {
        int index = 0;
        if (team == 1)
        {
            foreach (GameObject player in tm1.players)
            {
                if (player.GetComponent<Player>().isOut)
                {
                    index = tm1.players.IndexOf(player);
                    break;
                }
            }
            if (tm1.players[index].GetComponent<Player>().hasAI)
            {
                tm1.players[index].GetComponentInChildren<AI>().enabled = true;
                tm1.players[index].GetComponentInChildren<NavMeshAgent>().enabled = true;
                tm1.players[index].GetComponentInChildren<Rigidbody>().isKinematic = true;
            }
            else
            {
                tm1.players[index].GetComponentInChildren<Controller3D>().enabled = true;
                tm1.players[index].GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();
                tm1.players[index].GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
                tm1.players[index].GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            tm1.players[index].transform.GetChild(1).gameObject.SetActive(true);
            tm1.players[index].transform.GetChild(0).transform.position = tm1.players[index].GetComponent<Player>().childPos0;
            tm1.players[index].GetComponentInChildren<SpriteRenderer>().enabled = true;
            tm1.players[index].GetComponentInChildren<CapsuleCollider>().enabled = true;
            tm1.players[index].GetComponentInChildren<SphereCollider>().enabled = true;
            tm1.players[index].GetComponentInChildren<Rigidbody>().useGravity = true;
            tm1.players[index].GetComponent<Player>().isOut = false;
            print(" bringing 1 in");
        }
        if (team == 2)
        {
            foreach (GameObject player in tm2.players)
            {
                if (player.GetComponent<Player>().isOut)
                {
                    index = tm2.players.IndexOf(player);
                    break;
                }
            }
            if (tm2.players[index].GetComponent<Player>().hasAI)
            {
                tm2.players[index].GetComponentInChildren<AI>().enabled = true;
                tm2.players[index].GetComponentInChildren<NavMeshAgent>().enabled = true;
                tm2.players[index].GetComponentInChildren<Rigidbody>().isKinematic = true;
            }
            else
            {
                tm2.players[index].GetComponentInChildren<Controller3D>().enabled = true;
                tm2.players[index].GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();
                tm2.players[index].GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
                tm2.players[index].GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            tm2.players[index].transform.GetChild(1).gameObject.SetActive(true);
            tm2.players[index].transform.GetChild(0).transform.position = tm2.players[index].GetComponent<Player>().childPos0;
            tm2.players[index].GetComponentInChildren<SpriteRenderer>().enabled = true;
            tm2.players[index].GetComponentInChildren<CapsuleCollider>().enabled = true;
            tm2.players[index].GetComponentInChildren<SphereCollider>().enabled = true;
            tm2.players[index].GetComponentInChildren<Rigidbody>().useGravity = true;
            tm2.players[index].GetComponent<Player>().isOut = false;
            print(" bringing 2 in");
        }
    }

    internal void ClearContacts(GameObject ball)
    {
        if (hits.ContainsKey(ball))
        {
            foreach (GameObject player in hits[ball])
            {
                PlayerConfiguration pConfig = player.GetComponent<Player>().playerConfigObject.GetComponent<PlayerConfiguration>();
                pConfig.RemoveContat();

                    /*
                    ParticleSystem ps = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();                    //gr land ... I dont even think this gets unnormalized
                    ParticleSystem.MainModule ring_ps = ps.main;
                    ParticleSystem ps_2 = player.transform.GetChild(0).gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule spikes_ps = ps_2.main;
                    spikes_ps.simulationSpeed = 1;
                    ring_ps.simulationSpeed = 1;
                    */
                
            }
        }
    }

    private void LogOuts()
    {
        int outer = 0;
        int scorer = 0;
        string type = "";
        foreach (var group in outLog[outLog.Count - 1])
        {
            scorer = group.Key[0];
          //  print("scorer = " + scorer);
            outer = group.Key[1];
          //  print("outer = " + outer);
            type = (group.Value);
          //  print("type = " + type);
        }

        logFeed.GetComponent<GameLog>().AddToFeed(scorer, outer, type);
    }

            public void PlayWhistle()
            {
                if (!refAudioSource.isPlaying)
                {
                    refAudioSource.volume = 0.25f;
                    refAudioSource.priority = 150;
                    refAudioSource.clip = whistle_Sound;
                    refAudioSource.Play();
                }
        
            }

    public void CamShake(float intensity, Transform playerT)
    {
        camController.TrigCamShake(intensity, playerT);
    }

    internal void CamGlitch(float ballVelocity)
    {
        camController.GetComponent<CamController>().ActivateGlitch(ballVelocity);
    }

    public void HitDisplay(GameObject hittee, GameObject ball)
    {
        if (hitFX != null)
        {
            GameObject hitter = throws[ball].transform.GetChild(0).gameObject;
            Vector3 terPosition = hitter.transform.position;
            Color terColor = hitter.GetComponentInParent<Player>().color;

            Vector3 teePosition = hittee.transform.position;
            Color teeColor = hittee.GetComponentInParent<Player>().color;

            GameObject hfx = Instantiate(hitFX, teePosition, hitFX.transform.rotation);
            ParticleSystem ps = hfx.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psMain = ps.main;
            ParticleSystem.ColorOverLifetimeModule pscol = ps.colorOverLifetime;

            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(teeColor, 0.0f), new GradientColorKey(terColor, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(.5f, 0.0f) });

            pscol.color = grad;


            float ballVelocity = (ball.GetComponent<Rigidbody>().velocity.magnitude);
            float bv4s = Mathf.Clamp(ballVelocity / 30, 1.5f, 4);
            hfx.transform.localScale = new Vector3(bv4s, bv4s, bv4s);
            psMain.simulationSpeed = Mathf.Clamp(20 / (ballVelocity / 40), 15, 25);
        }

    }

    public void CatchDisplay(Vector3 position)
    {
        if (catchFX != null)
        {
            Instantiate(catchFX, position, catchFX.transform.rotation);
        }
    }

    internal void CatchDisplay(Vector3 position, float velocity)
    {
        if (catchFX != null)
        {
            GameObject catchFXObject1 = Instantiate(catchFX, position, catchFX.transform.rotation);
            ParticleSystem.MainModule catchPsMain1 = catchFXObject1.GetComponent<ParticleSystem>().main;
            ParticleSystem.VelocityOverLifetimeModule catchPsVelMod = catchFXObject1.GetComponent<ParticleSystem>().velocityOverLifetime;
            catchPsMain1.startSize = Mathf.Clamp((velocity / 40f), 1f, 2f);
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
         psMain.startColor = color;
        ParticleSystem.ColorOverLifetimeModule pscolm = psMain.colorOverLifetime;
        pscolm.color = new ParticleSystem.MinMaxGradient(Color.gray, color);
        Instantiate(outFX, playerT.position, outFX.transform.rotation);
    }

    public void OutDisplayX2(Transform controllerT, GameObject ball)
    {
        Color color = controllerT.gameObject.GetComponentInParent<Player>().color;

        if (outFX != null && outFX2 != null)
        {
            Transform t = controllerT;
            outFX2.transform.position = ball.transform.position;
            outFX2.GetComponent<ParticleSeek>().force = Vector3.Distance(controllerT.position, ball.transform.position) * 10;
            outFX2.GetComponent<ParticleSeek>().target = t;
            outFX2.GetComponent<ParticleSeek>().target.position = t.position;
            ParticleSystem.MainModule psMain2 = outFX2.GetComponent<ParticleSystem>().main;
            psMain2.startColor = ball.GetComponent<TrailRenderer>().startColor;
            Instantiate(outFX2, ball.transform.position, outFX2.transform.rotation);

            ParticleSystem psMain = outFX.GetComponent<ParticleSystem>();
             psMain.startColor = color;
            ParticleSystem.ColorOverLifetimeModule pscolm = psMain.colorOverLifetime;
            pscolm.color = new ParticleSystem.MinMaxGradient(Color.gray, color);
            Instantiate(outFX, controllerT.position, outFX.transform.rotation);

        }

    }

    public void WinDisplay(Vector3 position)
    {
        GameObject oneUp = Instantiate(plusOneFX);
        oneUp.transform.position = new Vector3(position.x * 2f, position.y - 7f, position.z);
        if (!winFX)
            winFX = Instantiate(winFX);
        winFX.SetActive(true);
        winFX.transform.position = position;
        Invoke("DestroyWinFX", 3);
    }

    public void DestroyWinFX()
    {
      //  Destroy(winFX);
    }


    public void PlayCheer()
    {
        
        if (!audienceAudioSource.isPlaying)
        {
            int ran = (int)(UnityEngine.Random.Range(0f, (float)(audience_Sounds.Length)));
            audienceAudioSource.clip = audience_Sounds[ran];
            audienceAudioSource.volume = .85f;
            audienceAudioSource.priority = 200;
            audienceAudioSource.Play();
        }
        
    }
    public void PlayDamn()
    {

        audienceAudioSource.clip = DAMN_Sound;
        audienceAudioSource.priority = 200;
        audienceAudioSource.volume = 0.75f;
        audienceAudioSource.pitch = 1f + damMeter;
        audienceAudioSource.Play();
        if (damMeter < 1f)
        {
            damMeter += .2f;
        }


    }
 

    
    public void PlayOuts()
    {
        int ran = (int)(UnityEngine.Random.Range(0f, (float)(out_Sounds.Length)));
        refAudioSource.clip = out_Sounds[ran];
        refAudioSource.priority = 120;
        refAudioSource.volume = 0.75f;
        refAudioSource.Play();
    }


    public void PlayCatches()
    {
        int ran = (int)(UnityEngine.Random.Range(0f, (float)(catch_Sounds.Length)));
        refAudioSource.clip = catch_Sounds[ran];
        refAudioSource.priority = 120;
        refAudioSource.volume = 0.75f;
        refAudioSource.Play();
    }
    

    public GameObject GetLogFeed()
    {
        return logFeed;
    }

    public void TeamScored(int team)
    {
        if (team == 1)
        {
            team1Points++;
        }

        if (team == 2)
        {
            team2Points++;
        }
    }
    internal void PostFX(string type)
    {
        if (type == "Player1Hit")
        {

        }
    }

    public void GameRestart(string reset)
    {

        round = 1; 
        ready = true;
        countDown = countDownNum;
        timer = 0;
        isCelebrating = false;
        camController.GetComponent<CamController>().Normal();
        hits.Clear();
        throws.Clear();

       // List<GameObject> players = new List<GameObject>();
      //  players.AddRange(tm1.players);
     //   players.AddRange(tm2.players);
     



        foreach (GameObject player in GetPlayers())
        {
            if (player.GetComponent<Player>().hasJoystick)
            {

                if (player.GetComponentInChildren<Controller3D>().ballGrabbed == true)
                {
                    player.GetComponentInChildren<Controller3D>().DropBall();
                }
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
            player.transform.GetChild(0).transform.position = Vector3.zero;
            player.GetComponent<Player>().isOut = false;
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            player.GetComponentInChildren<CapsuleCollider>().enabled = true;
            player.GetComponentInChildren<SphereCollider>().enabled = true;
            player.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            player.GetComponentInChildren<Rigidbody>().useGravity = true;
            if (player.GetComponent<Player>().hasAI)
            {

                player.GetComponentInChildren<AI>().enabled = true;
                player.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>().enabled = true;
                player.GetComponentInChildren<AI>().ResetLevel();
                player.GetComponentInChildren<Rigidbody>().isKinematic = true;
                 player.GetComponentInChildren<Animator>().runtimeAnimatorController = player.GetComponentInChildren<PlayerConfiguration>().play;

             //   if (team1Scored && player.GetComponent<Player>().team == 2 && joysticks.Length <= 1)                  // gr
                {
                    IncreaseAIIntensity(player.GetComponentInChildren<AI>(), difficultyScaler);
                }

            }
            else
            {
                  player.GetComponentInChildren<Animator>().runtimeAnimatorController = player.GetComponentInChildren<PlayerConfiguration>().play;
                player.GetComponentInChildren<Rigidbody>().isKinematic = false;
                player.GetComponentInChildren<Controller3D>().enabled = true;
                Controller3D.throwMagnetism = throwMag;
                player.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            }

        }
         foreach (GameObject ball in balls)
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

        // GR check 

        /*
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

        */
    }

    private void IncreaseAIIntensity(AI ai, float n)
    {
        ai.LevelIncrease(n);

    }

    private void DecreaseThrowMag(float difficultyScaler)
    {
        // GR check

       // if (mode == "Basic" && (Controller3D.throwMagnetism - throwDecScalar) >= 0.0)
        {
            Controller3D.throwMagnetism -= throwDecScalar;
        }

    }

    public void HitPause(float duration)
    {
        hitPause.Freeze(duration);
    }

    internal void HitPause()
    {
        hitPause = gameManager.GMFX.GetComponent<HitPause>();
        hitPause.Freeze(hitPauseDuration);
    }

    internal void SetHitPauseDuration(float hitPauseDur)
    {
        hitPauseDuration = hitPauseDur;
    }
    public void GameRestart()
    {

        round++;
        ready = true;
        countDown = countDownNum;
        timer = 0;
        celebrationTime = 5.0f;
        isCelebrating = false;
        camController.Normal();
        hits.Clear();
        throws.Clear();

        IncreaseLevelDifficulty();     // GR check

        foreach (GameObject player in GetPlayers())                                       //   methodize
        {
            GameObject playerconfigObject = player.transform.GetChild(0).gameObject;

            playerconfigObject.SetActive(true);
            player.GetComponent<Player>().isOut = false;

            Animator playerAnim = playerconfigObject.GetComponent<PlayerConfiguration>().animator;

             playerAnim.Rebind();
             playerAnim.Update(0f);
         //   print("ReEnabling player");

            playerconfigObject.GetComponent<SpriteRenderer>().enabled = true;                  
            playerconfigObject.GetComponent<CapsuleCollider>().enabled = true;
            playerconfigObject.GetComponent<SphereCollider>().enabled = true;
            playerconfigObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            playerconfigObject.GetComponent<Rigidbody>().useGravity = true;
            playerconfigObject.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<Player>().shadow.SetActive(true);  

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
             //   player.GetComponentInChildren<Controller3D>().SetTapThrowReadyToFalse();
             //   player.GetComponentInChildren<Controller3D>().ResetTouch1PhasePrev();
            //    player.GetComponentInChildren<Controller3D>().SetTouch0FXActivate(false);

                //   player.GetComponentInChildren<Controller3D>().animator.runtimeAnimatorController = player.GetComponentInChildren<Controller3D>().play;
                player.GetComponent<Player>().playerAura.SetActive(true);  //aura
                player.GetComponentInChildren<Controller3D>().FaceOpp();
            }

        }


        SetTeamSpawnLocations(1, stage.GetSpawnLocations(1, tm1.playerCount));
        SetTeamSpawnLocations(2, stage.GetSpawnLocations(2, tm2.playerCount));


        List<Vector3> ballSpwanLocations = stage.GetBallSpawnLocations(gameRule.ballCount);
        int i = 0;

        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            ball.GetComponent<SphereCollider>().enabled = true;
            ball.GetComponent<Ball>().DeactivateThrow();
            ball.transform.GetChild(2).gameObject.SetActive(false);
            ball.transform.position = ballSpwanLocations[i];

            float xBallDropForce = 1000 * UnityEngine.Random.Range(-1.0f, 1.0f);
            float zBallDropForce = 1000 * UnityEngine.Random.Range(-1.0f, 1.0f);


            ball.GetComponent<Rigidbody>().AddForce(new Vector3(round * xBallDropForce, 0f, round * zBallDropForce));

            i++;

        }
        team1Scored = false;
        team2Scored = false;

        if (round == 1)
        {
            // InstantiateSpeedBall();
        }

        PlayWhistle();
    }
    private bool GameOver()
    {
      //  if (TimeIsOut())
        {
          //  return true;
        }

        int i = 0;
        int j = 0;



        foreach (GameObject player in tm1.players)
        {
            if (player.GetComponent<Player>().isOut)
            {
                i++;
            }
        }

        foreach (GameObject player in tm2.players)
        {
            if (player.GetComponent<Player>().isOut)
            {
                j++;
            }
        }

        if (i == tm1.players.Count)
        {

            if (!team2Scored)
            {
                team2Points++;
                team2Scored = true;
                float rand = UnityEngine.Random.Range(-10.0f, 10.0f);
                WinDisplay(new Vector3(10 + rand / 5, 20, rand / 5));
                PlayCheer();
                PlayWhistle();
                print("~!!! Team 2 WiNS !!!~");
            }
            if (team2Points > 5)
            {
                team2Wins = true;
            }
            gameOver = true;
            return true;
        }
        if (j == tm2.players.Count)
        {

            if (!team1Scored)
            {
                team1Points++;
                team1Scored = true;
                float rand = UnityEngine.Random.Range(-10.0f, 10.0f);
                WinDisplay(new Vector3(-10 + rand / 5, 20, rand / 5));
                PlayCheer();
                PlayWhistle();
                print("~!!! Team 1 WiNS !!!~");
            }
            if (team1Points > 5)
            {
                gameOver = true;
                team1Wins = true;
            }
            return true;
        }
        return false;
    }

    public void GameReset()
    {
        gameOver = false;
        ready = true;
        isPlaying = false;
        team1Wins = false;
        team2Wins = false;
        team1Points = 0;
        team2Points = 0;
        team1Scored = false;
        team2Scored = false;
        level = 0;
         //GameRestart("reset");

        GameRestart();
    }

    void SetIsPlayingFalse()
    {
        isPlaying = false;
    }

    internal void EndGameMenu()
    {


        // adManager.LoadsSuperAwesomeVideo();
        //adManager.PlaySuperAwesomeVideo();

       cgg.GetComponent<EndGameMenuScript>().Pause();
        print("gameOver = " + gameOver);

    }
}
