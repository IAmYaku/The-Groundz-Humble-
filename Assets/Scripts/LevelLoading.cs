using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoading : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
   
    public void LoadGameLevel(string sceneName)
    {
        DestroyThemeMusic();
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.instance.TurnThemeMusic(false);

    }

    IEnumerator LoadAsynchronously (string sceneName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = (int)( progress * 100f) + "%";

            yield return null;
        }

    }

}
