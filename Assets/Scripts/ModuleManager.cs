using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleManager : MonoBehaviour
{
    public RevampTeamSelect revampTeamSelect;

   public List<GameObject> modules;
    public List<GameObject> starts;

    public GameObject multiplayerEventSystem1Gamepad;
    public GameObject multiplayerEventSystem1Keyboard;

    public GameObject multiplayerEventSystem2Gamepad;
    public GameObject multiplayerEventSystem3Gamepad;
    public GameObject multiplayerEventSystem4Gamepad;

    public GameObject p1Key;

    List<bool> modIsActives = new List<bool>();

    bool mod1Active;
    bool mod2Active;
    bool mod3Active;
    bool mod4Active;

    void Start()
    {
        modIsActives.Add(mod1Active);
        modIsActives.Add(mod2Active);
        modIsActives.Add(mod3Active);
        modIsActives.Add(mod4Active);

    }

    // Update is called once per frame
    void Update()
    {
        CheckStarts();
    }

    private void CheckStarts()
    {
        int gamepadCount = Gamepad.all.Count;

        for ( int i =0; i< gamepadCount; i++)
        {
            if (modIsActives[i] == false)
            {
                starts[i].SetActive(true);
            }
        }
    }

    public void EnableModule(int i)
    {
        if (i == 0)
        {
            p1Key.SetActive(false);
        }

        if (i < 4)
        {
            modules[i].SetActive(true);
        }
        
        if (starts[i].activeSelf == true)
        {
            starts[i].SetActive(false);
            modIsActives[i] = true;
        }
            
    }

  public void UpdatePlayerCharcter (int playerNumber, int charIndex)
    {
        if (playerNumber == 1)
        {
            Updatep1Charcter(charIndex);
        }

        if (playerNumber == 2)
        {
            Updatep2Charcter(charIndex);
        }

        if (playerNumber == 3)
        {
            Updatep3Charcter(charIndex);
        }

        if (playerNumber == 4)
        {
            Updatep4Charcter(charIndex);
        }

    }
    public void Updatep1Charcter(int charIndex)
    {
        
        if (charIndex == 0)
        {
            revampTeamSelect.SetModule1CharacterType(Mack.name);
        }
        if (charIndex == 1)
        {
            revampTeamSelect.SetModule1CharacterType(King.name);
        }
        if (charIndex == 2)
        {
            revampTeamSelect.SetModule1CharacterType(Nina.name);
        }

    }
    public void Updatep2Charcter(int charIndex)
    {
        if (charIndex == 0)
        {
            revampTeamSelect.SetModule2CharacterType(Mack.name);
        }
        if (charIndex == 1)
        {
            revampTeamSelect.SetModule2CharacterType(King.name);
        }
        if (charIndex == 2)
        {
            revampTeamSelect.SetModule2CharacterType(Nina.name);
        }

    }

    public void Updatep3Charcter(int charIndex)
    {
        if (charIndex == 0)
        {
            revampTeamSelect.SetModule3CharacterType(Mack.name);
        }
        if (charIndex == 1)
        {
            revampTeamSelect.SetModule3CharacterType(King.name);
        }
        if (charIndex == 2)
        {
            revampTeamSelect.SetModule3CharacterType(Nina.name);
        }
    
    }

    public void Updatep4Charcter(int charIndex)
    {
        if (charIndex == 0)
        {
            revampTeamSelect.SetModule4CharacterType(Mack.name);
        }
        if (charIndex == 1)
        {
            revampTeamSelect.SetModule4CharacterType(King.name);
        }
        if (charIndex == 2)
        {
            revampTeamSelect.SetModule4CharacterType(Nina.name);
        }
    }

}
