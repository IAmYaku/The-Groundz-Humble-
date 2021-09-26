using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamSelectUIController : MonoBehaviour
{
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

    private void Start()
    {
        UpdateCharacterSelectionUI();
    }


    public void DownArrow()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterList.Count - 1;
       UpdateCharacterSelectionUI();
    }
    public void UpArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelectionUI();
    }

    public void Confirm()
    {
        Debug.Log(string.Format("Character {0}:{1} was selected", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
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


