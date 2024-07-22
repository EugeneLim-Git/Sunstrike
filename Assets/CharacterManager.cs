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
    private List<List<BaseSkill>> characterSkillList;
    public List<Button> selectedSkillsButtons;
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
        characterSkillList.Add(characterSkillListOne);
        characterSkillList.Add(characterSkillListTwo);
        characterSkillList.Add(characterSkillListThree);
        characterSkillList.Add(characterSkillListFour);
    }

    public void OnCharacterSelected(CharacterLoaderSO charToLoad)
    {
        selectedCharacterList[currentPartySlot] = charToLoad;
    }

    public void AddSkillToCharList(BaseSkill skillToAdd)
    {
        if (chWaracterSkillList[currentPartySlot].Count < 4)
        {
            characterSkillList[currentPartySlot].Add(skillToAdd);
        }
        else
        {
            characterSkillList[currentPartySlot].Remove(characterSkillList[currentPartySlot][0]);
            characterSkillList[currentPartySlot].Add(skillToAdd);
        }
    }

    public void UpdateSelectedSkillUI()
    {
        int i = 0;
        foreach (BaseSkill skill in characterSkillList[currentPartySlot])
        {
            selectedSkillsButtons[i].image.sprite = skill.GetSkillUIImage();
        }
    }

}
