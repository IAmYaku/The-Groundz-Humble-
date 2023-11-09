using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Stage : MonoBehaviour
{
    GameManager gm;
    LevelManager lm;
    string name;
    int index;

    GameObject p1Icon;
    GameObject p2Icon;
    GameObject p3Icon;
    GameObject p4Icon;

    public List<Vector3> tm1_spawnPoints = new List<Vector3>();
    public List<Vector3> tm2_spawnPoints = new List<Vector3>();

    List<Vector3> ballSpawnPoints = new List<Vector3>();

    public float farSideLine; // +z
    public float nearSideLine; // -z
    public float halfCourtLine; // +x , 0
    public float baseLineLeft; //-x
    public float baseLineRight; // +x
    public float floor;  // -y
    public float roof; // +y

    public GameObject BottomPlane;
    public GameObject FrontPlane;
    public GameObject BackPlane;
    public GameObject LeftPlane;
    public GameObject RightPlane;
    public GameObject TopPlane;

    public GameObject[] walls = new GameObject[4];

    public GameObject halfCourtBox;


    public GameObject playingLevelPlane;

    public static bool loadedFromStage;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (!gm)
        {
            gm = GlobalConfiguration.Instance.gameManager.GetComponent<GameManager>();

        }


        lm = gm.levelManager;
       lm.SetStage(this);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GlobalConfiguration.Instance.SetIsAtScene(true, scene.buildIndex);


    }

    void Start()
    {
         if (GlobalConfiguration.Instance.gameMode == GlobalConfiguration.GameMode.none)
        {
            loadedFromStage = true;
            GlobalConfiguration.Instance.LoadDefaultGame();
            
        }

        InitBounds();
        lm.LoadLevel();
        print("Stage Start");

    }

    private void OnEnable()
    {
        /*
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (lm.GetSceneVisited(sceneIndex))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;


            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (!gm)
            {
                // gm = GlobalConfig.instaance.InstantiateGM
            }

            lm = gm.levelManager;
            lm.SetStage(this);

            InitBounds();
            // CreateSpawnLocations();
            lm.LoadLevel();
        }
        */
    }

    public List<Vector3> GetSpawnLocations(int team, int playerCount)
    {

        if (team == 1)
        {
            if (tm1_spawnPoints.Count == 0)
            {
                CreateSpawnLocations(team, playerCount);
            }

            
            return tm1_spawnPoints;
        }


        if (team == 2)
        {
            if (tm2_spawnPoints.Count == 0)
            {
         
                CreateSpawnLocations(team, playerCount);
            }

            else     // play local aka arcade
            {
                tm2_spawnPoints.Clear();
                CreateSpawnLocations(team, playerCount);
            }

            return tm2_spawnPoints;
        }

        return null;
    }

    private void CreateSpawnLocations(int team, int playerCount)
    {
        int zOffset = -15 * playerCount;
        int yOffset = 3;                           // takes into account size of players... since floor is @ 0
        float zDistMult = 30f;
        float xDistMult = .8f;

        if (team == 1)
        {
            for (int i=0; i< playerCount; i++)
            {
                Vector3 location = new Vector3(baseLineLeft * xDistMult, floor + yOffset, zOffset + i * zDistMult);
                print("tm1 location  (" + i + ") = " + location);
                tm1_spawnPoints.Add(location);

            }
        }

        if (team == 2)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Vector3 location = new Vector3(baseLineRight * xDistMult, floor + yOffset, zOffset + i * zDistMult);
                print("tm2 location  (" + i + ") = " + location);
                tm2_spawnPoints.Add(location);
            }
        }
    }



    internal List<Vector3> GetBallSpawnLocations(int ballCount)
    {
       if (ballSpawnPoints.Count ==0 && ballCount >0 )
        {
            for (int i=0; i<ballCount; i++)
            {
                int x = (int) halfCourtLine;
                 int y = (int) (roof / 2);
                int zOffset = -6;
                int zMult = 6;

                Vector3 ballPos = new Vector3(x, y, zOffset + zMult * i);
                ballSpawnPoints.Add(ballPos);
            }
        }

        return ballSpawnPoints;
    }


    private void InitBounds()
    {
        if (!playingLevelPlane || !BottomPlane || !LeftPlane || !RightPlane || !TopPlane || !FrontPlane || !BackPlane)
        {

            BottomPlane = GameObject.Find("Bottom Plane");
            LeftPlane = GameObject.Find("Left Plane");
            RightPlane = GameObject.Find("Right Plane");
            TopPlane = GameObject.Find("Top Plane");
            FrontPlane = GameObject.Find("Front Plane");
            BackPlane = GameObject.Find("Back Plane");
            playingLevelPlane = GameObject.FindGameObjectWithTag("Playing Level");


        }

        walls[0] = FrontPlane;
        walls[1] = BackPlane;
        walls[2] = LeftPlane;
        walls[3] = RightPlane;


        Bounds playingLevelBounds = playingLevelPlane.GetComponent<MeshCollider>().bounds;

        farSideLine = BackPlane.transform.position.z;       // we overlap planes so this might not be 100 accurate
        nearSideLine = FrontPlane.transform.position.z;
        halfCourtLine = halfCourtBox.transform.position.x;
        baseLineLeft = LeftPlane.transform.position.x;
        baseLineRight = RightPlane.transform.position.x;
        floor = playingLevelPlane.transform.position.y;
        roof = TopPlane.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal GameObject[] GetWalls()
    {
        return walls;
    }

    internal bool IsInGameBounds(Vector3 pos)
    {

        float padding;

       // print("pos = " + pos);
       // print(" baseLineLeft = " + baseLineLeft);
      //  print(" baseLineRight = " + baseLineRight);
      //  print(" farSideLine = " + farSideLine);
       // print(" nearSideLine = " + nearSideLine);
     //   print(" floor = " + floor);
       // print(" roof = " + roof);


        if (baseLineLeft < pos.x && pos.x < baseLineRight)
        {
            if (nearSideLine < pos.z && pos.z < farSideLine)
            {
                if (floor < pos.y && pos.y < roof)
                {
                    return true;
                }
            }
        }
        
        return false;

    }

    internal void ClearSpawnpoints(int v)
    {
       if (v == 2)
        {
            tm2_spawnPoints.Clear();
        }
       if (v == 1)
        {
            tm1_spawnPoints.Clear();
        }
    }

    internal void IncreaseSize(float difficultyScaler)
    {

        BackPlane.transform.localScale +=  Vector3.one * (difficultyScaler / 10f);
        FrontPlane.transform.localScale += Vector3.one * (difficultyScaler / 10f);
        LeftPlane.transform.localScale += Vector3.one * (difficultyScaler / 10f);
        RightPlane.transform.localScale += Vector3.one * (difficultyScaler / 10f);
        TopPlane.transform.localScale += Vector3.one * (difficultyScaler / 10f);
        BottomPlane.transform.localScale += Vector3.one * (difficultyScaler / 10f);

        BackPlane.transform.localPosition = new Vector3(BackPlane.transform.localPosition.x, BackPlane.transform.localPosition.y + 1.5f, BackPlane.transform.localPosition.z + 1.5f);
        FrontPlane.transform.localPosition = new Vector3(FrontPlane.transform.localPosition.x, FrontPlane.transform.localPosition.y + 1.5f, FrontPlane.transform.localPosition.z - 1.5f);

        RightPlane.transform.localPosition = new Vector3(RightPlane.transform.localPosition.x + 1.5f, RightPlane.transform.localPosition.y + 1.5f, RightPlane.transform.localPosition.z);
        LeftPlane.transform.localPosition = new Vector3(LeftPlane.transform.localPosition.x - 1.5f, LeftPlane.transform.localPosition.y + 1.5f, LeftPlane.transform.localPosition.z);

        TopPlane.transform.localPosition = new Vector3(TopPlane.transform.localPosition.x, TopPlane.transform.localPosition.y, TopPlane.transform.localPosition.z);
        BottomPlane.transform.localPosition = new Vector3(BottomPlane.transform.localPosition.x, BottomPlane.transform.localPosition.y, BottomPlane.transform.localPosition.z);


        GameObject navMeshBottomObject = BottomPlane.transform.GetChild(0).gameObject;
        NavMeshSurface navMeshSurface = navMeshBottomObject.GetComponent<NavMeshSurface>();
       
        navMeshSurface.BuildNavMesh();

        farSideLine = BackPlane.transform.position.z;       // we overlap planes so this might not be 100 accurate
        nearSideLine = FrontPlane.transform.position.z;
        halfCourtLine = halfCourtBox.transform.position.x;
        baseLineLeft = LeftPlane.transform.position.x;
        baseLineRight = RightPlane.transform.position.x;
        floor = playingLevelPlane.transform.position.y;
        roof = TopPlane.transform.position.y;

    }
}
