using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected TheRogue rogueSO;



    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = rogueSO.GetCharacterName();
        characterMaxHealth = rogueSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = rogueSO.GetBasePhysicalStrength();
        characterPhysicalDefense = rogueSO.GetBasePhysicalDefense();
        characterMagicalStrength = rogueSO.GetBaseMagicalStrength();
        characterMagicalDefense = rogueSO.GetBaseMagicalDefense();
        characterSpeed = rogueSO.GetBaseSpeed();
        characterClassMultiplier = 1;
    }

    //public void ExecuteCommand(BaseSkill skillToUse, BattleEntity targetOfSkill)
    //{
    //    bloodSisterSO.UseSkill(skillToUse, this, targetOfSkill, 1);
    //}

    public override float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);

        }
        else
        {
            return 0;
        }
    }

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        if (skillUsed.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            if (skillUsed.GetSkillSpeedMod() > 0)
            {
                entityAnimator.Play("AttackFast");
            }
            else
            {
                entityAnimator.Play("Attack");
            }
        }

    }
}
