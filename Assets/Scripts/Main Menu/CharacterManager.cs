using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    [SerializeField] public List<CharacterLoaderSO> selectedCharacterList;
    private List<BaseSkill> characterSkillListOne, characterSkillListTwo, characterSkillListThree, characterSkillListFour;
    public NewRunManager runManager;
    public List<SelectSkillIcon> selectedSkillsButtons;
    public List<PartySelectButton> selectedPartyButtons;
    public int currentPartySlot;

    public void Initialise()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        for (int i = 0; i < 4; i++)
        {
            selectedCharacterList.Add(null);
        }

        currentPartySlot = 0;

        selectedCharacterList = new List<CharacterLoaderSO>();

        runManager = FindObjectOfType<NewRunManager>();
    }

    public void RenewButtons()
    {
        selectedSkillsButtons = runManager.skillButtons;
        selectedPartyButtons = runManager.PartyButtons;
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
                    //selectedCharacterList.Remove(character);
                }
            }

            if (duplicateCharacter == false)
            {
                selectedCharacterList.Remove(selectedCharacterList[0]);
                selectedCharacterList.Add(charToLoad);
            }
            else
            {
                selectedCharacterList.Remove(charToLoad);
            }

            
        }
        else if (selectedCharacterList.Count != 0) // if there are other members, check if the character already exists, if yes, remove
        {

            CharacterLoaderSO characterToRemove = null;
            foreach (var character in selectedCharacterList)
            {
                if (character == charToLoad)
                {
                    characterToRemove = character;
                }
            }

            if (characterToRemove != null && selectedCharacterList.Count == 1)
            {
                selectedCharacterList.Remove(characterToRemove);
                foreach (var button in selectedSkillsButtons)
                {
                    button.transform.parent.gameObject.SetActive(false);
                }

                DescriptionText descText = FindObjectOfType<DescriptionText>();
                descText.ChangeText("");
                
            }
            else if (charToLoad == characterToRemove)
            {
                selectedCharacterList.Remove(characterToRemove);
                currentPartySlot = 0;
                SetSelectedCharacter(0);

            }
            else if (characterToRemove == null) // if character was not found in list already
            {
                selectedCharacterList.Add(charToLoad);
            }

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
        
        RefreshPartyNameList();

    }

    public void SetSelectedCharacter(int positionToChangeTo)
    {
        currentPartySlot = positionToChangeTo;
        selectedPartyButtons[positionToChangeTo].buttonText.text = selectedCharacterList[positionToChangeTo].GetCharacterBase().GetCharacterName();
        UpdateSelectedSkillUI();
        Debug.Log("Selected Character: " + selectedCharacterList[currentPartySlot].name);
    }

    public void RefreshPartyNameList()
    {
        if (selectedCharacterList.Count > 0)
        {
            int i = 0;

            while (i < selectedCharacterList.Count)
            {
                selectedPartyButtons[i].buttonText.text = selectedCharacterList[i].GetCharacterBase().GetCharacterName();
                i++;
            }

            if (selectedCharacterList.Count < 4)
            {
                Debug.Log("Character listless than 4!");
                while (i < 4)
                {
                    selectedPartyButtons[i].buttonText.text = "Select Character";
                    i++;

                }
            }
        }
        else
        {
            foreach (var button in selectedPartyButtons)
            {
                button.buttonText.text = "Select Character";
            }
        }
       runManager.RefreshCharacterButtons(selectedCharacterList);
    }

    public void OnSkillSelected(BaseSkill skillToAdd, SelectSkillIcon button)
    {
        int i = 0;
        bool skillAlreadyThere = false;
        BaseSkill skillToRemove = null;
        Debug.Log(currentPartySlot);
        foreach (BaseSkill skill in selectedCharacterList[currentPartySlot].selectedSkillList)
        {
            if (skill == skillToAdd) // checks if skill is already equipped. If yes, remove it. Allows user to click on button again to unequip the skill from character
            {
                skillToRemove = skill;
                skillAlreadyThere = true;
            }
            else
            {
                i++;
            }
        }

        if (skillAlreadyThere == false)
        {
            button.background.enabled = true;
            AddSkillToSelectedCharacter(skillToAdd);
            UpdateSelectedSkillUI();
        }
        else if (skillAlreadyThere == true)
        {
            button.background.enabled = false;
            selectedCharacterList[currentPartySlot].selectedSkillList.Remove(skillToRemove);
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
            i++;

            if (button.transform.parent.gameObject.active == false)
            {
                button.transform.parent.gameObject.SetActive(true);
            }
        }

        i = 0;

        foreach (BaseSkill skill in selectedCharacterList[currentPartySlot].selectableSkillList)
        {
            selectedSkillsButtons[i].GetComponent<Button>().image.sprite = skill.GetSkillUIImage();
            selectedSkillsButtons[i].background.enabled = false;
            selectedSkillsButtons[i].ChangeStoredSkill(skill);
            i++;
        }

        

        foreach (BaseSkill selectedSkill in selectedCharacterList[currentPartySlot].selectedSkillList)
        {
            Debug.Log(selectedSkill);
            foreach(SelectSkillIcon button in selectedSkillsButtons)
            {
                if (selectedSkill == button.storedSkill)
                {
                    button.background.enabled = true;
                }
            }
        }

        DescriptionText descText = FindObjectOfType<DescriptionText>();

        descText.ChangeText(selectedCharacterList[currentPartySlot].GetCharacterBase().GetCharacterDesc());
    }

    public List<GameObject> ReturnCharacterPrefabs()
    {
        List<GameObject> listToReturn = new List<GameObject>();
        int i = 0;
        foreach (var character in selectedCharacterList)
        {
            listToReturn.Add(character.GetCharacterPrefab());

        }

        return listToReturn;
    }


}
