using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseCharacter", order = 1)]
public class BaseCharacter : ScriptableObject
{
    [Header("Character Information")]
    [SerializeField] protected string characterName;
    [SerializeField] protected string characterDesc;
    [SerializeField] protected Sprite characterBaseSprite;
    [SerializeField] protected Animator characterAnimator;
    [SerializeField] AnimatorController characterAnimController;
    //born to dilly dally forced to lock in. . .

    [Header("Combat Data")]
    [SerializeField] protected float baseHealth;
    [SerializeField] protected float basePhysicalStrength;
    [SerializeField] protected float basePhysicalDefense;
    [SerializeField] protected float baseMagicalStrength;
    [SerializeField] protected float baseMagicalDefense;
    [SerializeField] protected float baseSpeed;
    
    //Physical and Magical stat-types dictate damage they take and receive from their respective types.
    //the intended gameplay manager/creator for entities will call upon these stats to insert them into characters/enemies
    //the statistical values will be used as part of an equation, rather than be directly modified for the sake of 

    //Balancing wise, this helps to create well-defined strengths and weaknesses as a baseline level,
    //as some characters or enemies are stronger or weaker on certain sides, and the game can be balanced to create
    //dynamic uses of these stats, such as boosting them or giving an enemy an abnormally high stat that players have to deal with
    
    public string GetCharacterName()
    {
        return characterName;
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }

    public float GetBasePhysicalStrength()
    {
        return basePhysicalStrength;
    }
    public float GetBasePhysicalDefense()
    {
        return basePhysicalDefense;
    }
    public float GetBaseMagicalStrength()
    {
        return baseMagicalStrength;
    }
    public float GetBaseMagicalDefense()
    {
        return baseMagicalDefense;
    }
    public float GetBaseSpeed()
    {
        return baseSpeed;
    }

    public float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float classMultiplier, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, classMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, classMultiplier, generalMultiplier);
        }   
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, classMultiplier, generalMultiplier);
        }
        else
        {
            return 0;
        }
    }    
}
