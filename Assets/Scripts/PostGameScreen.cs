using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameScreen : MonoBehaviour
{
    public GameObject freePlayDefeatObj;
    public GameObject freePlayWinObj;
    public GameObject arcadeDefeatObj;
    public GameObject arcadeWinObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreePlayWin(string charName)
    {
        if (charName == Nina.name)
        {
            freePlayWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject ninaSprite = spriteContainer.transform.GetChild(2).gameObject;
            ninaSprite.SetActive(true);
            print("Nina won");

        }

        if (charName == King.name)
        {
            freePlayWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            GameObject kingSprite = spriteContainer.transform.GetChild(1).gameObject;
            kingSprite.SetActive(true);
            print("King won");

        }

        if (charName == Mack.name)
        {
            freePlayWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject mackSprite = spriteContainer.transform.GetChild(0).gameObject;
            mackSprite.SetActive(true);
            print("Mack won");

        }
    }

    public void FreePlayDefeat(string charName)
    {
        if (charName == Nina.name)
        {
            freePlayDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject ninaSprite = spriteContainer.transform.GetChild(2).gameObject;
            ninaSprite.SetActive(true);
            print("Nina lost");

        }

        if (charName == King.name)
        {
            freePlayDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            GameObject kingSprite = spriteContainer.transform.GetChild(1).gameObject;
            kingSprite.SetActive(true);
            print("King lost");

        }

        if (charName == Mack.name)
        {
            freePlayDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject mackSprite = spriteContainer.transform.GetChild(0).gameObject;
            mackSprite.SetActive(true);
            print("Mack lost");

        }
    }

    public void arcadeWin(string charName)
    {
       // print(charName + " wins");

        if (charName == Nina.name)
        {
            arcadeWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject ninaSprite = spriteContainer.transform.GetChild(2).gameObject;
            ninaSprite.SetActive(true);
            print("Nina won");

        }

        if (charName == King.name)
        {
            arcadeWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            GameObject kingSprite = spriteContainer.transform.GetChild(1).gameObject;
            kingSprite.SetActive(true);
           print("King won");

        }

        if (charName == Mack.name)
        {
            arcadeWinObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            print("spriteContainer name = " + spriteContainer.name);
            GameObject mackSprite = spriteContainer.transform.GetChild(0).gameObject;
            print("mackSprite name = " + mackSprite.name);
            mackSprite.SetActive(true);
            print("Mack won");

        }
    }

    public void arcadeDefeat(string charName)
    {
        if (charName == Nina.name)
        {
            arcadeDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject ninaSprite = spriteContainer.transform.GetChild(2).gameObject;
            ninaSprite.SetActive(true);
            print("Nina lost");

        }

        if (charName == King.name)
        {
            arcadeDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            GameObject kingSprite = spriteContainer.transform.GetChild(1).gameObject;
            kingSprite.SetActive(true);
            print("King lost");

        }

        if (charName == Mack.name)
        {
            arcadeDefeatObj.SetActive(true);
            GameObject spriteContainer = freePlayWinObj.transform.GetChild(3).gameObject;
            spriteContainer.SetActive(true);
            GameObject mackSprite = spriteContainer.transform.GetChild(0).gameObject;
            mackSprite.SetActive(true);
            print("Mack lost");

        }
    }

    internal void SelectRestartArcadeDefeatButton()
    {
        Button restartButton = arcadeDefeatObj.transform.GetChild(1).GetComponent<Button>();
        restartButton.Select();

    }
}
