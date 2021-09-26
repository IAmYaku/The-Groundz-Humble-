using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevampMainMenu : MonoBehaviour
{

    public bool SettingsIsOpen = false;

    public GameObject settingsMenuUI;

    public GameObject image;

    private void Start()
    {

    }


    public void PlayLocal()
    {
        GlobalConfiguration.instance.SetGameMode("local");
        SceneManager.LoadScene("QuickTeamSelect");
        print("Local");
    }

    public void PlayMultiplayer()

    {
        GlobalConfiguration.instance.SetGameMode("multiplayer");
        SceneManager.LoadScene("TeamSelect");
        print("Multiplayer");
    }

    public void PlayStory()
    {
        GlobalConfiguration.instance.SetGameMode("story");
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
