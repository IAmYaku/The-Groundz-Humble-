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
    float orchestraCoolTimeMultiplier = 15f;

    public Orchestra currentOrchestra;

    List<Orchestra> orchestraList;

    public interface Orchestra 
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

        public void ResetOrchestra();

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

            int ballCount = GlobalConfiguration.Instance.gameManager.levelManager.balls.Count;

            foreach (List<Orchestra.OrchestraAction> orchestraStep in orchestraActionSteps)   // also diversity vs uniform in steps can be applied here
            {
                if (stepIndex == 0)
                {
                    foreach (GameObject aiObject in aiList)
                    {
                        if (ballCount > 0)
                        {
                            Orchestra.OrchestraAction orchestraAction = new RetrieveBallandReturnToSpawnPoint(aiObject);
                            orchestraStep.Add(orchestraAction);
                            ballCount--;
                        }
 
                    }        
                }

                 ballCount = GlobalConfiguration.Instance.gameManager.levelManager.balls.Count;

                if (stepIndex == 1)
                {
                    foreach (GameObject aiObject in aiList)
                    {
                        if (ballCount > 0)
                        {
                            Orchestra.OrchestraAction orchestraAction = new ChargeAndThrow(aiObject);
                        orchestraStep.Add(orchestraAction);
                            ballCount--;
                        }
                    }
                }

                stepIndex++;
            }


        }


        public void Run()
        {
          //  print("~ Running Mighty Duck...");
            if (stepIndex < orchestraActionSteps.Count)
            {
             //   print("~ stepIndex = " + stepIndex);
           //     print("~ orchestraActionSteps.Count = " + orchestraActionSteps.Count);
                isComplete = false;
                List<Orchestra.OrchestraAction> currentStep = orchestraActionSteps[stepIndex];

            //    print("~ currentStep = " + currentStep);


                int actionsCompleted = 0;
                foreach (Orchestra.OrchestraAction orchestraAction in currentStep)
                {
                    orchestraAction.Action();
                 //   print("~ orchestraAction = " + orchestraAction);

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
                    print("AI = " + ai.playerScript.number);
                    print("RetrieveBallandReturnToSpawnPoint Action");
                    ai.EvaluateGameState();
                    if (ai.gameState == AI.GameState.dangerous && !ai.panic_.panicked)
                    {
                        ai.SetState(ai.panic_);
                        ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                        print("RetrieveBallandReturnToSpawnPoint Panic");
                    }

                    else
                    {
                        ai.vertInput = 0; // normal if just panicked

                        if (!ai.ballGrabbed)
                        {
                            ai.SetState(ai.getBall_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                            print("RetrieveBallandReturnToSpawnPoint GetBall");
                        }

                        else
                        {
                            ai.SetState(ai.retreat_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                            print("RetrieveBallandReturnToSpawnPoint Retreat");

                            if (ai.IsAtRetreatPoint())
                            {
                                isComplete = true;
                                print("RetrieveBallandReturnToSpawnPoint Complete");
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
                        ai.vertInput = 0; // normal if just panicked

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

        public void ResetOrchestra()
        {
            isComplete = false;
            stepIndex = 0;

            foreach (List<Orchestra.OrchestraAction> orchestraStep in orchestraActionSteps)
            {

                foreach (Orchestra.OrchestraAction orchestraAction in orchestraStep)
                {
                    orchestraAction.isComplete = false;
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

                    if (runningAILevel >= aiMaxLevelCount) // max level for ais
                    {
                        isRunning = true;
                        print("Maxed level AI's");
                        print("~isOrchestrating...~");
                    }
                    else
                    {
                        isRunning = GetCanOrchestrate(runningAILevel, aiMaxLevelCount);
                        if (isRunning)
                        {
                            print("~isOrchestrating...~");
                        }

                    }
                }            
            }

            if (isRunning)
            {
               
                RunOrchestra();

                if (currentOrchestra.isComplete)
                {
                    currentOrchestra.ResetOrchestra();
                    isRunning = false;
                    // Get new orchestra from orchestraList
                }
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
        Reset();
    }

    internal void Clear()
    {
        aiList.Clear();
    }

    internal void RemoveAI(GameObject gameObject)
    {
        aiList.Remove(gameObject);
       
    }

    public void Reset()
    {
        isRunning = false;
        currentOrchestra.isInit = false;
        currentOrchestra.isComplete = false;
        //Clear();
    }
}
