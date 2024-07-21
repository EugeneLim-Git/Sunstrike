using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMenuStorage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI partyMemberNames;
    [SerializeField] private List<BaseCharacter> selectedCharacterList;
    private List<BaseSkill> characterSkillListOne, characterSkillListTwo, characterSkillListThree, characterSkillListFour;
    private List<List<BaseSkill>> characterSkillList;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //selectedCharacterList = new();
        for (int i = 0; i < 4; i++)
        {
            selectedCharacterList.Add(null);
        }
        partyMemberNames.text = ("1. Select a Character" + "\n" +
            "2. Select a Character" + "\n" +
            "3. Select a Character" + "\n" +
            "4. Select a Character" + "\n");

    }

    public void OnCharacterSelected()
    {
        // change character name, description, available skills
        // make character slot be filled with this character on click
        // have button that removes most recent entry into character comp list
    }

}
