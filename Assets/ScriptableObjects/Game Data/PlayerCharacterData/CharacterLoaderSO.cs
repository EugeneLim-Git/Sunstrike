using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterLoadData", order = 2)]
public class CharacterLoaderSO : ScriptableObject
{
    [SerializeField] private GameObject CharacterPrefab;
    [SerializeField] private BaseCharacter characterBase;
    [SerializeField] public List<BaseSkill> selectableSkillList;
    [SerializeField] public List<BaseSkill> selectedSkillList;

    public BaseCharacter GetCharacterBase()
    {
        return characterBase;
    }

    public GameObject GetCharacterPrefab()
    {
        return CharacterPrefab;
    }

    public BaseSkill GetSkillFromList(int listValue)
    {
        //used to set the character storage skilllist's skill by getting it from the loaderSO;
        return selectableSkillList[listValue];
    }

    public void AddSkillToBattleList(BaseSkill skillToAdd)
    {
        if (selectedSkillList.Count < 4)
        {
            selectedSkillList.Add(skillToAdd);
        }
        else
        {
            selectedSkillList.Remove(selectedSkillList[0]);
            selectedSkillList.Add(skillToAdd);
        }
    }
}
