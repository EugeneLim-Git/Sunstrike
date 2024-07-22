using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterLoadData", order = 2)]
public class CharacterLoaderSO : ScriptableObject
{
    [SerializeField] private GameObject CharacterPrefab;
    [SerializeField] private BaseCharacter characterBase;
    [SerializeField] private List<BaseSkill> selectableSkillList;
    [SerializeField] 

    public BaseCharacter GetCharacterBase()
    {
        return characterBase;
    }

    public BaseSkill GetSkillFromList(int listValue)
    {
        //used to set the character storage skilllist's skill by getting it from the loaderSO;
        return selectableSkillList[listValue];
    }
}
