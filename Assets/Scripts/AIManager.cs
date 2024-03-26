using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    LevelManager levelManager;

    public TeamManager teamManager;

    List<GameObject> aiList = new List<GameObject>();

    public bool isRunning;

    bool isOrchestrating;
    float orchestraCoolTime = 0f;
    float orchestraCoolTimeMultiplier = 15f;

    public Orchestra currentOrchestra;

    List<Orchestra> orchestraList;

    int orchestraIndex;

    public interface Orchestra 
    {

        bool isInit { get; set; }
        bool isComplete { get; set; }
        int stepIndex { get; set; }

        public void Init(List<GameObject> aiList);

        public void Run();

        public interface OrchestraAction
        {
            string name { get; set; }
            bool isComplete { get; set; }

            GameObject aiObject { get; set; }

            public class Command {

                enum CommandType { move, dodge, grab, block, release, super };

                Vector2 move = new Vector2();

                CommandType command;

                public void Action (GameObject aiObject)
                {
                    AI aiComp = aiObject.GetComponent<AI>();

                    if (command == CommandType.move )
                    {
                        aiComp.horzInput = move.x;
                        aiComp.horzInput = move.y;
                    }

                    if (command == CommandType.dodge)
                    {
                        aiComp.jumpInput = true;

                    }
                    if (command == CommandType.grab)
                    {
                        aiComp.action1Input = true;
                    }

                    if (command == CommandType.block)
                    {
                        aiComp.BlockInput(2f);
                    }

                    if (command == CommandType.release)
                    {
                        aiComp.action1Input = true;
                    }
                    if (command == CommandType.super)
                    {
                        aiComp.superTrigger = true;
                        aiComp.SuperInput(.5f);
                    }
                }

            }

            List<Command> commandList { get; set; }

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

        public int type;
        float timeDelay;
        private bool hasDelayed;

        public MightyDuck(int type)
        {
            this.type = type;
        }

        public void Init(List<GameObject> aiList)     // what if theres a mismatch in player count and actions?
        {

            print("Migty Duck Init");
            isInit = true;
             timeDelay = 0;
             hasDelayed = false;


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
                            Orchestra.OrchestraAction orchestraAction = new ChargeAndThrow(aiObject, type);
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
                isComplete = false;
                List<Orchestra.OrchestraAction> currentStep = orchestraActionSteps[stepIndex];

                if (type == 0 || type == 1)
                {
                    if (stepIndex == 0)
                    {
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
                                currentStep = orchestraActionSteps[stepIndex];
                            }
                        }
                    }

                    if (stepIndex == 1)
                    {
                        if (type == 0)
                        {
                            int actionsCompleted = GetActionsCompleted(currentStep);
                            //  print("actionsCompleted = " + actionsCompleted);

                            if (actionsCompleted == currentStep.Count)
                            {
                                stepIndex++;
                            }
                            else
                            {
                                Orchestra.OrchestraAction orchestraAction = currentStep[actionsCompleted];

                                orchestraAction.Action();

                               //   print("orchestraAction = " + orchestraAction.name);
                            }
                        }

                        if (type == 1)
                        {
                            print("type = " + type);

                            Orchestra.OrchestraAction orchestraAction = currentStep[0]; //init

                            if (!hasDelayed)
                            {
                                int actionIndex = Mathf.Clamp((int)(UpdateTimeDelay() / 3.0f), 0, currentStep.Count - 1);
                                orchestraAction = currentStep[actionIndex];
                                if (timeDelay >= 3.0f * currentStep.Count)
                                {
                                    hasDelayed = true;
                                    timeDelay = 0f;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < currentStep.Count; i++)
                                {
                                    orchestraAction = currentStep[i];
                                    orchestraAction.Action();

                                }
                            }



                            orchestraAction.Action();

                            int actionsCompleted = GetActionsCompleted(currentStep);

                            if (actionsCompleted == currentStep.Count)
                            {
                                stepIndex++;
                            }

                        }
                    }

                }



                    if (type == 2)
                    {
                    print("type = " + type);

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

                }

                else
                {
                    isComplete = true;
                    print("~AI Orchestra completed~!");

                }
            }
        

        public int GetActionsCompleted(List<Orchestra.OrchestraAction> currentStep)
        {
            int actionsCompleted = 0;

            foreach (Orchestra.OrchestraAction action in currentStep)
            {
                if (action.isComplete)
                {
                    actionsCompleted++;
                }
            }


            return actionsCompleted;
        }

        public float UpdateTimeDelay()
        {
            timeDelay += Time.deltaTime;

            return timeDelay;
        }


        public class RetrieveBallandReturnToSpawnPoint : Orchestra.OrchestraAction
        {
            public string name { get; set; }
            public bool isComplete { get ; set ; }
            public GameObject aiObject { get ; set ; }
            List<Orchestra.OrchestraAction.Command> Orchestra.OrchestraAction.commandList { get ; set ; }

            public RetrieveBallandReturnToSpawnPoint(GameObject aiIn)
            {
                aiObject = aiIn;
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                ai.isOrchestrating = true;


                isComplete = false;
                name = "RetrieveBallandReturnToSpawnPoint";
            }

            public void Action()
            {
              
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                GameManager gameManager = GlobalConfiguration.Instance.gameManager;
                if (!isComplete)
                {
                 //   print("AI = " + ai.playerScript.number);
                 //   print("RetrieveBallandReturnToSpawnPoint Action");
                    ai.EvaluateGameState();
                    if (ai.gameState == AI.GameState.dangerous && !ai.panic_.panicked)
                    {
                        ai.SetState(ai.panic_);
                        ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                    //    print("RetrieveBallandReturnToSpawnPoint Panic");
                    }

                    else
                    {
                        ai.vertInput = 0; // normal if just panicked

                        if (!ai.ballGrabbed)
                        {
                            ai.SetState(ai.getBall_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                      //      print("RetrieveBallandReturnToSpawnPoint GetBall");
                        }

                        else
                        {
                            ai.SetState(ai.retreat_);
                            ai.aiState.Action(gameManager, ai, ai.intensity, Vector3.zero);
                      //     print("RetrieveBallandReturnToSpawnPoint Retreat");

                            if (ai.IsAtRetreatPoint())
                            {
                                isComplete = true;
                         //       print("RetrieveBallandReturnToSpawnPoint Complete");
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

            int type;

            public string name { get; set; }

            List<Orchestra.OrchestraAction.Command> Orchestra.OrchestraAction.commandList { get; set; }

            public ChargeAndThrow(GameObject aiIn, int type)
            {
                aiObject = aiIn;
                
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                ai.isOrchestrating = true;

                this.type = type;
                name = "ChargeAndThrow";
                isComplete = false;
            }

            public void Action()
            {
                AI ai = aiObject.GetComponent<Player>().aiObject.GetComponent<AI>();
                GameManager gameManager = GlobalConfiguration.Instance.gameManager;
                if (!isComplete)
                {
                  //  print("AI = " + ai.playerScript.number);
                 //   print("ChargeAndThrow Action");
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

                            if ( type == 2)
                            {
                                ai.superTrigger = true;
                            }

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
        levelManager = GlobalConfiguration.Instance.gameManager.levelManager;

        orchestraList = new List<Orchestra>();
        Orchestra mightyDuck0 = new MightyDuck(0);
        Orchestra mightyDuck1 = new MightyDuck(1);
        Orchestra mightyDuck2 = new MightyDuck(2);


        /*
         * 0
         * 0
         */

        orchestraList.Add(mightyDuck0);
        orchestraList.Add(mightyDuck1);
        orchestraList.Add(mightyDuck2);

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isPlaying)
        {

            string gameMode = levelManager.GetGameMode();

            if (gameMode == "multiplayer")
            {
                MultiplayerUpdate();
            }

            if (gameMode == "arcade")
            {
                ArcadeUpdate();
            }
        }

            
    }

    private void RunOrchestra()
    {
        if (!currentOrchestra.isInit)
        {
            aiList = teamManager.GetAIList(true);
            currentOrchestra.Init(aiList);
        }
        else
        {
            currentOrchestra.Run();
        }
    }

    private void MultiplayerUpdate()
    {
        if (!isRunning)
        {
            if (orchestraCoolTime > 0)
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

            if (currentOrchestra == null)
            {
                currentOrchestra = orchestraList[0]; // random
            }

            RunOrchestra();

            if (currentOrchestra.isComplete)
            {
                currentOrchestra.ResetOrchestra();
                isRunning = false;
                orchestraIndex = (int)UnityEngine.Random.Range(0f, orchestraList.Count - .01f);

                currentOrchestra = orchestraList[orchestraIndex];
                
            }
        }
    }

    private void ArcadeUpdate()
    {
        if (!isRunning)
        {
            if (orchestraCoolTime > 0)
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
            if (currentOrchestra == null)
            {
                currentOrchestra = orchestraList[orchestraIndex]; // Get arcadeLevel
            }

            RunOrchestra();

            if (currentOrchestra.isComplete)
            {
                currentOrchestra.ResetOrchestra();
                isRunning = false;
                orchestraIndex++;
                currentOrchestra = orchestraList[orchestraIndex];
                
            }
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

    }

    public void AddAITeam(GameObject aiToAdd)
    {
        aiList.Add(aiToAdd);
        ResetManager();
    }

    internal void Clear()
    {
        aiList.Clear();
    }

    internal void RemoveAI(GameObject gameObject)
    {
        aiList.Remove(gameObject);
       
    }

    public void ResetManager()
    {
       
        isRunning = false;
        Clear();

        if (levelManager.isPlaying)
        {
            aiList = teamManager.GetAIList(true);
        }
        else
        {
            aiList = teamManager.GetAIList(false);
        }

      
        if (currentOrchestra != null)
        {
            currentOrchestra.isInit = false;
            currentOrchestra.ResetOrchestra();
        }
        
    }


}
