using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RevampMainMenu : MonoBehaviour
{
    public GameObject loadingObject;

    public GameObject buttonList;

    public GameObject eventSystemObject;

    private void Start()
    {
        GlobalConfiguration.loadPoint = GlobalConfiguration.LoadPoint.gameMode;

        if (GlobalConfiguration.Instance.gameMode == GlobalConfiguration.GameMode.test)
        {
            buttonList.transform.GetChild(0).gameObject.SetActive(true);
            buttonList.transform.GetChild(1).gameObject.SetActive(false);
            buttonList.transform.GetChild(2).gameObject.SetActive(false);

            EventSystem m_EventSystem = EventSystem.current;
            m_EventSystem.SetSelectedGameObject(buttonList.transform.GetChild(0).gameObject);
        }

        GlobalConfiguration.Instance.SetDefaultJoin(false);
    }


    public void PlayTest()
    {


        GlobalConfiguration.Instance.SetGameMode("test");
        LoadLevel("Test Groundz");
        eventSystemObject.SetActive(false);
        GlobalConfiguration.Instance.SetDefaultJoin(false);
        print("Test");
    }

    public void PlayLocal()
    {
        GlobalConfiguration.Instance.SetGameMode("arcade");
        SceneManager.LoadScene("QuickTeamSelect");
        print("Local");
    }

    public void PlayMultiplayer()

    {
        GlobalConfiguration.Instance.SetGameMode("multiplayer");
        SceneManager.LoadScene("RevampTeamSelect");
        print("Multiplayer");
    }

    public void PlayStory()
    {
        GlobalConfiguration.Instance.SetGameMode("story");
        SceneManager.LoadScene("StoryMode");
    }

    public void Menu()
    {
        SceneManager.LoadScene("GamemodeMenu");
    }


    public void PlayCredits()
    {
        SceneManager.LoadScene("CreditsRolling");
    }

    public void PlayControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchrously( sceneName));
    }

    IEnumerator LoadAsynchrously(string sceneName)
    {
        loadingObject.SetActive(true);
        Slider slider = loadingObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        Text progressText = loadingObject.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
    }

    void Update()
    {

    }
}
