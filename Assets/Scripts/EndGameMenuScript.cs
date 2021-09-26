using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuScript : MonoBehaviour {

    public bool GameIsOver = false;
    public GameObject endGameMenuUI;

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
        endGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
    }

   public void Pause()
    {
        endGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
        print("EndGame Pause");

    }

    public void LoadMenu()
    {
        // GameManager.playerTypes.Clear();
        levelManager.EndGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamemodeMenu");
    }

    public void RestartGame()
    {
       
        GameIsOver = false;
        levelManager.GameReset();
        endGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelManager.SetStart(true);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadControls()
    {
        //Time.timeScale = 1f;
       // SceneManager.LoadScene("Controls"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void ToggleModes()
    {
        /*
        TextMesh modeText = transform.GetChild(1).GetChild(5).GetChild(0).gameObject.GetComponent<TextMesh>();
        modeText.text= gameManager.ToggleMode() + " Mode";
        */
    }

    public void  KeyboardView()
    {
        /*
       TextMesh modeText = transform.GetChild(1).GetChild(4).GetChild(0).gameObject.GetComponent<TextMesh>();
        modeText.text = (gameManager.ToggleViewNMode() + " View");
        */
    }
}
