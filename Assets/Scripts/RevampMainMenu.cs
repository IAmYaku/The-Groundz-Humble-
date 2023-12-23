using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RevampMainMenu : MonoBehaviour
{
    public GameObject image;

    public GameObject buttonList;

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
    }


    public void PlayTest()
    {


        GlobalConfiguration.Instance.SetGameMode("test");
        SceneManager.LoadScene("Test Groundz");
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

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchrously(sceneIndex));
    }

    IEnumerator LoadAsynchrously(int sceneIndex)
    {
        image.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }

    void Update()
    {

    }
}
