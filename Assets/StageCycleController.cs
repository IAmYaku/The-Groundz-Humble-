using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCycleController : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    private int selectedStageIndex;
    


    // Menu Controller

    [Header("List of Stages")]
    [SerializeField] public List<StageSelectObject> stageList = new List<StageSelectObject>();


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI stageTitle;
    [SerializeField] private Image stageImage;
   

    private void Start()
    {
        UpdateStageSelectionUI();
    }


    public void LeftArrow()
    {
        selectedStageIndex--;
        if (selectedStageIndex < 0)
            selectedStageIndex = stageList.Count - 1;

        UpdateStageSelectionUI();
    }

    public void RightArrow()
    {
        selectedStageIndex++;
        if (selectedStageIndex == stageList.Count)
            selectedStageIndex = 0;


        UpdateStageSelectionUI();
    }

    

    public void Confirm()
    {

        if(stageList[selectedStageIndex].StageName == "THEGROUNDZEAST")
        {
            LoadGameLevel("TheGroundzEast");
            Debug.Log(string.Format("Stage {0}:{1} was selected", selectedStageIndex, stageList[selectedStageIndex].StageName));
        }

        if (stageList[selectedStageIndex].StageName == "THELIBRARY")
        {
            LoadGameLevel("TheLibrary");
            Debug.Log(string.Format("Stage {0}:{1} was selected", selectedStageIndex, stageList[selectedStageIndex].StageName));
        }

        if (stageList[selectedStageIndex].StageName == "THEBACKYARD")
        {
            LoadGameLevel("TheBackyard");
            Debug.Log(string.Format("Stage {0}:{1} was selected", selectedStageIndex, stageList[selectedStageIndex].StageName));
        }
    }

    private void UpdateStageSelectionUI()
    {
        stageImage.sprite = stageList[selectedStageIndex].render;
        stageTitle.text = stageList[selectedStageIndex].StageName;
       
    }

    [System.Serializable]
    public class StageSelectObject
    {
        public Sprite render;
        public string StageName;
        
    }

   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Level Loading


    public void LoadGameLevel(string sceneName)
    {
        DestroyThemeMusic();
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.instance.TurnThemeMusic(false);

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

}
