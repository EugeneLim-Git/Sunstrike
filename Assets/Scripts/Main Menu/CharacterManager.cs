using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    [SerializeField] private TextMeshProUGUI partyMemberNames;
    [SerializeField] private List<CharacterLoaderSO> selectedCharacterList;
    private List<BaseSkill> characterSkillListOne, characterSkillListTwo, characterSkillListThree, characterSkillListFour;
    public List<SelectSkillIcon> selectedSkillsButtons;
    private int currentPartySlot;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        for (int i = 0; i < 4; i++)
        {
            selectedCharacterList.Add(null);
        }
        partyMemberNames.text = ("1. Select a Character" + "\n" +
            "2. Select a Character" + "\n" +
            "3. Select a Character" + "\n" +
            "4. Select a Character" + "\n");

        currentPartySlot = 0;

        selectedCharacterList = new List<CharacterLoaderSO>();

        characterSkillListOne = new List<BaseSkill> ();
        characterSkillListTwo = new List<BaseSkill>();
        characterSkillListThree = new List<BaseSkill>();
        characterSkillListFour = new List<BaseSkill>();

        foreach (var icon in selectedSkillsButtons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    public void OnCharacterSelected(CharacterLoaderSO charToLoad)
    {
        if (selectedCharacterList.Count == 4)
        {
            bool duplicateCharacter = false;
            foreach (var character in selectedCharacterList)
            {
                if (character == charToLoad)
                {
                    duplicateCharacter = true;
                    selectedCharacterList.Remove(character);
                }
            }

            if (duplicateCharacter == false)
            {
                selectedCharacterList.Remove(selectedCharacterList[0]);
                selectedCharacterList.Add(charToLoad);
            }

            
        }
        else if (selectedCharacterList.Count != 0) // if there are other members, check if the character already exists, if yes, remove
        {
            foreach (var character in selectedCharacterList)
            {
                if (character == charToLoad)
                {
                    selectedCharacterList.Remove(character);
                }
            }
            selectedCharacterList.Add(charToLoad);
        }
        else // if count is 0, simply add character
        {
            selectedCharacterList.Add(charToLoad);
        }

        int position = 0;
        foreach (var character in selectedCharacterList)
        {
            if (character == charToLoad)
            {
                SetSelectedCharacter(position);
                break;
            }
            position++;
        }
        
    }

    public void SetSelectedCharacter(int positionToChangeTo)
    {
        currentPartySlot = positionToChangeTo;
        Debug.Log("Selected Character: " + selectedCharacterList[currentPartySlot].name);
    }

    public void OnSkillSelected(BaseSkill skillToAdd)
    {
        int i = 0;
        bool skillAlreadyThere = false;
        foreach(BaseSkill skill in selectedCharacterList[currentPartySlot].selectedSkillList)
        {
            if (skill == skillToAdd) // checks if skill is already equipped. If yes, remove it. Allows user to click on button again to unequip the skill from character
            {
                selectedCharacterList[currentPartySlot].selectedSkillList.Remove(skill);
                skillAlreadyThere = true;
            }
            else
            {
                i++;
            }
        }

        if (skillAlreadyThere == false)
        {
            AddSkillToSelectedCharacter(skillToAdd);
        }
    }

    public void AddSkillToSelectedCharacter(BaseSkill skillToAdd)
    {
        selectedCharacterList[currentPartySlot].AddSkillToBattleList(skillToAdd);

    }

    public void UpdateSelectedSkillUI()
    {
        int i = 0;

        foreach (var button in selectedSkillsButtons)
        {
            selectedSkillsButtons[i].GetComponent<Button>().image.sprite = null;
        }

        foreach (BaseSkill skill in selectedCharacterList[currentPartySlot].selectableSkillList)
        {
            selectedSkillsButtons[i].GetComponent<Button>().image.sprite = skill.GetSkillUIImage();
        }
    }

    public void SetCharacterSkillSets()
    {
        characterSkillListOne = selectedCharacterList[0].selectedSkillList;
        characterSkillListTwo = selectedCharacterList[1].selectedSkillList;
        characterSkillListThree = selectedCharacterList[2].selectedSkillList;
        characterSkillListFour = selectedCharacterList[3].selectedSkillList;
    }

}
