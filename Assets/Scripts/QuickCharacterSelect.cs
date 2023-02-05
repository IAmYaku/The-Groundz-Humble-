using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem.UI;

public class QuickCharacterSelect : TeamSelect
{

    public TextMeshProUGUI characterSelectText;
    public TextMeshProUGUI pressStartText;

    bool gamepadStartPressed;

    public MultiplayerEventSystem multiplayerEventSystem;

    public GameObject mackContainerObject;

    public override void Start()
    {
        print("------- QuickCharacterSelect -------");
        base.Start();

        GlobalConfiguration.Instance.SetQuickCharacterSelect(this);
        GlobalConfiguration.Instance.SetIsAtQuickCharacterSelect(true);

    }

    public void QuickCharacterSelectInit ()
    {
       print("------- QuickCharacterSelect -------");
        base.Start();
    }
   

    public override void Update() 
    {
        Gamepad g = Gamepad.current;
        if (null !=g)        // gamepad
        {

            if (!gamepadStartPressed)
            {
                if (pressStartText.gameObject.activeSelf == false)
                {
                    pressStartText.gameObject.SetActive(true);
                   // characterSelectText.gameObject.SetActive(false);

                }
            }


            if(!ready && g.buttonWest.ReadValue() > 0)
            {
               // PickMack();
            }


            if (!ready && g.buttonEast.ReadValue() > 0)
            {
               // PickKing();
            }
        }
        else                        // keyboard
        {
            if (pressStartText.gameObject.activeSelf != false)
            {
                pressStartText.gameObject.SetActive(false);
               // characterSelectText.gameObject.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
              //  PickMack();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
              //  PickKing();
            }
        }

        base.Update();
    }


    public void PickMack()
    {
        if (!isLoading)
        {
        SetModule1CharacterType("Mack");

        print("PlayerReady Mack");
        PlayerReadyArcade(1,"Mack");
        }
    }

    public void PickKing()
    {
        if (!isLoading)
        {
            SetModule1CharacterType("King");

            print("PlayerReady King");
            PlayerReadyArcade(1, "King");
        }
    }

    public void LoadGameLevel(string sceneName)
    {
        if (!isLoading)
        {
            DestroyThemeMusic();
            StartCoroutine(LoadAsynchronously(sceneName));
            isLoading = true;
        }

    }

    private void DestroyThemeMusic()
    {

        GlobalConfiguration.Instance.TurnThemeMusic(false);

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

    internal void SetStartText(bool v)  // poor naming
    {

        if (!v)
        {
            gamepadStartPressed = true;
            pressStartText.gameObject.SetActive(false);
            characterSelectText.gameObject.SetActive(true);
        }
    }
    public void BackButton()
    {
        SceneManager.LoadScene("GamemodeMenu");
    }

    internal EventSystem GetEventSystem()
    {
        throw new NotImplementedException();
    }

    internal void SetFirstSelectedToMack()
    {
        multiplayerEventSystem.firstSelectedGameObject = mackContainerObject;
    }
}
