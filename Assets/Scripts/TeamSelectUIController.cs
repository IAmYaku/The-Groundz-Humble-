using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class TeamSelectUIController : MonoBehaviour
{
    public int controllerNumber;

    private int selectedCharacterIndex;
    private Color desiredColor;
    public GameObject CharPanel;
    public GameObject promtText;

    [Header("List of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterRender;
    [SerializeField] private Image backgroundColor;


    public ModuleManager moduleManager;

    private void Start()
    {
        UpdateCharacterSelectionUI();
    }


    public void DownArrow()
    {
        selectedCharacterIndex--;

        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterList.Count - 1;
        }

            UpdateCharacterSelectionUI();
            moduleManager.UpdatePlayerCharcter(controllerNumber, selectedCharacterIndex);


    }
    public void UpArrow()
    {
        selectedCharacterIndex++;

        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }


        UpdateCharacterSelectionUI();
        moduleManager.UpdatePlayerCharcter(controllerNumber, selectedCharacterIndex);
    }

    public void Confirm()
    {
        Debug.Log(string.Format("Character {0}:{1} was selected", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
       if (controllerNumber == 1)
        {
            moduleManager.Updatep1Charcter(selectedCharacterIndex);
        }
        if (controllerNumber == 2)
        {
            moduleManager.Updatep2Charcter(selectedCharacterIndex);
        }
        if (controllerNumber == 3)
        {
            moduleManager.Updatep3Charcter(selectedCharacterIndex);
        }
        if (controllerNumber == 4)
        {
            moduleManager.Updatep4Charcter(selectedCharacterIndex);
        }


        if (RevampTeamSelect.starts > 0 )
        {
            RevampTeamSelect revampTeamSelect = GlobalConfiguration.Instance.GetRevampTeamSelect();
            revampTeamSelect.multiplayerEventSystemMainGamepad.SetActive(true);
            GlobalConfiguration.Instance.SetInputModule(controllerNumber - 1, revampTeamSelect.multiplayerEventSystemMainGamepad.GetComponent<InputSystemUIInputModule>());
        }
        

    }

    //Dropdowns
    public void OnPlayerTypeSelected1(int num)
    {
        print("OnPlayerTypeSelected " + num);
    }

    public void OnTeamTypeSelected(int num)
    {
        print("OnTeamTypeSelected " + num);
    }

    public void OnGamemodeTypeSelected(int num)
    {
        print("OnGamemodeTypeSelected" + num);
    }

    private void UpdateCharacterSelectionUI()
    {
       characterRender.sprite = characterList[selectedCharacterIndex].render;
       characterName.text = characterList[selectedCharacterIndex].characterName;
        backgroundColor.color = characterList[selectedCharacterIndex].characterColor;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite render;
        public string characterName;
        public Color characterColor;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Panel Update

    public void CharPanelUpdate()
    {
        CharPanel.SetActive(true);

        if(CharPanel == enabled)
        {
            promtText.SetActive(false);
        }
    }
}


