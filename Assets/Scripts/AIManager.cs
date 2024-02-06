using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public TeamManager teamManager;

    List<GameObject> aiList = new List<GameObject>();

    bool set;

    public bool isRunning;

    bool isOrchestrating;
    float orchestraCoolTime = 0f;
    float orchestraCoolTimeMultiplier = 10f;

    Orchestra currentOrchestra;

    List<Orchestra> orchestraList;

    interface Orchestra 
    {

        bool isInit { get; set; }
        bool isComplete { get; set; }
        int stepIndex { get; set; }

        public void Init(List<GameObject> aiList);

        public void Run();

        public interface OrchestraAction
        {
             bool isComplete { get; set; }

            GameObject aiObject{ get; set; }

           void Action();
        }
        List <List< OrchestraAction>> orchestraActionSteps { get; set; }

    }

    class MightyDuck : Orchestra
    {
        public bool isInit { get; set; }
       
        public bool isComplete { get; set; }
        public List<List<Orchestra.OrchestraAction>> orchestraActionSteps { get; set ; }
        public int stepIndex { get; set; }

        public void Init(List<GameObject> aiList)     // what if theres a mismatch in player count and actions?
        {

            print("Migty Duck Init");
            isInit = true;

            orchestraActionSteps = new List<List<Orchestra.OrchestraAction>>();

            List<Orchestra.OrchestraAction> step1 = new List<Orchestra.OrchestraAction>();
            List<Orchestra.OrchestraAction> step2 = new List<Orchestra.OrchestraAction>();

            /*
             * 
             * 
             */

            orchestraActionSteps.Add(step1);
            orchestraActionSteps.Add(step2);


            int stepIndex = 0;

            foreach (List<Orchestra.OrchestraAction> orchestraStep in orchestraActionSteps)   // also diversity vs uniform in steps can be applied here
            {
                if (stepIndex == 0)
                {
                    foreach (GameObject aiObject in aiList)
                    {
                        Orchestra.OrchestraAction orchestraAction = new RetrieveBallandReturnToSpawnPoint(aiObject);
                        orchestraStep.Add(orchestraAction);
                    }        
                }

                if (stepIndex == 1)
                {
                    foreach (GameObject aiObject in aiList)
                    {
                        Orchestra.OrchestraAction orchestraAction = new ChargeAndThrow(aiObject);
                        orchestraStep.Add(orchestraAction);
                    }
                }

                stepIndex++;
            }


        }


        public void Run()
        {
            if (stepIndex < orchestraActionSteps.Count)
            {
                isComplete = false;
                List<Orchestra.OrchestraAction> currentStep = orchestraActionSteps[stepIndex];


                int actionsCompleted = 0;
                foreach (Orchestra.OrchestraAction orchestraAction in currentStep)
                {
                    orchestraAction.Action();

                    if (orchestraAction.isComplete)
                    {
                        actionsCompleted++;
                    }

                    if (actionsCompleted == currentStep.Count)
                    {
                        stepIndex++;
                    }
                }
            }

            else
            {
                isComplete = true;
                print("~AI Orchestra completed~!");
            }
        }

        public class RetrieveBallandReturnToSpawnPoint : Orchestra.OrchestraAction
        {
            public bool isComplete { get ; set ; }
            public GameObject aiObject { get ; set ; }

            public RetrieveBallandReturnToSpawnPoint(GameObject aiIn)
            {
                aiObject = aiIn;

                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                ai.isOrchestrating = true;
            }

            public void Action()
            {
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                GameManager gameManager = GlobalConfiguration.Instance.gameManager;
                if (!isComplete)
                {
                    ai.EvaluateGameState();
                    if (ai.gameState == AI.GameState.dangerous)
                    {
                        ai.SetState(ai.panic_);
                        ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                    }

                    else
                    {
                        if (!ai.ballGrabbed)
                        {
                            ai.SetState(ai.getBall_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                        }

                        else
                        {
                            ai.SetState(ai.retreat_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                            
                            if (ai.IsAtRetreatPoint())
                            {
                                isComplete = true;
                            }
                        }

                    }

                }
            }
        }



        public class ChargeAndThrow : Orchestra.OrchestraAction
        {
            public bool isComplete { get; set; }
            public GameObject aiObject { get; set; }

            public ChargeAndThrow(GameObject aiIn)
            {
                aiObject = aiIn;

                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                ai.isOrchestrating = true;
            }

            public void Action()
            {
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                GameManager gameManager = GlobalConfiguration.Instance.gameManager;
                if (!isComplete)
                {
                    ai.EvaluateGameState();
                    if (ai.gameState == AI.GameState.dangerous)
                    {
                        ai.SetState(ai.panic_);
                        ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                    }

                    else
                    {
                        if (!ai.ballGrabbed) // assuming thrown
                        {
                            isComplete = true;
                        }

                        else
                        {
                            ai.SetState(ai.throwBall_);
                            ai.superTrigger = true;
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);

                        }

                    }

                }
            }
        }
        
    }


    void Start()
    {
        orchestraList = new List<Orchestra>();
        Orchestra mightyDuck = new MightyDuck();


        /*
         * 
         * 
         */

        orchestraList.Add(mightyDuck);

        currentOrchestra = mightyDuck;


    }

    // Update is called once per frame
    void Update()
    {
        if (set && GlobalConfiguration.Instance.gameManager.levelManager.isPlaying)
        {
            if (!isRunning)
            {
                if (orchestraCoolTime > 0 )
                {
                    orchestraCoolTime -= Time.deltaTime;
                }
                else
                {
                    int aiMaxLevelCount = aiList.Count * 3;
                    int runningAILevel = 0;
                    for (int i = 0; i < aiList.Count; i++)
                    {
                        runningAILevel += aiList[i].GetComponent<Player>().aiObject.GetComponent<AI>().level;
                    }

                    if (runningAILevel == aiMaxLevelCount) // max level for ais
                    {
                        isRunning = true;
                        print("Maxed level AI's");
                    }
                    else
                    {
                        isRunning = GetCanOrchestrate(runningAILevel, aiMaxLevelCount);
                    }
                }

                
            }
            if (isRunning)
            {
                RunOrchestra();
            }
           


        }
    }

    private void RunOrchestra()
    {

        if (!currentOrchestra.isInit)
        {
            currentOrchestra.Init(aiList);
        }
        else
        {
            currentOrchestra.Run();
        }
    }

    private bool GetCanOrchestrate(int runningAILevel, int aiMaxlevelCount)
    {
        float ran = UnityEngine.Random.Range(0f, 1f);

        if (ran <= (runningAILevel / aiMaxlevelCount))
        {
            return true;
        }

        else
        {
            orchestraCoolTime = orchestraCoolTimeMultiplier * (aiMaxlevelCount - runningAILevel);
            return false;
        }
    }

    public void SetAITeam(List<GameObject> aiList)
    {
        this.aiList = aiList;

        for (int i = 0; i < aiList.Count; i++)
        {
           aiList[i].GetComponent<Player>().aiObject.GetComponent<AI>().SetAIManager(this);
        }

        set = true;
    }

    public void AddAITeam(GameObject aiToAdd)
    {
        aiList.Add(aiToAdd);
    }

    internal void Clear()
    {
        aiList.Clear();
    }
}
