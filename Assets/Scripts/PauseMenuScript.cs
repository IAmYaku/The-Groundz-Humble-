using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
//using TMPro;

public class PauseMenuScript : MonoBehaviour {

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public LevelManager levelManager;
    public GameObject controlScreen;
    public GameObject[] players;
    bool pauseStateChanged = false;
    // Update is called once per frame

    private void Start()
    {

        if (levelManager)
        {
            players = levelManager.GetPlayers().ToArray();
        }
        else
        {
            GameObject gameManager = GlobalConfiguration.Instance.gameManager.gameObject;
            levelManager = gameManager.GetComponent<LevelManager>();
            players = levelManager.GetPlayers().ToArray();
        }
    }
        void Update ()
    {

		if(IsPauseInput())
        {
            if(!pauseStateChanged)
            {
                pauseStateChanged = true;
                if(GameIsPaused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }
        }
        else
        {
            pauseStateChanged = false;
        }
        
	}

    private bool IsPauseInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }

        Gamepad g = Gamepad.current;
        if(null != g)
        {
            float pauseValue = g.startButton.ReadValue();
            if (pauseValue > 0)
            {
                return true;

            }

           
        }

        return false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void QuitToMain()
    {
        levelManager.EndGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamemodeMenu");
    }

    public void RestartGame()
    {
        levelManager.GameReset();
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void LoadControls()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Controls"); 
    }


    public void RevealHelpText()
    {
       
    }

   
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }



    public void  ToggleViewNMode()          // obsolete
    {
        Text modeText = transform.GetChild(1).GetChild(4).GetChild(0).gameObject.GetComponent<Text>();
      //  modeText.text = ("Mode x View: " + gameManager.ToggleViewNMode());
    }
}
