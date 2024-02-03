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


    class Orchestra
    { 
        public interface OrchestraAction
        {
             bool isComplete { get; set; }

            GameObject aiObject{ get; set; }

           void Action();
        }

        Dictionary<OrchestraAction, GameObject> orchestraActions;

        public class RetrieveBallandReturnToSpawnPoint : OrchestraAction
        {
            public bool isComplete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public GameObject aiObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            RetrieveBallandReturnToSpawnPoint(GameObject aiIn)
            {
                aiObject = aiIn;
            }

            public void Action()
            {
                if (!isComplete) {
                  //  if (ai)     // Getting into sketchy territory when not cycling through states based off of intensity

                }
            }
        }




    }



    void Start()
    {
        Orchestra mightyDuck = new Orchestra();

    }

    // Update is called once per frame
    void Update()
    {
        if (set && GlobalConfiguration.Instance.gameManager.levelManager.isPlaying)
        {
            if (!isRunning && false)
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
        throw new NotImplementedException();
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
