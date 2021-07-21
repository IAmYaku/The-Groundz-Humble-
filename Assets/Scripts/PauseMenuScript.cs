using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;

public class PauseMenuScript : MonoBehaviour {

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public LevelManager levelManager;
    public GameObject[] players;
    // Update is called once per frame

    private void Start()
    {

        if (levelManager)
        {
            
            players = levelManager.GetPlayers().ToArray();
        }
        else
        {
            levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
            players = levelManager.GetPlayers().ToArray();
        }
    }
        void Update ()
    {

		if(Input.GetKeyDown(KeyCode.Escape) ||IsPauseInput())
        {
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

    private bool IsPauseInput()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player>().hasJoystick)
            {
                if (Input.GetKeyDown(player.GetComponent<Player>().joystick.pauseInput))
                {
                    return true;
                }
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

    public void LoadMenu()
    {
      //  GameManager.playerTypes.Clear();
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
