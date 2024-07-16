using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PartyComposition", order = 2)]
public class PartyCompSO : ScriptableObject
{
    public List<GameObject> playerCharacters; // for reference in game
    public List<BaseSkill> characterOneSkills, characterTwoSkills, characterThreeSkills, characterFourSkills; // for reference in UI

    private void Start()
    {

    }

    private void LoadCharacters()
    {
        characterOneSkills = playerCharacters[0].GetComponent<BattleEntity>().skillList;
        characterTwoSkills = playerCharacters[1].GetComponent<BattleEntity>().skillList;
        characterThreeSkills = playerCharacters[2].GetComponent<BattleEntity>().skillList;
        characterFourSkills = playerCharacters[3].GetComponent<BattleEntity>().skillList;
    }

    private void SetCharacter(BattleEntity characterToSave, int slot)
    {
        GameObject savedGuy = characterToSave.GetComponent<GameObject>();
        playerCharacters[slot - 1] = savedGuy;
    }
}
